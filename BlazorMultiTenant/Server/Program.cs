global using BlazorMultiTenant.Server.Services.AuthService;
global using BlazorMultiTenant.Server.Services.HeroService;
global using BlazorMultiTenant.Server.Services.TenantService;
global using BlazorMultiTenant.Server.Services.TenantManagerService;
global using BlazorMultiTenant.Server.Data;
global using BlazorMultiTenant.Shared.Models;
global using BlazorMultiTenant.Shared.Dtos;
global using BlazorMultiTenant.Shared.Enums;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using System.Security.Claims;


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlazorMultiTenant.Shared.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
string jwtIssuer = builder.Configuration["JwtIssuer"];
string jwtAudience = builder.Configuration["JwtAudience"];
string jwtSecurityKey = builder.Configuration["JwtSecurityKey"];



builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString)); 
builder.Services.AddDbContext<TenantContext>(options => options.UseSqlServer(connectionString)); 

builder.Services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtIssuer,
				ValidAudience = jwtAudience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey)),
				ClockSkew = TimeSpan.FromSeconds(0),
			};
		});



builder.Services.AddAuthorization(config =>
{
	config.AddPolicy(Policies.IsSuperUser, Policies.IsSuperUserPolicy());
	config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
	config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
});

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ITenantManagerService, TenantManagerService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHeroService, HeroService>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();