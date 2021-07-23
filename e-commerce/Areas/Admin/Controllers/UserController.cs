using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace e_commerce.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        public UserController(UserManager<User> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index(int? page)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_userManager.Users.Count() / 8);

            List<User> Users = _userManager.Users.ToList();
            List<UserVM> usersVMs = new List<UserVM>();

            foreach (User user in Users)
            {
                UserVM userVM = new UserVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName,
                    IsDeleted = user.IsDeleted,
                    Role = (await _userManager.GetRolesAsync(user))[0]
                };

                usersVMs.Add(userVM);
            }

            if (page == null)
            {
                ViewBag.Page = 1;
                return View(usersVMs.OrderByDescending(p => p.Id).Take(8).ToList());
            }
            else
            {
                ViewBag.Page = page;
                return View(usersVMs.OrderByDescending(p => p.Id).Skip(((int)page - 1) * 8).Take(8).ToList());
            }
        }

        public async Task<IActionResult> IsActive(string id)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            UserVM userVM = new UserVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                Role = (await _userManager.GetRolesAsync(user))[0],
                IsDeleted = user.IsDeleted
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsActive(string id, bool IsActivated)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsDeleted = IsActivated;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeRole(string id)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            UserVM userVM = new UserVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                Role = (await _userManager.GetRolesAsync(user))[0],
                IsDeleted = user.IsDeleted,
                Roles = new List<string> { "Admin", "Moderator", "User" }
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, string Role)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            string oldRole = (await _userManager.GetRolesAsync(user))[0];
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            await _userManager.AddToRoleAsync(user, Role);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            UserVM userVM = new UserVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                Role = (await _userManager.GetRolesAsync(user))[0],
                IsDeleted = user.IsDeleted
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, string NewPassword)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, NewPassword);
            return RedirectToAction(nameof(Index));
        }
    }
}
