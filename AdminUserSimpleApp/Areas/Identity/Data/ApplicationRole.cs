using Microsoft.AspNetCore.Identity;

namespace AdminUserSimpleApp.Areas.Identity.Data;

public class ApplicationRole : IdentityRole<Guid>
{
    public string Description { get; set; }
}
