using EmptyProject.Models;
using EmptyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(AdministrationCreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };

                var result = await this.roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }


        public ActionResult ListRoles()
        {
            var rols = this.roleManager.Roles;
            return View(rols);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound", "Please Add role Id  In the URL");
            }

            var role = await this.roleManager.FindByIdAsync(id);

            if (role is null)
            {
                return View("NotFound", $"This role  with id = {id}  not found");
            }

            EditRoleViewModel model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>(),
            };


            foreach (var user in await userManager.Users.ToListAsync())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Email);
                }
            }


            return View(model);
        }



        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleViewModel model)
        {


            var role = await this.roleManager.FindByIdAsync(model.Id);

            if (role is null)
            {
                return View("NotFound", $"This role  with id = {model.Id}  not found");
            }


            role.Name = model.RoleName;
            IdentityResult identityResult = await roleManager.UpdateAsync(role);

            if (identityResult.Succeeded)
            {
                return RedirectToAction(nameof(ListRoles));
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> EditUsersRole(string idRole)
        {
            if (string.IsNullOrEmpty(idRole))
            {
                return View("NotFound", $"This role  with id = {idRole}  not found");
            }

            var role = await this.roleManager.FindByIdAsync(idRole);

            if (role is null)
            {
                return View("NotFound", $"This role  with id = {idRole}  not found");
            }

            List<EditUsersRoleViewModel> Models = new List<EditUsersRoleViewModel>();

            foreach (var user in await userManager.Users.ToListAsync())
            {

                EditUsersRoleViewModel model = new EditUsersRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = false
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.IsSelected = true;
                }

                Models.Add(model);

            }

            ViewBag.idRole = idRole;
            return View(Models);
        }


        [HttpPost]
        public async Task<ActionResult> EditUsersRole(string idRole, List<EditUsersRoleViewModel> model)
        {
            if (string.IsNullOrEmpty(idRole))
            {
                return View("NotFound", $"This role  with id = {idRole}  not found");
            }

            var role = await this.roleManager.FindByIdAsync(idRole);

            if (role is null)
            {
                return View("NotFound", $"This role  with id = {idRole}  not found");
            }

            IdentityResult result = null;

            for (int i = 0; i < model.Count; i++)

            {
                AppUser user = await userManager.FindByIdAsync(model[i].UserId);

                if (await userManager.IsInRoleAsync(user, role.Name) && !model[i].IsSelected)
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else if (!(await userManager.IsInRoleAsync(user, role.Name)) && model[i].IsSelected)
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }

                if (result != null && !result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }



            return RedirectToAction(nameof(Edit), new { id = idRole });
        }
    }
}
