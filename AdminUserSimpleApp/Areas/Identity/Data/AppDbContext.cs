using AdminUserSimpleApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminUserSimpleApp.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        this.SeedDefaultUser(builder);
        this.SeedRoles(builder);
        this.SeedUserRoles(builder);
    }

    private void SeedDefaultUser(ModelBuilder builder)
    {
        var user = new ApplicationUser()
        {
            Id = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5"),
            UserName = "ranielgarcia@gmail.com",
            Email = "ranielgarcia@gmail.com",
            NormalizedEmail = "RANIELGARCIA@GMAIL.COM",
            NormalizedUserName = "RANIELGARCIA@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = "513163ce-890c-4bc6-bd2f-08ac63b01ecd",
            ConcurrencyStamp = "82c18512-6bd3-47e9-bacf-61839a18e160",
            FirstName = "Raniel",
            LastName = "Garcia",
            Address = "Test",
            Notes = "test",
            BirthDate = new DateOnly(1996, 05, 25),
            IsDeleted = false,
        };

        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Password@123");

        builder.Entity<ApplicationUser>().HasData(user);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole() { 
                Id = Guid.Parse("fab4fac1-c546-41de-aebc-a14da6895711"), 
                Name = "Admin", 
                Description = "Admin Role",
                ConcurrencyStamp = "8be259aa-fea6-430d-8589-8e63347b5fe9", 
                NormalizedName = "Admin" },
            new ApplicationRole() { 
                Id = Guid.Parse("c7b013f0-5201-4317-abd8-c211f91b7330"), 
                Name = "Client", 
                Description = "Client Role",
                ConcurrencyStamp = "a1974dc4-d377-400b-9125-b38f11148ab1", 
                NormalizedName = "Client" }
            );
    }

    private void SeedUserRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>() { RoleId = Guid.Parse("fab4fac1-c546-41de-aebc-a14da6895711"), 
                UserId = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5") }
            );
    }
}
