using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            User user = new User
            {
                UserName = register.UserName,
                FullName = register.FullName,
                Email = register.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            User user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email Or Password Is Wrong!");
                return View(loginVM);
            }

            if (user.IsDeleted)
            {
                ModelState.AddModelError("", "Your Account Is Blocked!");
                return View(loginVM);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RemindMe, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account Is Lock Out!");
                return View(loginVM);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Or Password Is Wrong!");
                return View(loginVM);
            }

            var role = (await _userManager.GetRolesAsync(user))[0];
            if (role == "Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            if (role == "Teacher")
            {
                return RedirectToAction("Index", "Course", new { area = "Admin" });
            }
            if (role == "Speaker")
            {
                return RedirectToAction("Index", "Event", new { area = "Admin" });
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
            if (!await _roleManager.RoleExistsAsync("Teacher"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Teacher" });
            if (!await _roleManager.RoleExistsAsync("Speaker"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Speaker" });
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        }
        #endregion
    }
}
