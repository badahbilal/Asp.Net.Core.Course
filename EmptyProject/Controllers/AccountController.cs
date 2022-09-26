using EmptyProject.Models;
using EmptyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmptyProject.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Employee2");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fullName = model.LastName.Trim().ToUpper() + "_" + model.FirstName.Trim().ToLower();
                AppUser user = new AppUser
                {
                    FristName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Email = model.Email,
                    UserName = fullName,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Employee2");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountLoginViewModel model, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var userDataBase = await userManager.FindByEmailAsync(model.Email);

                var result = await signInManager.PasswordSignInAsync(userDataBase.UserName,
                    model.Password,
                    model.Remember,
                    false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "Employee2");

                    }
                }
                ModelState.AddModelError(string.Empty, "Login Invalid Attempt.");
            }

            return View(model);
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ChekingExistingEmail(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"This Email {Email} id already taken");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditAccount(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                AppUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    EditaccountViewModel model = new EditaccountViewModel()
                    {
                        FristName = user.FristName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Id = user.Id,

                    };

                    return View(model);
                }
            }
            return RedirectToAction("index", "Employee2");
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(EditaccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Age = model.Age;
                    user.FristName = model.FristName;
                    user.LastName = model.LastName;
                    user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);

                    IdentityResult result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Employee2");
                    }

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }




    }
}