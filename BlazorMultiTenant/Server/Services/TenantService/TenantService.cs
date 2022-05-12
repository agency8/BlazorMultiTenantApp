namespace BlazorMultiTenant.Server.Services.TenantService
{

    public class TenantService : ITenantService
    {
        private readonly TenantContext _tenantContext;

        public TenantService(TenantContext tenantContext)
        {
            _tenantContext = tenantContext;
        }


        public Tenant GetTenant(string myTenantId)
        {
            Tenant tenant = null;
            if (!string.IsNullOrEmpty(myTenantId))
                tenant = _tenantContext.Tenants.FirstOrDefault(t => t.Id == $"{myTenantId}");
            
            return tenant;
        }
     
        
    }
}
