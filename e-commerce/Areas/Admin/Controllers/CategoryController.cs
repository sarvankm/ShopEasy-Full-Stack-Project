using e_commerce.Data;
using e_commerce.Extensions;
using e_commerce.Models;
using FrontToBack.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CategoryController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Categories.OrderByDescending(p => p.Id).Include(p=>p.Products));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState["File"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("", "Please choose image !");
                return View();
            }

            if (!category.File.IsImage())
            {
                ModelState.AddModelError("File", $"This {category.File.FileName} name file format not correct !");
                return View();
            }

            if (category.File.CheckFileSize(2000))
            {
                ModelState.AddModelError("File", $" This {category.File.FileName} name file size is greater than 150 kb !");
                return View();
            }
            if (await _db.Categories.AnyAsync(c =>c.IsDeleted == false && c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "This name already exist!");
                return View();
            }
            category.Image = await category.File.SaveFileAsync(_env.WebRootPath, "img/");

            await _db.Categories.AddAsync(category);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Category category = await _db.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _db.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbCategory == null) return NotFound();

            if (ModelState["File"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("", "Please choose image !");
                return View();
            }

            if (!category.File.IsImage())
            {
                ModelState.AddModelError("File", $"In this {category.File.FileName} name file format not correct !");
                return View();
            }

            if (category.File.CheckFileSize(2000))
            {
                ModelState.AddModelError("File", $" In this {category.File.FileName} name file size is greater than 150 kb !");
                return View();
            }

            Helper.DeleteFile(_env.WebRootPath, "img", dbCategory.Image);

            dbCategory.Name = category.Name;
            dbCategory.Image = await category.File.SaveFileAsync(_env.WebRootPath, "img");

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Category category = await _db.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _db.Categories.Include(c=>c.CategoryChild).FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbCategory == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", dbCategory.Image);

            dbCategory.IsDeleted = true;
            foreach (CategoryChild item in dbCategory.CategoryChild)
            {
                item.IsDeleted = true;
            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
