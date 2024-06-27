using AdminUserSimpleApp.Areas.Identity.Data;
using AdminUserSimpleApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminUserSimpleApp.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleViewModel roleModel)
    {
        if (ModelState.IsValid)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleModel?.RoleName);
            if (roleExists)
            {
                ModelState.AddModelError("", "Role Already Exists");
            }
            else
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = roleModel?.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Role");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(roleModel);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string roleId)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return View("Error");
        }

        var model = new EditRoleViewModel
        {
            Id = role.Id,
            RoleName = role.Name,
        };
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(EditRoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
            return View("NotFound");
        }
        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            return RedirectToAction("List");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
        return View("List", await _roleManager.Roles.ToListAsync());
    }

}
