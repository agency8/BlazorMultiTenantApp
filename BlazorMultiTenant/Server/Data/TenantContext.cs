namespace BlazorMultiTenant.Server.Data
{
    public class TenantContext : IdentityDbContext<ApplicationUser>
    {
        // dotnet ef migrations add InitialCreate --context TenantContext
        // dotnet ef database update --context TenantContext

        public TenantContext(DbContextOptions<TenantContext> options) : base(options) 
        { }

        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Hero> Heros => Set<Hero>();
        public DbSet<Comic> Comics => Set<Comic>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            modelBuilder.Entity<ApplicationUser>(b => { b.ToTable("Users"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(b => { b.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRole>(b => { b.ToTable("Roles"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("RoleClaims"); });
            modelBuilder.Entity<IdentityUserRole<string>>(b => { b.ToTable("UserRoles"); });

            var tenantGuid1 = Guid.NewGuid().ToString();
            var tenantGuid2 = Guid.NewGuid().ToString();
            modelBuilder.Entity<Tenant>().HasData(
                new Tenant
                {
                    Id = tenantGuid1,
                    TenantName = "Master Tenant",
                    TenantType = TenantTypes.SingleDataBase
                },
                new Tenant
                {
                    Id = tenantGuid2,
                    TenantName = "Tenant A",
                    TenantType = TenantTypes.SingleDataBase
                });


            foreach (string role in Enum.GetNames(typeof(Roles)))
            {
                var guid = Guid.NewGuid().ToString();
                modelBuilder.Entity<IdentityRole>().HasData(
                    new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        Id = guid,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    });

                if (role == "SuperUser")
                    SeedSuperUser(modelBuilder, guid, tenantGuid1);
                if (role == "Administrator")
                    SeedAdministrator(modelBuilder, guid, tenantGuid2);

            }


            modelBuilder.Entity<Comic>().HasData(
                new Comic { Id = 1, Name = "Marvel" },
                new Comic { Id = 2, Name = "DC" }
            );

            modelBuilder.Entity<Hero>().HasData(
                new Hero
                {
                    Id = 1,
                    TenantId = tenantGuid1,
                    FirstName = "Peter",
                    LastName = "Parker",
                    HeroName = "Spiderman",
                    ComicId = 1,
                },
                new Hero
                {
                    Id = 2,
                    TenantId = tenantGuid2,
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    HeroName = "Batman",
                    ComicId = 2
                },
                new Hero
                {
                    Id = 3,
                    TenantId = tenantGuid2,
                    FirstName = "Tony",
                    LastName = "Stark",
                    HeroName = "Ironman",
                    ComicId = 1
                }
            );


        } //OnModelCreating



        private void SeedSuperUser(ModelBuilder builder, string roleGuid, string tenantGuid)
        {
            var userGuid = Guid.NewGuid().ToString();
            ApplicationUser user = new ApplicationUser()
            {
                Id = userGuid,
                TenantId = tenantGuid,
                UserName = "superuser",
                NormalizedUserName = "SUPERUSER",
                Email = "super@super.com",
                NormalizedEmail = "SUPER@SUPER.COM",
            };

            user.PasswordHash = GeneratePasswordHash(user, "password123#");
            builder.Entity<ApplicationUser>().HasData(user);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = roleGuid,
                UserId = userGuid
            });
        } //SeedSuperUser

        private void SeedAdministrator(ModelBuilder builder, string roleGuid, string tenantGuid)
        {
            var userGuid = Guid.NewGuid().ToString();
            ApplicationUser user = new ApplicationUser()
            {
                Id = userGuid,
                TenantId = tenantGuid,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
            };

            user.PasswordHash = GeneratePasswordHash(user, "password123#");
            builder.Entity<ApplicationUser>().HasData(user);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = roleGuid,
                UserId = userGuid
            });
        } //SeedAdministrator

        public string GeneratePasswordHash(ApplicationUser user, string password)
        {
            var passHash = new PasswordHasher<ApplicationUser>();
            return passHash.HashPassword(user, password);
        } //GeneratePasswordHash




    }
}
