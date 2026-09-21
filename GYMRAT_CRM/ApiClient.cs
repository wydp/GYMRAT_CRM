using CRM.domain.Entities;
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
    }
}