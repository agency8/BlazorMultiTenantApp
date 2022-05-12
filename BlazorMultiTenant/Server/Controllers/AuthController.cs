using Microsoft.AspNetCore.Mvc;

namespace BlazorMultiTenant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        } //AuthController


        [HttpPost("Login")]
        public async Task<ServiceResponse<string>> Login([FromBody] LoginDto request)
        {
            var result = await _authService.UserLogin(request);
            return result;
        } //Login



        [HttpPost("Register")]
        public async Task<ServiceResponse<int>> Post([FromBody] RegisterDto request)
        {
            var result = await _authService.UserRegister(request);
            return result;
        } //Register
    }
}
