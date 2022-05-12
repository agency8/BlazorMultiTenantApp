using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMultiTenant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantManagerController : ControllerBase
    {
        private readonly ITenantManagerService _tenantService;

        public TenantManagerController(ITenantManagerService tenantService)
        {
            _tenantService = tenantService;
        }


        [HttpGet("GetTenants")]
        public async Task<ActionResult<ServiceResponse<List<Tenant>>>> GetTenants()
        {
            var result = await _tenantService.GetTenantList();
            return Ok(result);
        } //GetTenants


        [HttpPost("CreateTenant")]
        public async Task<ActionResult<ServiceResponse<List<Tenant>>>> CreateTenant(Tenant tenant)
        {
            var result = await _tenantService.CreateTenant(tenant);
            return Ok(result);
        } //CreateHero


        [HttpPut("UpdateTenant")]
        public async Task<ActionResult<ServiceResponse<List<Tenant>>>> UpdateTenant(Tenant tenant)
        {
            var result = await _tenantService.UpdateTenant(tenant);
            return Ok(result);
        } //UpdateTenant



        [HttpDelete("DeleteTenant/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Tenant>>>> DeleteTenant(string tenantId)
        {
            var result = await _tenantService.DeleteTenant(tenantId);
            return Ok(result);
        } //DeleteTenant






    }
}
