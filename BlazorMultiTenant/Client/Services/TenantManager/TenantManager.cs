namespace BlazorMultiTenant.Client.Services.TenantManager
{
    public class TenantManager : ITenantManager
    {
        private readonly HttpClient _http;
        public TenantManager(HttpClient http)
        {
            _http = http;
        }



        public List<Tenant> TenantList { get; set; } = new List<Tenant>();


        public async Task GetTenants()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Tenant>>>("api/TenantManager/GetTenants");

            if (response != null && response.Data != null)
                TenantList = response.Data;
        } //GetTenants






        public async Task CreateTenant(Tenant tenant)
        {
            var response = await _http.PostAsJsonAsync("api/TenantManager/CreateTenant", tenant);
            await GetTenants();
            //OnChange.Invoke();
        }

        public async Task UpdateTenant(Tenant tenant)
        {
            var response = await _http.PutAsJsonAsync("api/TenantManager/UpdateTenant", tenant);
            await GetTenants();
            //OnChange.Invoke();
        }

        public async Task DeleteTenant(string tenantId)
        {
            var response = await _http.DeleteAsync($"api/TenantManager/DeleteTenant/{tenantId}");
            await GetTenants();
            //OnChange.Invoke();
        }





    }
}
