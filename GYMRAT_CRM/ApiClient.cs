using CRM.domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM.winforms
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private const int CompanyId = 1; // TenantA — hardcoded for exam scope

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            // The API returns camelCase JSON but our entities are PascalCase.
            // Without this, every property silently falls back to its default.
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public ApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5004")
            };

            // Attach JWT if a user is logged in
            if (Model.AuthContext.IsLoggedIn)
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", Model.AuthContext.Token);
            }
        }

        // ==================================================================
        // CUSTOMERS
        // ==================================================================

        public async Task<List<Customer>> GetCustomersAsync() =>
            await _http.GetFromJsonAsync<List<Customer>>($"/tenant/{CompanyId}/customers", JsonOptions) ?? new();

        public async Task<Customer?> CreateCustomerAsync(Customer customer)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/customers", customer, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Customer Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Customer>(JsonOptions);
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/customers/{id}", customer, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Customer Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateCustomerAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/customers/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // MEMBERSHIP PLANS
        // ==================================================================

        public async Task<List<MembershipPlan>> GetMembershipPlansAsync() =>
            await _http.GetFromJsonAsync<List<MembershipPlan>>($"/tenant/{CompanyId}/membershipplans", JsonOptions) ?? new();

        public async Task<MembershipPlan?> CreateMembershipPlanAsync(MembershipPlan plan)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/membershipplans", plan, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Plan Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MembershipPlan>(JsonOptions);
        }

        public async Task UpdateMembershipPlanAsync(int id, MembershipPlan plan)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/membershipplans/{id}", plan, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Plan Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateMembershipPlanAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/membershipplans/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // MEMBERSHIP SALES
        // ==================================================================

        public async Task<List<MembershipSale>> GetMembershipSalesAsync() =>
            await _http.GetFromJsonAsync<List<MembershipSale>>($"/tenant/{CompanyId}/membershipsales", JsonOptions) ?? new();

        public async Task<MembershipSale?> CreateMembershipSaleAsync(MembershipSale sale)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/membershipsales", sale, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid sale data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MembershipSale>(JsonOptions);
        }
        public async Task CancelMembershipSaleAsync(int id, string reason)
        {
            var response = await _http.PostAsJsonAsync(
                $"/tenant/{CompanyId}/membershipsales/{id}/cancel",
                new { reason }, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(
                    await ExtractErrorMessageAsync(response, "Cannot cancel this sale."));

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new InvalidOperationException("Sale not found.");

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<MembershipSale>> GetMembershipSalesReportAsync(DateTime from, DateTime to)
        {
            // End of day for 'to' so same-day sales aren't excluded
            var fromStr = from.Date.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var toStr = to.Date.AddDays(1).AddSeconds(-1)
                .ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            return await _http.GetFromJsonAsync<List<MembershipSale>>(
                $"/tenant/{CompanyId}/membershipsales?from={fromStr}&to={toStr}",
                JsonOptions) ?? new();
        }

        // ==================================================================
        // ATTENDANCE
        // ==================================================================

        public async Task<AttendanceDto?> CheckInMemberAsync(CheckInRequest request)
        {
            var response = await _http.PostAsJsonAsync(
                $"/tenant/{CompanyId}/attendance/checkin", request, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Cannot check in."));

            response.EnsureSuccessStatusCode();

            // Response wraps attendance in an anonymous object; extract via JsonDocument
            var doc = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonDocument>(JsonOptions);
            var attendanceJson = doc?.RootElement.GetProperty("attendance").GetRawText();

            if (string.IsNullOrEmpty(attendanceJson)) return null;
            return System.Text.Json.JsonSerializer.Deserialize<AttendanceDto>(attendanceJson, JsonOptions);
        }

        public async Task CheckOutMemberAsync(int attendanceId)
        {
            var response = await _http.PostAsync(
                $"/tenant/{CompanyId}/attendance/{attendanceId}/checkout", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AttendanceDto>> GetTodayAttendanceAsync() =>
            await _http.GetFromJsonAsync<List<AttendanceDto>>(
                $"/tenant/{CompanyId}/attendance/today", JsonOptions) ?? new();

        public async Task<List<AttendanceDto>> GetAttendanceHistoryAsync(int? customerId = null, DateTime? from = null, DateTime? to = null)
        {
            var query = new System.Collections.Generic.List<string>();
            if (customerId.HasValue) query.Add($"customerId={customerId.Value}");
            if (from.HasValue) query.Add($"from={from.Value:yyyy-MM-dd}");
            if (to.HasValue) query.Add($"to={to.Value:yyyy-MM-dd}");

            var queryString = query.Count > 0 ? "?" + string.Join("&", query) : "";
            return await _http.GetFromJsonAsync<List<AttendanceDto>>(
                $"/tenant/{CompanyId}/attendance{queryString}", JsonOptions) ?? new();
        }

        // ==================================================================
        // INQUIRIES
        // ==================================================================

        public async Task<List<Inquiry>> GetInquiriesAsync() =>
            await _http.GetFromJsonAsync<List<Inquiry>>($"/tenant/{CompanyId}/inquiries", JsonOptions) ?? new();

        public async Task<Inquiry?> CreateInquiryAsync(Inquiry inquiry)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/inquiries", inquiry, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid inquiry data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Inquiry>(JsonOptions);
        }

        public async Task UpdateInquiryStatusAsync(int id, RequestStatus status)
        {
            var response = await _http.PutAsJsonAsync(
                $"/tenant/{CompanyId}/inquiries/{id}/status",
                new UpdateStatusRequest { Status = status }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateInquiryAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/inquiries/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // FEEDBACK
        // ==================================================================

        public async Task<List<Feedback>> GetFeedbacksAsync() =>
            await _http.GetFromJsonAsync<List<Feedback>>($"/tenant/{CompanyId}/feedbacks", JsonOptions) ?? new();

        public async Task<Feedback?> CreateFeedbackAsync(Feedback feedback)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/feedbacks", feedback, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid feedback data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Feedback>(JsonOptions);
        }

        public async Task UpdateFeedbackStatusAsync(int id, RequestStatus status)
        {
            var response = await _http.PutAsJsonAsync(
                $"/tenant/{CompanyId}/feedbacks/{id}/status",
                new UpdateStatusRequest { Status = status }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateFeedbackAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/feedbacks/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // CAMPAIGNS
        // ==================================================================

        public async Task<List<Campaign>> GetCampaignsAsync() =>
            await _http.GetFromJsonAsync<List<Campaign>>($"/tenant/{CompanyId}/campaigns", JsonOptions) ?? new();

        public async Task<Campaign?> CreateCampaignAsync(Campaign campaign)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/campaigns", campaign, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Campaign Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Campaign>(JsonOptions);
        }

        public async Task UpdateCampaignAsync(int id, Campaign campaign)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/campaigns/{id}", campaign, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Campaign Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCampaignStatusAsync(int id, CampaignStatus status)
        {
            var response = await _http.PutAsJsonAsync(
                $"/tenant/{CompanyId}/campaigns/{id}/status",
                new UpdateCampaignStatusRequest { Status = status }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateCampaignAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/campaigns/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // PROMOTIONS
        // ==================================================================

        public async Task<List<Promotion>> GetPromotionsAsync() =>
            await _http.GetFromJsonAsync<List<Promotion>>($"/tenant/{CompanyId}/promotions", JsonOptions) ?? new();

        public async Task<Promotion?> CreatePromotionAsync(Promotion promotion)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/promotions", promotion, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Promotion Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Promotion>(JsonOptions);
        }

        public async Task UpdatePromotionAsync(int id, Promotion promotion)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/promotions/{id}", promotion, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Promotion Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivatePromotionAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/promotions/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // PROMO CODES
        // ==================================================================

        public async Task<List<PromoCodeDto>> GetPromoCodesAsync() =>
            await _http.GetFromJsonAsync<List<PromoCodeDto>>($"/tenant/{CompanyId}/promocodes", JsonOptions) ?? new();

        public async Task<PromoCodeDto?> CreatePromoCodeAsync(PromoCodeDto promoCode)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/promocodes", promoCode, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This promo code already exists.");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid promo code data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PromoCodeDto>(JsonOptions);
        }

        public async Task DeactivatePromoCodeAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/promocodes/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // LEADS
        // ==================================================================

        public async Task<List<LeadDto>> GetLeadsAsync() =>
            await _http.GetFromJsonAsync<List<LeadDto>>(
                $"/tenant/{CompanyId}/leads", JsonOptions) ?? new();

        public async Task<LeadDto?> CreateLeadAsync(LeadDto lead)
        {
            var response = await _http.PostAsJsonAsync(
                $"/tenant/{CompanyId}/leads", lead, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Lead Code is already in use — please choose a different one.");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid lead data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LeadDto>(JsonOptions);
        }

        public async Task UpdateLeadAsync(int id, LeadDto lead)
        {
            var response = await _http.PutAsJsonAsync(
                $"/tenant/{CompanyId}/leads/{id}", lead, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Lead Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateLeadStatusAsync(int id, string status)
        {
            var response = await _http.PutAsJsonAsync(
                $"/tenant/{CompanyId}/leads/{id}/status",
                new { status }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task<LeadDto?> ConvertLeadAsync(int id, ConvertLeadRequest request)
        {
            var response = await _http.PostAsJsonAsync(
                $"/tenant/{CompanyId}/leads/{id}/convert", request, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Cannot convert lead."));

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("A customer with this code already exists.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LeadDto>(JsonOptions);
        }

        public async Task DeactivateLeadAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/leads/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // RETENTION
        // ==================================================================

        public async Task<List<AtRiskMemberDto>> GetAtRiskMembersAsync(int days = 30) =>
            await _http.GetFromJsonAsync<List<AtRiskMemberDto>>(
                $"/tenant/{CompanyId}/retention/at-risk?days={days}", JsonOptions) ?? new();

        public async Task<List<WinBackMemberDto>> GetWinBackMembersAsync() =>
            await _http.GetFromJsonAsync<List<WinBackMemberDto>>(
                $"/tenant/{CompanyId}/retention/win-back", JsonOptions) ?? new();

        public async Task<List<Customer>> GetFrozenCustomersAsync() =>
            await _http.GetFromJsonAsync<List<Customer>>(
                $"/tenant/{CompanyId}/retention/frozen", JsonOptions) ?? new();

        public async Task<List<RetentionActionDto>> GetRetentionActionsAsync() =>
            await _http.GetFromJsonAsync<List<RetentionActionDto>>(
                $"/tenant/{CompanyId}/retention/actions", JsonOptions) ?? new();

        public async Task<RetentionActionDto?> LogRetentionActionAsync(LogRetentionActionRequest request)
        {
            var response = await _http.PostAsJsonAsync(
                $"/tenant/{CompanyId}/retention/actions", request, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(
                    await ExtractErrorMessageAsync(response, "Invalid retention action data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RetentionActionDto>(JsonOptions);
        }

        public async Task UpdateRetentionOutcomeAsync(int actionId, string outcome, string? notes)
        {
            var response = await _http.PutAsJsonAsync(
                $"/tenant/{CompanyId}/retention/actions/{actionId}/outcome",
                new { outcome, notes }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task FreezeCustomerAsync(int customerId, DateTime? frozenUntil, string? notes)
        {
            var response = await _http.PostAsJsonAsync(
                $"/tenant/{CompanyId}/retention/freeze/{customerId}",
                new { frozenUntil, notes }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task UnfreezeCustomerAsync(int customerId)
        {
            var response = await _http.DeleteAsync(
                $"/tenant/{CompanyId}/retention/freeze/{customerId}");
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // STAFF MANAGEMENT
        // ==================================================================

        public async Task<List<StaffDto>> GetStaffAsync() =>
            await _http.GetFromJsonAsync<List<StaffDto>>("/staff", JsonOptions) ?? new();

        public async Task<StaffDto?> CreateStaffAsync(CreateStaffRequest request)
        {
            var response = await _http.PostAsJsonAsync("/staff", request, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This username is already taken.");

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                throw new InvalidOperationException("You do not have permission to create this role.");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid staff data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StaffDto>(JsonOptions);
        }

        public async Task UpdateStaffAsync(int id, UpdateStaffRequest request)
        {
            var response = await _http.PutAsJsonAsync($"/staff/{id}", request, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateStaffAsync(int id)
        {
            var response = await _http.DeleteAsync($"/staff/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task ResetStaffPasswordAsync(int id, string newPassword)
        {
            var response = await _http.PostAsJsonAsync(
                $"/staff/{id}/reset-password",
                new { newPassword }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        // ==================================================================
        // BRANCHES
        // ==================================================================

        public async Task<List<Branch>> GetBranchesAsync() =>
            await _http.GetFromJsonAsync<List<Branch>>(
                $"/tenant/{CompanyId}/branches", JsonOptions) ?? new();

        // ==================================================================
        // AUTH
        // ==================================================================

        public async Task<LoginResult?> LoginAsync(string username, string password)
        {
            var response = await _http.PostAsJsonAsync("/auth/login",
                new { username, password }, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null; // invalid credentials

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LoginResult>(JsonOptions);
        }

        // ==================================================================
        // SHARED HELPERS
        // ==================================================================

        private static async Task<string> ExtractErrorMessageAsync(HttpResponseMessage response, string fallback)
        {
            try
            {
                var doc = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                if (doc != null && doc.TryGetValue("message", out var msg)) return msg;
            }
            catch { }
            return fallback;
        }

        // ==================================================================
        // DTOs
        // ==================================================================

        public class LoginResult
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
            public int? CompanyId { get; set; }
            public int? BranchId { get; set; }
            public List<string> Permissions { get; set; } = new();
            public string Token { get; set; } = string.Empty;
            public DateTime ExpiresAt { get; set; }
        }

        public class PromoCodeDto
        {
            public int PromoCodeId { get; set; }
            public string Code { get; set; } = string.Empty;
            public int PromotionId { get; set; }
            public int? MaxUses { get; set; }
            public int CurrentUses { get; set; }
            public DateTime? ExpiresAt { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public Promotion? Promotion { get; set; }
        }

        public class StaffDto
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public int? CompanyId { get; set; }
            public int? BranchId { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? LastLoginAt { get; set; }
        }

        public class CreateStaffRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public int? BranchId { get; set; }
        }

        public class UpdateStaffRequest
        {
            public string Username { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public int? BranchId { get; set; }
            public bool IsActive { get; set; }
        }

        public class AtRiskMemberDto
        {
            public int CustomerId { get; set; }
            public string CustomerCode { get; set; } = string.Empty;
            public string CustomerName { get; set; } = string.Empty;
            public string? ContactNumber { get; set; }
            public string? EmailAddress { get; set; }
            public string PlanName { get; set; } = string.Empty;
            public DateTime ExpiryDate { get; set; }
            public int DaysLeft { get; set; }
            public string Status { get; set; } = string.Empty;
            public bool IsFrozen { get; set; }
        }

        public class WinBackMemberDto
        {
            public int CustomerId { get; set; }
            public string CustomerCode { get; set; } = string.Empty;
            public string CustomerName { get; set; } = string.Empty;
            public string? ContactNumber { get; set; }
            public string? EmailAddress { get; set; }
            public string LastPlanName { get; set; } = string.Empty;
            public DateTime LastExpiry { get; set; }
            public int DaysExpired { get; set; }
        }

        public class RetentionActionDto
        {
            public int RetentionActionId { get; set; }
            public int CustomerId { get; set; }
            public int PerformedByUserId { get; set; }
            public string ActionType { get; set; } = string.Empty;
            public string? Notes { get; set; }
            public DateTime? FollowUpDate { get; set; }
            public string Outcome { get; set; } = string.Empty;
            public DateTime Timestamp { get; set; }
            public Customer? Customer { get; set; }
        }

        public class LogRetentionActionRequest
        {
            public int CustomerId { get; set; }
            public string ActionType { get; set; } = string.Empty;
            public string? Notes { get; set; }
            public DateTime? FollowUpDate { get; set; }
            public string Outcome { get; set; } = "Pending";
        }

        public class LeadDto
        {
            public int LeadId { get; set; }
            public string LeadCode { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? ContactNumber { get; set; }
            public string? EmailAddress { get; set; }
            public string? Address { get; set; }
            public string Source { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string? Notes { get; set; }
            public int? ConvertedCustomerId { get; set; }
            public DateTime? ConvertedAt { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public Customer? ConvertedCustomer { get; set; }
        }

        public class ConvertLeadRequest
        {
            public string CustomerCode { get; set; } = string.Empty;
            public string? Address { get; set; }
        }

        public class AttendanceDto
        {
            public int AttendanceId { get; set; }
            public int CustomerId { get; set; }
            public int? BranchId { get; set; }
            public DateTime CheckInTime { get; set; }
            public DateTime? CheckOutTime { get; set; }
            public string? Notes { get; set; }
            public bool IsActive { get; set; }
            public Customer? Customer { get; set; }
        }

        public class CheckInRequest
        {
            public int CustomerId { get; set; }
            public int? BranchId { get; set; }
            public string? Notes { get; set; }
        }
    }
}