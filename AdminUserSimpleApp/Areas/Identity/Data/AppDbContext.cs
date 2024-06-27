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
            UserName = "admin@2025",
            Email = "admin123@gmail.com",
            NormalizedEmail = "ADMIN123@GMAIL.COM",
            NormalizedUserName = "ADMIN@2025",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = "513163ce-890c-4bc6-bd2f-08ac63b01ecd",
            ConcurrencyStamp = "82c18512-6bd3-47e9-bacf-61839a18e160",
            PhoneNumber = "09979022241",
            FirstName = "Raniel",
            LastName = "Garcia",
            Address = "Test",
            Notes = "test",
            BirthDate = new DateOnly(1996, 05, 25)
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
                Description = "Admin Role",
                ConcurrencyStamp = "1", 
                NormalizedName = "Admin" },
            new ApplicationRole() { 
                Id = Guid.Parse("c7b013f0-5201-4317-abd8-c211f91b7330"), 
                Name = "Client", 
                Description = "Client Role",
                ConcurrencyStamp = "2", 
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
