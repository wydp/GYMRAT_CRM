using CRM.domain.Entities;
using CRM.winforms.LocalData;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRM.winforms.Sync
{
    public class SyncService
    {
        private readonly LocalDbContext _local;
        private readonly ApiClient _api;
        private readonly string _logPath;
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public SyncService(LocalDbContext localDbContext, ApiClient apiClient)
        {
            _local = localDbContext;
            _api = apiClient;
            var dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GymRat");
            System.IO.Directory.CreateDirectory(dir);
            _logPath = System.IO.Path.Combine(dir, "sync.log");
        }

        public async Task SyncAsync()
        {
            // Ensure local DB exists
            await _local.Database.EnsureCreatedAsync();

            Log("Sync started");

            // 1. Push local outbox entries to server
            await PushOutboxAsync();

            // 2. Pull authoritative data from server and upsert locally
            await PullAndApplyAsync();

            Log("Sync finished");
        }

        private async Task PushOutboxAsync()
        {
            var pending = await _local.OutboxEntries
                .OrderBy(o => o.CreatedAtUtc)
                .ToListAsync();

            Log($"PushOutbox: {pending.Count} entries pending");
            foreach (var entry in pending)
            {
                try
                {
                    switch (entry.EntityType)
                    {
                        case "Customer":
                            var cust = JsonSerializer.Deserialize<Customer>(entry.Payload, JsonOptions);
                            if (cust != null)
                            {
                                if (entry.Operation == "Create")
                                {
                                    var created = await _api.CreateCustomerAsync(cust);
                                    if (created != null)
                                    {
                                        // Update local record id if server assigned a different id
                                        var local = await _local.Customers.FirstOrDefaultAsync(c => c.CustomerCode == created.CustomerCode);
                                        if (local != null && local.CustomerId != created.CustomerId)
                                        {
                                            local.CustomerId = created.CustomerId;
                                            _local.Customers.Update(local);
                                            await _local.SaveChangesAsync();
                                        }
                                    }
                                }
                                else if (entry.Operation == "Update")
                                {
                                    await _api.UpdateCustomerAsync(cust.CustomerId, cust);
                                }
                                else if (entry.Operation == "Delete")
                                {
                                    await _api.DeactivateCustomerAsync(cust.CustomerId);
                                }
                            }
                            break;

                        case "MembershipPlan":
                            var plan = JsonSerializer.Deserialize<MembershipPlan>(entry.Payload, JsonOptions);
                            if (plan != null)
                            {
                                if (entry.Operation == "Create")
                                {
                                    var created = await _api.CreateMembershipPlanAsync(plan);
                                    if (created != null)
                                    {
                                        var local = await _local.MembershipPlans.FirstOrDefaultAsync(p => p.PlanCode == created.PlanCode);
                                        if (local != null && local.MembershipPlanId != created.MembershipPlanId)
                                        {
                                            local.MembershipPlanId = created.MembershipPlanId;
                                            _local.MembershipPlans.Update(local);
                                            await _local.SaveChangesAsync();
                                        }
                                    }
                                }
                                else if (entry.Operation == "Update")
                                {
                                    await _api.UpdateMembershipPlanAsync(plan.MembershipPlanId, plan);
                                }
                                else if (entry.Operation == "Delete")
                                {
                                    await _api.DeactivateMembershipPlanAsync(plan.MembershipPlanId);
                                }
                            }
                            break;

                        case "MembershipSale":
                            var sale = JsonSerializer.Deserialize<MembershipSale>(entry.Payload, JsonOptions);
                            if (sale != null)
                            {
                                if (entry.Operation == "Create")
                                {
                                    var created = await _api.CreateMembershipSaleAsync(sale);
                                    // created will have server-assigned id
                                }
                                else if (entry.Operation == "Update")
                                {
                                    // No generic update endpoint for sales in ApiClient; skip
                                }
                                else if (entry.Operation == "Delete")
                                {
                                    // No delete endpoint for sales
                                }
                            }
                            break;

                        default:
                            // Unknown entity type — skip
                            break;
                    }

                    // If we reach here, assume success — remove outbox entry
                    _local.OutboxEntries.Remove(entry);
                    await _local.SaveChangesAsync();
                    Log($"Pushed outbox entry {entry.OutboxEntryId} ({entry.EntityType}/{entry.Operation})");
                }
                catch (Exception)
                {
                    // Increment attempt count so we don't retry indefinitely without backoff
                    entry.AttemptCount++;
                    _local.OutboxEntries.Update(entry);
                    await _local.SaveChangesAsync();
                    Log($"Failed pushing outbox entry {entry.OutboxEntryId}; attempt {entry.AttemptCount}");
                }
            }
        }

        private async Task PullAndApplyAsync()
        {
            Log("PullAndApply: starting pull");
            // Customers
            var customers = await _api.GetCustomersAsync();
            foreach (var c in customers)
            {
                var local = await _local.Customers.FirstOrDefaultAsync(x => x.CustomerCode == c.CustomerCode);
                if (local == null)
                {
                    _local.Customers.Add(c);
                }
                else
                {
                    // update fields
                    local.CustomerName = c.CustomerName;
                    local.ContactNumber = c.ContactNumber;
                    local.EmailAddress = c.EmailAddress;
                    local.Address = c.Address;
                    local.IsActive = c.IsActive;
                    _local.Customers.Update(local);
                }
            }

            // Membership Plans
            var plans = await _api.GetMembershipPlansAsync();
            foreach (var p in plans)
            {
                var local = await _local.MembershipPlans.FirstOrDefaultAsync(x => x.PlanCode == p.PlanCode);
                if (local == null)
                {
                    _local.MembershipPlans.Add(p);
                }
                else
                {
                    local.PlanName = p.PlanName;
                    local.Description = p.Description;
                    local.Price = p.Price;
                    local.DurationInDays = p.DurationInDays;
                    local.IsActive = p.IsActive;
                    _local.MembershipPlans.Update(local);
                }
            }

            // Membership sales (reporting)
            var sales = await _api.GetMembershipSalesAsync();
            foreach (var s in sales)
            {
                var local = await _local.MembershipSales.FirstOrDefaultAsync(x => x.MembershipSaleId == s.MembershipSaleId);
                if (local == null)
                {
                    _local.MembershipSales.Add(s);
                }
                else
                {
                    local.AmountPaid = s.AmountPaid;
                    local.IsCancelled = s.IsCancelled;
                    local.CancellationReason = s.CancellationReason;
                    _local.MembershipSales.Update(local);
                }
            }

            await _local.SaveChangesAsync();
            Log($"PullAndApply: applied customers={customers.Count}, plans={plans.Count}, sales={sales.Count}");
        }

        private void Log(string message)
        {
            try
            {
                var ts = DateTime.UtcNow.ToString("o");
                var line = $"[{ts}] {message}{Environment.NewLine}";
                System.IO.File.AppendAllText(_logPath, line);
            }
            catch { /* swallow logging errors */ }
        }
    }
}
