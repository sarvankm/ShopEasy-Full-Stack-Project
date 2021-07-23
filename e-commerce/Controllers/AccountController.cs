using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(RegisterVM register)
        {
            ViewBag.Val = TempData["Validation"];
            AccountVM accountVM = new AccountVM { RegisterVM = register };
            return View(accountVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return RedirectToAction("Login",new AccountVM { RegisterVM = registerVM });

            User user = new User
            {
                UserName = registerVM.UserName,
                FullName = registerVM.FullName,
                Email = registerVM.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                
                foreach (var error in identityResult.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                    {
                        TempData["Validation"] = "Daxil etdiyiniz istifadəçi adı mövcuddur.";
                        break;
                    }
                    if (error.Code == "PasswordRequiresUpper")
                    {
                        TempData["Validation"] = "Parolda ən az 1 böyük hərf olmalıdır.";
                        break;
                    }
                    if (error.Code  == "PasswordTooShort")
                    {
                        TempData["Validation"] = "Parolda ən az 8 xarakter olmalıdır.";
                        break;
                    }
                    if (error.Code == "PasswordRequiresNonAlphanumeric")
                    {
                        TempData["Validation"] = "Parolda ən az 1 simvol olmalıdır.";
                        break;
                    }
                    
                }
                RegisterVM register = new RegisterVM
                {
                    FullName = registerVM.FullName,
                    UserName = registerVM.UserName,
                    Email = registerVM.Email
                };
                return RedirectToAction("Login", register);
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, true);


            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(new AccountVM { LoginVM = loginVM });
            User user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email və ya şifrə yanlışdır!");
                return View(new AccountVM {LoginVM=loginVM});
            }

            if (user.IsDeleted)
            {
                ModelState.AddModelError("", "Hesabınız bloklandı!");
                return View(new AccountVM { LoginVM = loginVM });
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Hesabınız kilidlənib!");
                return View(new AccountVM { LoginVM = loginVM });
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email və ya şifrə yanlışdır!");
                return View(new AccountVM { LoginVM = loginVM });
            }

            var role = (await _userManager.GetRolesAsync(user))[0];
            if (role == "Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            if (role == "Moderator")
            {
                return RedirectToAction("Index", "ContactForm", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #region Create Role
        public async Task CreateRole()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            if (!await _roleManager.RoleExistsAsync("Moderator"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        }
        #endregion
    }
}
