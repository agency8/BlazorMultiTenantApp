namespace BlazorMultiTenant.Server.Services.TenantService
{
    public interface ITenantService
    {
        public Tenant GetTenant(string tenantId);

    }
}
