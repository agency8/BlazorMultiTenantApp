namespace BlazorMultiTenant.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> UserLogin(LoginDto user);
        Task<ServiceResponse<int>> UserRegister(RegisterDto user);
        string GetUserId();
    }
}
