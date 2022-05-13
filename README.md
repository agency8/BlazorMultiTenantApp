# Blazor 6 MultiTenant Demo
This is a rough demo on how to setup and run a hybrid Multi Tenancy using Blazor.

### Features:
- Tenancy using **a single database**
- Tenancy using **multiple databases**
- Tenancy entries are store in the database
- When a user logins in, a 'tenantid' is **encoded and stored in the JWT token**
- 2 DataBase contexts are used, 1 for tenancy and the other for site data.
- Data is **filtered in the datacontext** automatically
- Test data is created so that you can test the **tenancy functionality**.
- Login uses encoded **JWT tokens**
- **Management** of Tenancies (you are able to add/edit and delete tenancies)
- **Policy based** roles and authentication
- **Custom Database table names** (Users, Roles etc., instead of the standard AspNetUsers, AspNetRoles)

---

### Setup Instructions
The access 'Tenant management' page is ‘SuperUser’, this is needed.
Change your database connection in the ‘appsettings.json’ file (sqlConnection))
Create a secret key for JwtSecurityKey, this can be a Guid or anything you like

`"JwtSecurityKey": "ADD_A_NEW_SECURITY_KEY_HERE"`

Change the ‘JwtIssuer’ and ‘JwtAudience’ to match your site.
Using the ‘Package Manager Console’ or preferred way, create a migration:
Make sure you are in the right directory:

-	`cd blazormultitenant` 
-	`cd server`

Create a migration

-	`dotnet ef migrations add InitialCreate --context TenantContext`

Update the database

-	`dotnet ef database update --context TenantContext`

Once you have created the database via EntityFramework, you should be able to run the app and login with the pre-filled user accounts below:

**Username** : superuser
**Password**: password123#

**Username** : admin
**Password**: password123#

There is test data seeded, if you login with superuser you will see:
- 'Tenants' (to manage tenants) 
- 'Heros' (to test the tenants and filters for tenants)

If you login in as admin you will only see 'Heros'. You are able to add new heros into the app, this will save the data using the current TenantId and will not be visible to any other user, therefore the tenancy works.

When adding new users, you will have to add the tenant id manually in the User table.
