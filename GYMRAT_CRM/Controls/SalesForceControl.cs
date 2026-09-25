using CRM.domain.Entities;
using CRM.winforms.LocalData;
using CRM.winforms.Sync;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class SalesForceControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private static readonly CultureInfo PesoCulture = CultureInfo.GetCultureInfo("en-PH");

        // Class-level field so WireEvents() can see it
        private Button btnCancelMembership = null!;

        // Cache the loaded lists so we can look up the selected item's price/ID
        // without another HTTP call.
        private List<Customer> _customers = new();
        private List<MembershipPlan> _plans = new();

        public SalesForceControl()
        {
            InitializeComponent();
            StyleGrid();
            StyleMembershipsGrid();
            StyleRenewalsGrid();

            BuildCancelMembershipButton();  // ← builds + adds to tab

            WireEvents();
            _ = LoadInitialDataAsync();

            var leadControl = new LeadControl { Dock = DockStyle.Fill };
            tabLeads.Controls.Add(leadControl);

            var checkInControl = new CheckInControl { Dock = DockStyle.Fill };
            tabCheckIn.Controls.Add(checkInControl);

            var historyControl = new AttendanceHistoryControl { Dock = DockStyle.Fill };
            tabHistory.Controls.Add(historyControl);
        }

        private void BuildCancelMembershipButton()
        {
            btnCancelMembership = new Button
            {
                Name = "btnCancelMembership",
                Text = "Cancel Membership",
                Size = new Size(160, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(185, 28, 28),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Location = new Point(600, 10),
            };
            btnCancelMembership.FlatAppearance.BorderSize = 0;

            tabMemberships.Controls.Add(btnCancelMembership);
            btnCancelMembership.BringToFront();
        }

        private void StyleGrid()
        {
            dgvRecentSales.BorderStyle = BorderStyle.None;
            dgvRecentSales.RowHeadersVisible = false;
            dgvRecentSales.AllowUserToAddRows = false;
            dgvRecentSales.AllowUserToDeleteRows = false;
            dgvRecentSales.ReadOnly = true;
            dgvRecentSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentSales.MultiSelect = false;
            dgvRecentSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentSales.EnableHeadersVisualStyles = false;
            dgvRecentSales.GridColor = Color.FromArgb(229, 231, 235);
            dgvRecentSales.ColumnHeadersHeight = 40;
            dgvRecentSales.RowTemplate.Height = 36;

            dgvRecentSales.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvRecentSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvRecentSales.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvRecentSales.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvRecentSales.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvRecentSales.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvRecentSales.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvRecentSales.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvRecentSales.Columns.Add("SaleDate", "Sale Date");
            dgvRecentSales.Columns.Add("CustomerName", "Customer");
            dgvRecentSales.Columns.Add("PlanName", "Plan");
            dgvRecentSales.Columns.Add("AmountPaid", "Amount Paid");

            dgvRecentSales.Columns["AmountPaid"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void StyleMembershipsGrid()
        {
            dgvMemberships.BorderStyle = BorderStyle.None;
            dgvMemberships.RowHeadersVisible = false;
            dgvMemberships.AllowUserToAddRows = false;
            dgvMemberships.AllowUserToDeleteRows = false;
            dgvMemberships.ReadOnly = true;
            dgvMemberships.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMemberships.MultiSelect = false;
            dgvMemberships.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemberships.EnableHeadersVisualStyles = false;
            dgvMemberships.GridColor = Color.FromArgb(229, 231, 235);
            dgvMemberships.ColumnHeadersHeight = 40;
            dgvMemberships.RowTemplate.Height = 36;

            dgvMemberships.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvMemberships.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvMemberships.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvMemberships.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvMemberships.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvMemberships.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMemberships.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvMemberships.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvMemberships.Columns.Add("CustomerName", "Customer");
            dgvMemberships.Columns.Add("PlanName", "Current Plan");
            dgvMemberships.Columns.Add("StartDate", "Start");
            dgvMemberships.Columns.Add("ExpiryDate", "Expires");
            dgvMemberships.Columns.Add("DaysLeft", "Days Left");
            dgvMemberships.Columns.Add("Status", "Status");
        }

        private void StyleRenewalsGrid()
        {
            dgvRenewals.BorderStyle = BorderStyle.None;
            dgvRenewals.RowHeadersVisible = false;
            dgvRenewals.AllowUserToAddRows = false;
            dgvRenewals.AllowUserToDeleteRows = false;
            dgvRenewals.ReadOnly = true;
            dgvRenewals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRenewals.MultiSelect = false;
            dgvRenewals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRenewals.EnableHeadersVisualStyles = false;
            dgvRenewals.GridColor = Color.FromArgb(229, 231, 235);
            dgvRenewals.ColumnHeadersHeight = 40;
            dgvRenewals.RowTemplate.Height = 36;

            dgvRenewals.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvRenewals.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvRenewals.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvRenewals.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvRenewals.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvRenewals.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvRenewals.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvRenewals.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvRenewals.Columns.Add("CustomerName", "Customer");
            dgvRenewals.Columns.Add("PlanName", "Current Plan");
            dgvRenewals.Columns.Add("ExpiryDate", "Expires");
            dgvRenewals.Columns.Add("DaysLeft", "Days Left");
            dgvRenewals.Columns.Add("PlanPrice", "Renewal Cost");
        }

        private void WireEvents()
        {
            btnProcessSale.Click += async (s, e) => await ProcessSaleAsync();
            btnClear.Click += (s, e) => ClearForm();
            cmbPlan.SelectedIndexChanged += (s, e) => AutoFillAmountFromPlan();

            btnRefreshMemberships.Click += async (s, e) => await LoadMembershipsAsync();
            chkMembershipsActiveOnly.CheckedChanged += async (s, e) => await LoadMembershipsAsync();
            btnCancelMembership.Click += async (s, e) => await CancelSelectedMembershipAsync();

            btnRefreshRenewals.Click += async (s, e) => await LoadRenewalsAsync();
            btnRenew.Click += async (s, e) => await RenewSelectedAsync();
        }

        private async System.Threading.Tasks.Task LoadInitialDataAsync()
        {
            try
            {
                _customers = await _api.GetCustomersAsync();
                _plans = await _api.GetMembershipPlansAsync();

                cmbCustomer.DataSource = _customers;
                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "CustomerId";
                cmbCustomer.SelectedIndex = -1;

                cmbPlan.DataSource = _plans;
                cmbPlan.DisplayMember = "PlanName";
                cmbPlan.ValueMember = "MembershipPlanId";
                cmbPlan.SelectedIndex = -1;

                dtpSaleDate.Value = DateTime.Today;

                await LoadRecentSalesAsync();
                await LoadMembershipsAsync();
                await LoadRenewalsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task LoadRecentSalesAsync()
        {
            var sales = await _api.GetMembershipSalesAsync();
            dgvRecentSales.Rows.Clear();

            foreach (var s in sales.OrderByDescending(x => x.SaleDate).Take(30))
            {
                var row = new DataGridViewRow();
                row.CreateCells(dgvRecentSales,
                    s.SaleDate.ToString("yyyy-MM-dd"),
                    s.Customer?.CustomerName ?? "(unknown)",
                    s.MembershipPlan?.PlanName ?? "(unknown)",
                    s.AmountPaid.ToString("C2", PesoCulture));
                row.Tag = s.MembershipSaleId;

                dgvRecentSales.Rows.Add(row);
            }
        }

        private class MembershipRow
        {
            public int MembershipSaleId { get; set; }
            public int CustomerId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string PlanName { get; set; } = string.Empty;
            public DateTime StartDate { get; set; }
            public DateTime ExpiryDate { get; set; }
            public int DaysLeft { get; set; }
            public string Status { get; set; } = string.Empty;
        }

        private async System.Threading.Tasks.Task LoadMembershipsAsync()
        {
            try
            {
                var sales = await _api.GetMembershipSalesAsync();

                var latestPerCustomer = sales
                    .Where(s => s.Customer != null && s.MembershipPlan != null)
                    .GroupBy(s => s.CustomerId)
                    .Select(g => g.OrderByDescending(s => s.SaleDate).First())
                    .ToList();

                var today = DateTime.UtcNow.Date;
                var rows = new List<MembershipRow>();

                foreach (var s in latestPerCustomer)
                {
                    var expiry = s.SaleDate.Date.AddDays(s.MembershipPlan!.DurationInDays);
                    var daysLeft = (expiry - today).Days;

                    string status;
                    if (daysLeft < 0) status = "Expired";
                    else if (daysLeft <= 30) status = "Expiring Soon";
                    else status = "Active";

                    rows.Add(new MembershipRow
                    {
                        MembershipSaleId = s.MembershipSaleId,
                        CustomerId = s.CustomerId,
                        CustomerName = s.Customer!.CustomerName,
                        PlanName = s.MembershipPlan.PlanName,
                        StartDate = s.SaleDate.Date,
                        ExpiryDate = expiry,
                        DaysLeft = daysLeft,
                        Status = status,
                    });
                }

                if (chkMembershipsActiveOnly.Checked)
                    rows = rows.Where(r => r.Status != "Expired").ToList();

                dgvMemberships.Rows.Clear();

                foreach (var r in rows.OrderBy(x => x.ExpiryDate))
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvMemberships,
                        r.CustomerName ?? string.Empty,
                        r.PlanName ?? string.Empty,
                        r.StartDate.ToString("yyyy-MM-dd"),
                        r.ExpiryDate.ToString("yyyy-MM-dd"),
                        r.DaysLeft >= 0 ? r.DaysLeft.ToString() : "—",
                        r.Status);

                    row.Tag = r.MembershipSaleId;

                    int rowIndex = dgvMemberships.Rows.Add(row);

                    var statusCell = dgvMemberships.Rows[rowIndex].Cells["Status"];
                    statusCell.Style.ForeColor = r.Status switch
                    {
                        "Active" => Color.FromArgb(21, 128, 61),
                        "Expiring Soon" => Color.FromArgb(180, 83, 9),
                        _ => Color.FromArgb(185, 28, 28),
                    };
                }

                int active = rows.Count(r => r.Status == "Active");
                int expiring = rows.Count(r => r.Status == "Expiring Soon");
                int expired = rows.Count(r => r.Status == "Expired");

                lblMembershipsSummary.Text =
                    $"{rows.Count} memberships — {active} active, {expiring} expiring soon, {expired} expired.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load memberships: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class RenewalRow
        {
            public int CustomerId { get; set; }
            public int MembershipPlanId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string PlanName { get; set; } = string.Empty;
            public DateTime ExpiryDate { get; set; }
            public int DaysLeft { get; set; }
            public decimal PlanPrice { get; set; }
        }

        private List<RenewalRow> _renewalRows = new();

        private async System.Threading.Tasks.Task LoadRenewalsAsync()
        {
            try
            {
                var sales = await _api.GetMembershipSalesAsync();

                var latestPerCustomer = sales
                    .Where(s => s.Customer != null && s.MembershipPlan != null)
                    .GroupBy(s => s.CustomerId)
                    .Select(g => g.OrderByDescending(s => s.SaleDate).First())
                    .ToList();

                var today = DateTime.UtcNow.Date;
                _renewalRows = new List<RenewalRow>();

                foreach (var s in latestPerCustomer)
                {
                    var expiry = s.SaleDate.Date.AddDays(s.MembershipPlan!.DurationInDays);
                    var daysLeft = (expiry - today).Days;

                    if (daysLeft > 30) continue;

                    _renewalRows.Add(new RenewalRow
                    {
                        CustomerId = s.CustomerId,
                        MembershipPlanId = s.MembershipPlanId,
                        CustomerName = s.Customer!.CustomerName,
                        PlanName = s.MembershipPlan.PlanName,
                        ExpiryDate = expiry,
                        DaysLeft = daysLeft,
                        PlanPrice = s.MembershipPlan.Price,
                    });
                }

                dgvRenewals.Rows.Clear();

                foreach (var r in _renewalRows.OrderBy(x => x.DaysLeft))
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvRenewals,
                        r.CustomerName ?? string.Empty,
                        r.PlanName ?? string.Empty,
                        r.ExpiryDate.ToString("yyyy-MM-dd"),
                        r.DaysLeft >= 0 ? r.DaysLeft.ToString() : $"{-r.DaysLeft} overdue",
                        r.PlanPrice.ToString("C2", PesoCulture));
                    row.Tag = r.CustomerId;

                    int rowIndex = dgvRenewals.Rows.Add(row);

                    var daysCell = dgvRenewals.Rows[rowIndex].Cells["DaysLeft"];
                    daysCell.Style.ForeColor = r.DaysLeft < 0
                        ? Color.FromArgb(185, 28, 28)
                        : Color.FromArgb(180, 83, 9);
                }

                lblRenewalsSummary.Text = _renewalRows.Count == 0
                    ? "No members are expiring in the next 30 days. 🎉"
                    : $"{_renewalRows.Count} member(s) need renewal attention.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load renewals: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task RenewSelectedAsync()
        {
            if (dgvRenewals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a member to renew.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvRenewals.SelectedRows[0];
            if (selectedRow.Tag is not int customerId) return;

            var renewal = _renewalRows.FirstOrDefault(r => r.CustomerId == customerId);
            if (renewal == null) return;

            var confirm = MessageBox.Show(
                $"Renew {renewal.CustomerName}'s {renewal.PlanName} membership for {renewal.PlanPrice.ToString("C2", PesoCulture)}?",
                "Confirm Renewal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var sale = new MembershipSale
            {
                CustomerId = renewal.CustomerId,
                MembershipPlanId = renewal.MembershipPlanId,
                SaleDate = DateTime.UtcNow,
                AmountPaid = renewal.PlanPrice,
                IsActive = true,
            };

            try
            {
                // Local-first: write renewal sale to local DB and enqueue outbox
                var dbPath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "GymRat", $"tenant_{Model.AuthContext.CompanyId}.sqlite");
                var options = new DbContextOptionsBuilder<LocalDbContext>()
                    .UseSqlite($"Data Source={dbPath}")
                    .Options;
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dbPath) ?? ".");

                using var localDb = new LocalDbContext(options);
                await localDb.Database.EnsureCreatedAsync();

                sale.MembershipSaleId = 0;
                sale.CreatedAt = DateTime.UtcNow;

                await localDb.MembershipSales.AddAsync(sale);
                // Ensure referenced Customer and MembershipPlan exist locally to satisfy FK constraints
                var existingCust = await localDb.Customers.FirstOrDefaultAsync(c => c.CustomerId == sale.CustomerId);
                if (existingCust == null)
                {
                    var cust = _customers.FirstOrDefault(c => c.CustomerId == sale.CustomerId);
                    if (cust != null)
                    {
                        await localDb.Customers.AddAsync(new Customer
                        {
                            CustomerId = cust.CustomerId,
                            CustomerCode = cust.CustomerCode,
                            CustomerName = cust.CustomerName,
                            ContactNumber = cust.ContactNumber,
                            EmailAddress = cust.EmailAddress,
                            Address = cust.Address,
                            IsActive = cust.IsActive,
                            CreatedAt = cust.CreatedAt
                        });
                    }
                }

                var existingPlan = await localDb.MembershipPlans.FirstOrDefaultAsync(p => p.MembershipPlanId == sale.MembershipPlanId);
                if (existingPlan == null)
                {
                    var planObj = _plans.FirstOrDefault(p => p.MembershipPlanId == sale.MembershipPlanId);
                    if (planObj != null)
                    {
                        await localDb.MembershipPlans.AddAsync(new MembershipPlan
                        {
                            MembershipPlanId = planObj.MembershipPlanId,
                            PlanCode = planObj.PlanCode,
                            PlanName = planObj.PlanName,
                            Description = planObj.Description,
                            Price = planObj.Price,
                            DurationInDays = planObj.DurationInDays,
                            IsActive = planObj.IsActive,
                            CreatedAt = planObj.CreatedAt
                        });
                    }
                }

                await localDb.SaveChangesAsync();

                // Avoid serializing navigation properties (which would cause the API to attempt
                // inserting related entities). Serialize a minimal DTO with only scalar fields.
                sale.Customer = null;
                sale.MembershipPlan = null;

                var outbox = new OutboxEntry
                {
                    EntityType = "MembershipSale",
                    Operation = "Create",
                    Payload = JsonSerializer.Serialize(new
                    {
                        CustomerId = sale.CustomerId,
                        MembershipPlanId = sale.MembershipPlanId,
                        SaleDate = sale.SaleDate,
                        AmountPaid = sale.AmountPaid,
                        IsActive = sale.IsActive,
                        CreatedAt = sale.CreatedAt
                    })
                };
                await localDb.OutboxEntries.AddAsync(outbox);
                await localDb.SaveChangesAsync();

                SyncManager.Instance.QueueImmediateSync();

                MessageBox.Show($"Renewed {renewal.CustomerName}'s membership (local).", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadRecentSalesAsync();
                await LoadMembershipsAsync();
                await LoadRenewalsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not renew: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task CancelSelectedMembershipAsync()
        {
            if (dgvMemberships.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a membership to cancel.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvMemberships.SelectedRows[0];
            if (row.Tag is not int saleId) return;

            var customerName = row.Cells["CustomerName"].Value?.ToString() ?? "(unknown)";
            var planName = row.Cells["PlanName"].Value?.ToString() ?? "(unknown)";
            var status = row.Cells["Status"].Value?.ToString() ?? "";

            if (status == "Expired")
            {
                MessageBox.Show(
                    "This membership is already expired. Cancellation only applies to active memberships.",
                    "Already Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new CancelReasonDialog(customerName, planName);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            var reason = dialog.Reason!;

            try
            {
                await _api.CancelMembershipSaleAsync(saleId, reason);
                MessageBox.Show(
                    $"Cancelled {customerName}'s membership.",
                    "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadRecentSalesAsync();
                await LoadMembershipsAsync();
                await LoadRenewalsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cancellation Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not cancel: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoFillAmountFromPlan()
        {
            if (cmbPlan.SelectedItem is MembershipPlan plan)
            {
                var price = plan.Price;
                if (price < numAmount.Minimum) price = numAmount.Minimum;
                if (price > numAmount.Maximum) price = numAmount.Maximum;
                numAmount.Value = price;
            }
        }

        private async System.Threading.Tasks.Task ProcessSaleAsync()
        {
            if (cmbCustomer.SelectedItem is not Customer customer)
            {
                MessageBox.Show("Please select a customer.", "Missing Customer",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbPlan.SelectedItem is not MembershipPlan plan)
            {
                MessageBox.Show("Please select a membership plan.", "Missing Plan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numAmount.Value <= 0m)
            {
                MessageBox.Show("Amount must be greater than zero.", "Invalid Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var sale = new MembershipSale
            {
                CustomerId = customer.CustomerId,
                MembershipPlanId = plan.MembershipPlanId,
                SaleDate = dtpSaleDate.Value.ToUniversalTime(),
                AmountPaid = numAmount.Value,
                IsActive = true,
            };

            try
            {
                // Local-first: write sale to local SQLite and enqueue outbox entry
                var dbPath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "GymRat", $"tenant_{Model.AuthContext.CompanyId}.sqlite");
                var options = new DbContextOptionsBuilder<LocalDbContext>()
                    .UseSqlite($"Data Source={dbPath}")
                    .Options;
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dbPath) ?? ".");

                using var localDb = new LocalDbContext(options);
                await localDb.Database.EnsureCreatedAsync();

                // Ensure we don't accidentally set an identity value from client
                sale.MembershipSaleId = 0;
                sale.CreatedAt = DateTime.UtcNow;

                await localDb.MembershipSales.AddAsync(sale);
                // Ensure referenced Customer and MembershipPlan exist locally to satisfy FK constraints
                var existingCust = await localDb.Customers.FirstOrDefaultAsync(c => c.CustomerId == sale.CustomerId);
                if (existingCust == null)
                {
                    var cust = _customers.FirstOrDefault(c => c.CustomerId == sale.CustomerId);
                    if (cust != null)
                    {
                        await localDb.Customers.AddAsync(new Customer
                        {
                            CustomerId = cust.CustomerId,
                            CustomerCode = cust.CustomerCode,
                            CustomerName = cust.CustomerName,
                            ContactNumber = cust.ContactNumber,
                            EmailAddress = cust.EmailAddress,
                            Address = cust.Address,
                            IsActive = cust.IsActive,
                            CreatedAt = cust.CreatedAt
                        });
                    }
                }

                var existingPlan = await localDb.MembershipPlans.FirstOrDefaultAsync(p => p.MembershipPlanId == sale.MembershipPlanId);
                if (existingPlan == null)
                {
                    var planObj = _plans.FirstOrDefault(p => p.MembershipPlanId == sale.MembershipPlanId);
                    if (planObj != null)
                    {
                        await localDb.MembershipPlans.AddAsync(new MembershipPlan
                        {
                            MembershipPlanId = planObj.MembershipPlanId,
                            PlanCode = planObj.PlanCode,
                            PlanName = planObj.PlanName,
                            Description = planObj.Description,
                            Price = planObj.Price,
                            DurationInDays = planObj.DurationInDays,
                            IsActive = planObj.IsActive,
                            CreatedAt = planObj.CreatedAt
                        });
                    }
                }

                await localDb.SaveChangesAsync();

                // Avoid serializing navigation properties (which would cause the API to attempt
                // inserting related entities). Serialize a minimal DTO with only scalar fields.
                sale.Customer = null;
                sale.MembershipPlan = null;

                var outbox = new OutboxEntry
                {
                    EntityType = "MembershipSale",
                    Operation = "Create",
                    Payload = JsonSerializer.Serialize(new
                    {
                        CustomerId = sale.CustomerId,
                        MembershipPlanId = sale.MembershipPlanId,
                        SaleDate = sale.SaleDate,
                        AmountPaid = sale.AmountPaid,
                        IsActive = sale.IsActive,
                        CreatedAt = sale.CreatedAt
                    })
                };
                await localDb.OutboxEntries.AddAsync(outbox);
                await localDb.SaveChangesAsync();

                // Trigger background sync
                SyncManager.Instance.QueueImmediateSync();

                MessageBox.Show(
                    $"Sale recorded locally: {customer.CustomerName} — {plan.PlanName} — {numAmount.Value.ToString("C2", PesoCulture)}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                await LoadRecentSalesAsync();
                await LoadMembershipsAsync();
                await LoadRenewalsAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Sale Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not process sale: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            cmbCustomer.SelectedIndex = -1;
            cmbPlan.SelectedIndex = -1;
            numAmount.Value = 0;
            dtpSaleDate.Value = DateTime.Today;
        }
    }
}