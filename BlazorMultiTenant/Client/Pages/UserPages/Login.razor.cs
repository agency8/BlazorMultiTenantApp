namespace BlazorMultiTenant.Client.Pages.UserPages
{
    public partial class Login
    {
        private LoginDto loginDto = new LoginDto();
        private bool ShowErrors;
        private string Error = "";

        private async Task HandleLogin()
        {
            ShowErrors = false;

            var result = await AuthService.Login(loginDto);

            if (result.Success)
            {
                var returnUrl = NavManager.QueryString("returnUrl") ?? "/";
                NavManager.NavigateTo(returnUrl);
            }
            else
            {
                ShowErrors = true;
                Error = result.Message;
            }
        }


    }
}
