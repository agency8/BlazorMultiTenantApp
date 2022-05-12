namespace BlazorMultiTenant.Client.Pages.UserPages
{
    public partial class Register
    {
        RegisterDto user = new RegisterDto();
        string message = string.Empty;
        string messageCssClass = string.Empty;

        async Task HandleRegistration()
        {
            var result = await AuthService.Register(user);
            message = result.Message;
            if (result.Success)
                NavigationManager.NavigateTo("/login");
            else
                messageCssClass = "text-danger";
        }


    }
}
