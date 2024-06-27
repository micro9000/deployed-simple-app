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
        ApplicationUser user = new ApplicationUser()
        {
            Id = Guid.Parse("b74ddd14-6340-4840-95c2-db12554843e5"),
            UserName = "Admin",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            LockoutEnabled = false,
            PhoneNumber = "1234567890"
        };

        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        var hashedPassword = passwordHasher.HashPassword(user, "Admin*123");
        user.PasswordHash = hashedPassword;

        builder.Entity<ApplicationUser>().HasData(user);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole() { 
                Id = Guid.Parse("fab4fac1-c546-41de-aebc-a14da6895711"), 
                Name = "Admin", 
                ConcurrencyStamp = "1", 
                NormalizedName = "Admin" },
            new ApplicationRole() { 
                Id = Guid.Parse("c7b013f0-5201-4317-abd8-c211f91b7330"), 
                Name = "Client", 
                ConcurrencyStamp = "2", 
                NormalizedName = "Client" }
            );
    }

    private void SeedUserRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
            );
    }
}
