using AdminUserSimpleApp.Areas.Identity.Data;
using AdminUserSimpleApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminUserSimpleApp.Controllers;
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var activeUsers = users.Where(u => !u.IsDeleted).ToList();
        var roles = await _roleManager.Roles.ToListAsync();

        Dictionary<string, List<string>> userRolesMap = new Dictionary<string, List<string>>();
        foreach(var user in activeUsers)
        {
            foreach (var role in roles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    if (userRolesMap.ContainsKey(user.Email))
                    {
                        userRolesMap[user.Email].Add(role.Name);
                    }
                    else
                    {
                        userRolesMap.Add(user.Email, new List<string>() { role.Name});
                    }
                }
            }
        }

        List<UserWithRoles> userWithRoles = new List<UserWithRoles>();
        foreach (var user in activeUsers)
        {
            if (userRolesMap.ContainsKey(user.Email))
            {
                userWithRoles.Add(new UserWithRoles
                {
                    UserInfo = user,
                    Roles = userRolesMap[user.Email].ToList()
                });
            }
            else
            {
                userWithRoles.Add(new UserWithRoles
                {
                    UserInfo = user,
                    Roles = new List<string>()
                });
            }
        }

        var viewModel = new UserListModel
        {
            UsersWithRoles = userWithRoles
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
            return View("NotFound");
        }

        user.IsDeleted = true;
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
        return RedirectToAction("Index");
    }

}
