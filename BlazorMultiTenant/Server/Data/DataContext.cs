using System.Data.Common;
using Microsoft.EntityFrameworkCore;


namespace BlazorMultiTenant.Server.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {

        // dotnet ef migrations add InitialCreate --context DataContext
        // dotnet ef database update --context DataContext

        private readonly string _tenant = string.Empty;
        protected string CurrectTenantId => _httpContextAccessor.HttpContext.User.FindFirstValue("tenantid");
        protected bool IsSuperUser => _httpContextAccessor.HttpContext.User.IsInRole("SuperUser");
        public Tenant Tenant { get; set; }
        public string TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantDBType { get; set; }
        public string TenantConnectionString { get; set; }

        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DataContext(DbContextOptions<DataContext> options, 
            ITenantService tenantService, 
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
            SetTenant();
        }


        private void SetTenant()
        {
            try {
                //var TenantId = _httpContextAccessor.HttpContext.User.FindFirstValue("tenantid");
                var Tenant = _tenantService.GetTenant(CurrectTenantId);
                TenantId = Tenant.Id;
                TenantName = Tenant.TenantName;
                TenantDBType = Tenant.TenantType.ToString();
                TenantConnectionString = Tenant.DBConnectionString;
            }
            catch(Exception ex) {
                Console.WriteLine("************************************** Error : " + ex.Message);
            }

        }

        /*private void SetTenant(string tenantId)
        {
            var _context = _httpContextAccessor.HttpContext;
            var tid = _context.User.FindFirstValue("tenantid");
            //Console.WriteLine("************************************** entry : " + tid);
            var tenantData = _tenantService.GetTenant(tenantId);
            if (tenantData != null)
            {
                TenantId = tenantData.Id;
                TenantName = tenantData.TenantName;
                TenantType = tenantData.TenantType.ToString();
                if (TenantType.ToLower() != "singledatabase")
                    TenantDbConnection = tenantData.DBConnectionString;
                }
            else 
            {
                    throw new Exception("Invalid Tenant! (from SetTenant)");
             }
        }*/


        /*private void SetTenant()
        {
            var _context = _httpContextAccessor.HttpContext;
            if (_context != null)
            {
                //Pass over the httpContext
                //var headerTenantId = _context.User.FindFirstValue("tenantid");
                //Console.WriteLine("**************************************");
                //Console.WriteLine("***************** : " + headerTenantId);
                //Console.WriteLine("**************************************");
                //TenantId = headerTenantId;
                //Console.WriteLine("**************************************");
                //Console.WriteLine("***************** : TenantId: " + TenantId);
                //Console.WriteLine("**************************************");
                //SetTenant(headerTenantId);
                //Console.WriteLine("**************************************");
                //Console.WriteLine("***************** : TenantId: " + TenantId);
                //Console.WriteLine("**************************************");



                //if (string.IsNullOrEmpty(TenantId))
                //{ throw new Exception("Invalid Tenant! (from SetTenant)"); }

                //modelBuilder.Entity<Hero>().HasQueryFilter(t => t.TenantId == TenantId);
                //}                
            }
            else
            {
                //throw new Exception("2. Invalid Tenant! [" + TenantId + "]");
            }



            //var _context = _httpContextAccessor.HttpContext;
            //var headerTenantId = _context.User.FindFirstValue("tenantid");
            //TenantId = headerTenantId;
            //if (string.IsNullOrEmpty(TenantId))
            //{ throw new Exception("Invalid Tenant! (from SetTenant)"); }







            //var tenantData = _tenantService.GetTenant();
            //if (tenantData != null)
            //{
            //TenantId = tenantData.Id;
            // TenantName = tenantData.TenantName;
            //TenantType = tenantData.TenantType.ToString();
            //if (TenantType.ToLower() != "singledatabase")
            //TenantDbConnection = tenantData.DBConnectionString;
            //}
            //else {
            //throw new Exception("Invalid Tenant! (from SetTenant)");
            // }

        }*/


       


        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Hero> Heros => Set<Hero>();
        public DbSet<Comic> Comics => Set<Comic>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //if(!IsSuperUser){
                modelBuilder.Entity<Hero>().HasQueryFilter(t => t.TenantId == $"{TenantId}");
            //}
            

            modelBuilder.Entity<ApplicationUser>(b => { b.ToTable("Users"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(b => { b.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRole>(b => { b.ToTable("Roles"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("RoleClaims"); });
            modelBuilder.Entity<IdentityUserRole<string>>(b => { b.ToTable("UserRoles"); });

            

        } //OnModelCreating


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (TenantDBType != "SingleDataBase") 
            {
                if (TenantConnectionString != string.Empty && TenantConnectionString != null)
                {
                    optionsBuilder.UseSqlServer(TenantConnectionString);
                }
            }           
        } //OnConfiguring


    }
}