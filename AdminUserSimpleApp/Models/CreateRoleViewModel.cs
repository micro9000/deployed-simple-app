using System.ComponentModel.DataAnnotations;

namespace AdminUserSimpleApp.Models;

public class CreateRoleViewModel
{
    [Required]
    [Display(Name = "Role")]
    public string RoleName { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; }
}
