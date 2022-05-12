namespace BlazorMultiTenant.Server.Services.TenantManagerService
{
    public interface ITenantManagerService
    {
        Task<ServiceResponse<List<Tenant>>> GetTenantList();
        Task<ServiceResponse<List<Tenant>>> CreateTenant(Tenant tenant);
        Task<ServiceResponse<List<Tenant>>> UpdateTenant(Tenant tenant);
        Task<ServiceResponse<List<Tenant>>> DeleteTenant(string tenantId);
    }
}
