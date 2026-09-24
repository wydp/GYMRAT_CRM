using CRM.domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace CRM.winforms
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private const int CompanyId = 1; // TenantA for now — hardcoded is fine for this exam scope

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            // The API returns camelCase JSON (customerCode, isActive, ...) but your
            // C# entities use PascalCase (CustomerCode, IsActive, ...). System.Text.Json
            // matches property names case-SENSITIVELY by default, so without this,
            // every property silently falls back to its default value instead of
            // throwing an error — which is why this bug was so easy to miss.
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public ApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new System.Uri("http://localhost:5004")
            };

            // If a user is logged in, attach the JWT to every request.
            // Token comes from AuthContext, set by the login form.
            if (Model.AuthContext.IsLoggedIn)
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", Model.AuthContext.Token);
            }
        }

        // ============================================================
        // ===================== CUSTOMERS ==============================
        // ============================================================

        public async Task<List<Customer>> GetCustomersAsync() =>
            await _http.GetFromJsonAsync<List<Customer>>($"/tenant/{CompanyId}/customers", JsonOptions) ?? new();

        public async Task<Customer?> CreateCustomerAsync(Customer customer)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/customers", customer, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Customer Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Customer>(JsonOptions);
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/customers/{id}", customer, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Customer Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateCustomerAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/customers/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ============================================================
        // ===================== MEMBERSHIP PLANS ========================
        // ============================================================

        public async Task<List<MembershipPlan>> GetMembershipPlansAsync() =>
            await _http.GetFromJsonAsync<List<MembershipPlan>>($"/tenant/{CompanyId}/membershipplans", JsonOptions) ?? new();

        public async Task<MembershipPlan?> CreateMembershipPlanAsync(MembershipPlan plan)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/membershipplans", plan, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Plan Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MembershipPlan>(JsonOptions);
        }

        public async Task UpdateMembershipPlanAsync(int id, MembershipPlan plan)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/membershipplans/{id}", plan, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Plan Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateMembershipPlanAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/membershipplans/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ============================================================
        // ===================== MEMBERSHIP SALES ========================
        // ============================================================

        public async Task<List<MembershipSale>> GetMembershipSalesAsync() =>
            await _http.GetFromJsonAsync<List<MembershipSale>>($"/tenant/{CompanyId}/membershipsales", JsonOptions) ?? new();

        // ============================================================
        // ===================== REPORTS =================================
        // ============================================================

        public async Task<List<MembershipSale>> GetMembershipSalesReportAsync(DateTime from, DateTime to)
        {
            // End of day for 'to' — otherwise a sale at 14:00 on the selected
            // day would be excluded, because ASP.NET Core parses "2026-06-30"
            // as midnight at the START of that day.
            var fromStr = from.Date.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var toStr = to.Date.AddDays(1).AddSeconds(-1)
                .ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            return await _http.GetFromJsonAsync<List<MembershipSale>>(
                $"/tenant/{CompanyId}/membershipsales?from={fromStr}&to={toStr}",
                JsonOptions) ?? new();
        }

        // ============================================================
        // ===================== INQUIRIES ================================
        // ============================================================

        public async Task<List<Inquiry>> GetInquiriesAsync() =>
            await _http.GetFromJsonAsync<List<Inquiry>>($"/tenant/{CompanyId}/inquiries", JsonOptions) ?? new();

        public async Task<Inquiry?> CreateInquiryAsync(Inquiry inquiry)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/inquiries", inquiry, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new System.InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid inquiry data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Inquiry>(JsonOptions);
        }

        public async Task UpdateInquiryStatusAsync(int id, RequestStatus status)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/inquiries/{id}/status", new UpdateStatusRequest { Status = status }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateInquiryAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/inquiries/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ============================================================
        // ===================== FEEDBACK ==================================
        // ============================================================

        public async Task<List<Feedback>> GetFeedbacksAsync() =>
            await _http.GetFromJsonAsync<List<Feedback>>($"/tenant/{CompanyId}/feedbacks", JsonOptions) ?? new();

        public async Task<Feedback?> CreateFeedbackAsync(Feedback feedback)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/feedbacks", feedback, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new System.InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid feedback data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Feedback>(JsonOptions);
        }

        public async Task UpdateFeedbackStatusAsync(int id, RequestStatus status)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/feedbacks/{id}/status", new UpdateStatusRequest { Status = status }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateFeedbackAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/feedbacks/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ============================================================
        // ===================== CAMPAIGNS ===============================
        // ============================================================

        public async Task<List<Campaign>> GetCampaignsAsync() =>
            await _http.GetFromJsonAsync<List<Campaign>>($"/tenant/{CompanyId}/campaigns", JsonOptions) ?? new();

        public async Task<Campaign?> CreateCampaignAsync(Campaign campaign)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/campaigns", campaign, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Campaign Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Campaign>(JsonOptions);
        }

        public async Task UpdateCampaignAsync(int id, Campaign campaign)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/campaigns/{id}", campaign, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Campaign Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCampaignStatusAsync(int id, CampaignStatus status)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/campaigns/{id}/status", new UpdateCampaignStatusRequest { Status = status }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateCampaignAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/campaigns/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ============================================================
        // ===================== PROMOTIONS ==============================
        // ============================================================

        public async Task<List<Promotion>> GetPromotionsAsync() =>
            await _http.GetFromJsonAsync<List<Promotion>>($"/tenant/{CompanyId}/promotions", JsonOptions) ?? new();

        public async Task<Promotion?> CreatePromotionAsync(Promotion promotion)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/promotions", promotion, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Promotion Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Promotion>(JsonOptions);
        }

        public async Task UpdatePromotionAsync(int id, Promotion promotion)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/promotions/{id}", promotion, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This Promotion Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivatePromotionAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/promotions/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ============================================================
        // ===================== SHARED HELPERS ============================
        // ============================================================

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

        // ============================================================
        // ===================== SALES FORCE =============================
        // ============================================================

        public async Task<MembershipSale?> CreateMembershipSaleAsync(MembershipSale sale)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/membershipsales", sale, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new System.InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid sale data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MembershipSale>(JsonOptions);
        }

        // Response shape from POST /auth/login
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

        // ============================================================
        // ===================== AUTH ====================================
        // ============================================================

        public async Task<LoginResult?> LoginAsync(string username, string password)
        {
            var response = await _http.PostAsJsonAsync("/auth/login",
                new { username, password }, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null; // invalid credentials

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LoginResult>(JsonOptions);
        }

        // ============================================================
        // ===================== PROMO CODES =============================
        // ============================================================

        public async Task<List<PromoCodeDto>> GetPromoCodesAsync() =>
            await _http.GetFromJsonAsync<List<PromoCodeDto>>($"/tenant/{CompanyId}/promocodes", JsonOptions) ?? new();

        public async Task<PromoCodeDto?> CreatePromoCodeAsync(PromoCodeDto promoCode)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/promocodes", promoCode, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This promo code already exists.");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new System.InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid promo code data."));

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PromoCodeDto>(JsonOptions);
        }

        public async Task DeactivatePromoCodeAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/promocodes/{id}");
            response.EnsureSuccessStatusCode();
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
            public CRM.domain.Entities.Promotion? Promotion { get; set; }
        }

        // ---- Staff Management ----

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


        public async Task<List<StaffDto>> GetStaffAsync() =>
    await _http.GetFromJsonAsync<List<StaffDto>>("/staff", JsonOptions) ?? new();

        public async Task<StaffDto?> CreateStaffAsync(CreateStaffRequest request)
        {
            var response = await _http.PostAsJsonAsync("/staff", request, JsonOptions);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new System.InvalidOperationException("This username is already taken.");

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                throw new System.InvalidOperationException("You do not have permission to create this role.");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new System.InvalidOperationException(await ExtractErrorMessageAsync(response, "Invalid staff data."));

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
            var response = await _http.PostAsJsonAsync($"/staff/{id}/reset-password",
                new { newPassword }, JsonOptions);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<CRM.domain.Entities.Branch>> GetBranchesAsync() =>
    await _http.GetFromJsonAsync<List<CRM.domain.Entities.Branch>>(
        $"/tenant/{CompanyId}/branches", JsonOptions) ?? new();
    }
}