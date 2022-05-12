namespace BlazorMultiTenant.Server.Services.TenantManagerService
{
    public class TenantManagerService : ITenantManagerService
    {
        private readonly DataContext _context;

        public TenantManagerService(DataContext context)
        {
            _context = context;
        }

        private async Task<Tenant> GetTenantById(string id)
        {
            return await _context.Tenants.FindAsync(id);
        } //GetTenantById







        public async Task<ServiceResponse<List<Tenant>>> GetTenantList()
        {
            var response = new ServiceResponse<List<Tenant>>
            {
                Data = await _context.Tenants.ToListAsync()
            };
            return response;
        } //GetTenantList


        public async Task<ServiceResponse<List<Tenant>>> CreateTenant(Tenant tenant)
        {
            tenant.Id = Guid.NewGuid().ToString();
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return await GetTenantList();
        } //CreateTenant


        public async Task<ServiceResponse<List<Tenant>>> UpdateTenant(Tenant tenant)
        {
            var foundTenant = await GetTenantById(tenant.Id);
            if (foundTenant == null)
                return new ServiceResponse<List<Tenant>>
                {
                    Success = false,
                    Message = "Tenant not found."
                };

            foundTenant.TenantName = tenant.TenantName;
            foundTenant.TenantType = tenant.TenantType;
            if (foundTenant.TenantType.ToString().ToLower() != "singledatabase")
                foundTenant.DBConnectionString = tenant.DBConnectionString;
            else
                foundTenant.DBConnectionString = string.Empty;

            await _context.SaveChangesAsync();
            return await GetTenantList();
        } //UpdateTenant


        public async Task<ServiceResponse<List<Tenant>>> DeleteTenant(string tenantId)
        {
            var foundTenant = await GetTenantById(tenantId);
            if (foundTenant == null)
            {
                return new ServiceResponse<List<Tenant>>
                {
                    Success = false,
                    Message = "Tenant not found."
                };
            }
            else
            {
                _context.Tenants.Remove(foundTenant);
                await _context.SaveChangesAsync();
            }

            return await GetTenantList();
        } //DeleteTenant

    }
}
