using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace AdminUserSimpleApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }
    public DateOnly BirthDate { get; set; }

    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
}

