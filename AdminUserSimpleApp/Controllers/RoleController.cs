﻿using AdminUserSimpleApp.Areas.Identity.Data;
using AdminUserSimpleApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminUserSimpleApp.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
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
                ApplicationRole identityRole = new ApplicationRole
                {
                    Name = roleModel?.RoleName,
                    Description = roleModel?.Description
                };
                var result = await _roleManager.CreateAsync(identityRole);
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
        List<ApplicationRole> roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string roleId)
    {
        ApplicationRole role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return View("Error");
        }

        var model = new EditRoleViewModel
        {
            Id = role.Id.ToString(),
            RoleName = role.Name,
            Description = role.Description,
            Users = new List<string>()
        };

        foreach(var user in _userManager.Users.ToList())
        {
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                model.Users.Add(user.UserName);
            }
        }

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
                role.Description = model.Description;

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


    [HttpGet]
    public async Task<IActionResult> EditUsersInRole(string roleId)
    {
        ViewBag.roleId = roleId;
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
            return View("NotFound");
        }
        ViewBag.RollName = role.Name;
        var model = new List<UserRoleViewModel>();
        foreach (var user in _userManager.Users.ToList())
        {
            var userRoleViewModel = new UserRoleViewModel
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName
            };
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                userRoleViewModel.IsSelected = true;
            }
            else
            {
                userRoleViewModel.IsSelected = false;
            }
            model.Add(userRoleViewModel);
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
            return View("NotFound");
        }
        for (int i = 0; i < model.Count; i++)
        {
            var user = await _userManager.FindByIdAsync(model[i].UserId);
            IdentityResult? result;
            if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
            {
                result = await _userManager.AddToRoleAsync(user, role.Name);
            }
            else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
            {
                result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
            else
            {
                continue;
            }

            if (result.Succeeded)
            {
                if (i < (model.Count - 1))
                    continue;
                else
                    return RedirectToAction("Edit", new { roleId = roleId });
            }
        }
        return RedirectToAction("Edit", new { roleId = roleId });
    }

}
