using Microsoft.AspNetCore.Authorization;

namespace BlazorMultiTenant.Shared.Helpers
{
    public static class Policies
    {
		public const string IsSuperUser = "IsSuperUser";
		public const string IsAdmin = "IsAdmin";
		public const string IsUser = "IsUser";


		public static AuthorizationPolicy IsSuperUserPolicy()
		{
			return new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.RequireRole("SuperUser")
				.Build();
		}

		public static AuthorizationPolicy IsAdminPolicy()
		{
			return new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.RequireRole("SuperUser", "Administrator")
				.Build();
		}

		public static AuthorizationPolicy IsUserPolicy()
		{
			return new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
					.RequireRole("User", "Registered")
					.Build();
		}
	}
}
