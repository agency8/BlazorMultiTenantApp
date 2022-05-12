namespace BlazorMultiTenant.Client.Services.TenantManager
{
    public interface ITenantManager
    {
        List<Tenant> TenantList { get; set; }
        Task GetTenants();
        Task CreateTenant(Tenant tenant);
        Task UpdateTenant(Tenant tenant);
        Task DeleteTenant(string tenantId);
    }
}
