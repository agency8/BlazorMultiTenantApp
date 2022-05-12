using Microsoft.AspNetCore.Components;


namespace BlazorMultiTenant.Client.Pages.Admin
{
    public partial class TenantManager
    {
        [Inject] ITenantManager TenantService { get; set; }
        private bool isEditing = false;
        Tenant tenant = new Tenant();
        bool DbStringVisible = false;

        protected override async Task OnInitializedAsync()
        {
            await TenantService.GetTenants();
        }


        private void CreateNewTenant()
        {
            isEditing = true;
        }


        private void EditTenant(Tenant editingTenant)
        {
            tenant = editingTenant;
            if (tenant.TenantType.ToString().ToLower() != "singledatabase")
                DbStringVisible = true;

            isEditing = true;
        }


        private async void DeleteTenant(string id)
        {
            await TenantService.DeleteTenant(id);
            isEditing = false;
        }


        private async Task CancelEditing()
        {
            isEditing = false;
            tenant = new Tenant();
            await TenantService.GetTenants();
        }


        private async Task HandleSaveTenant()
        {
            if (!string.IsNullOrEmpty(tenant.Id))
            {
                await TenantService.UpdateTenant(tenant);
            }
            else
            {
                await TenantService.CreateTenant(tenant);
            }
            tenant = new Tenant();
            isEditing = false;
        }


        public void TypeChanged(ChangeEventArgs e)
        {
            if (e.Value.ToString().ToLower() == "singledatabase")
                DbStringVisible = false;
            else
                DbStringVisible = true;
        }


    }
}
