using System.Net.Http;
using System.Net.Http.Json;
using CRM.domain.Entities;

namespace CRM.winforms
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private const int CompanyId = 1; // TenantA for now — hardcoded is fine for this exam scope

        public ApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5004")
            };
        }

        // ---- Customers ----
        public async Task<List<Customer>> GetCustomersAsync() =>
            await _http.GetFromJsonAsync<List<Customer>>($"/tenant/{CompanyId}/customers") ?? new();

        public async Task<Customer?> CreateCustomerAsync(Customer customer)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/customers", customer);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Customer Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/customers/{id}", customer);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Customer Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }


        public async Task DeleteCustomerAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/customers/{id}");
            response.EnsureSuccessStatusCode();
        }

        // ---- Membership Plans ----
        public async Task<List<MembershipPlan>> GetMembershipPlansAsync() =>
            await _http.GetFromJsonAsync<List<MembershipPlan>>($"/tenant/{CompanyId}/membershipplans") ?? new();

        public async Task<MembershipPlan?> CreateMembershipPlanAsync(MembershipPlan plan)
        {
            var response = await _http.PostAsJsonAsync($"/tenant/{CompanyId}/membershipplans", plan);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Plan Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MembershipPlan>();
        }

        public async Task UpdateMembershipPlanAsync(int id, MembershipPlan plan)
        {
            var response = await _http.PutAsJsonAsync($"/tenant/{CompanyId}/membershipplans/{id}", plan);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new InvalidOperationException("This Plan Code is already in use — please choose a different one.");

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMembershipPlanAsync(int id)
        {
            var response = await _http.DeleteAsync($"/tenant/{CompanyId}/membershipplans/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}