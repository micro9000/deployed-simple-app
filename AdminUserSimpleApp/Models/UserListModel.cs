using AdminUserSimpleApp.Areas.Identity.Data;

namespace AdminUserSimpleApp.Models;

public class UserWithRoles
{
    public ApplicationUser UserInfo { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}

public class UserListModel
{
    public IEnumerable<UserWithRoles> UsersWithRoles { get; set; }
}
