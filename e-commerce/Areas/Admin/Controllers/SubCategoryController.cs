using e_commerce.Data;
using e_commerce.Models;
using FrontToBack.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public SubCategoryController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.CategoryChilds.Include(c => c.Category));
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = _db.Categories.Where(c => c.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryChild categorychild, int? MainCategoryId)
        {
            if (!ModelState.IsValid) return View();
            Category category = await _db.Categories.Include(c => c.CategoryChild).FirstOrDefaultAsync(c => c.Id == MainCategoryId);
            foreach (CategoryChild item in category.CategoryChild)
            {
                if (item.Name == categorychild.Name)
                {
                    ModelState.AddModelError("Name", "This name already exist in this top category!");
                    return View();
                }
            }

            if (MainCategoryId == null)
            {
                ModelState.AddModelError("", "Category must be selected!");
                return View();
            }

            categorychild.CategoryId = (int)MainCategoryId;

            await _db.AddAsync(categorychild);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            CategoryChild category = await _db.CategoryChilds.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (category == null) return NotFound();
            List<Category> categories = await _db.Categories.Include(c => c.CategoryChild).Where(c => c.IsDeleted == false).ToListAsync();
            foreach (Category item in categories)
            {
                foreach (CategoryChild item2 in item.CategoryChild)
                {
                    if (item2.Id == id)
                    {
                        ViewBag.TopCategory = item;
                    }
                }
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CategoryChild categoryChild)
        {
            if (id == null) return NotFound();
            CategoryChild dbCategoryChild = await _db.CategoryChilds.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbCategoryChild == null) return NotFound();

            dbCategoryChild.Name = categoryChild.Name;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            CategoryChild dbCategoryChild = await _db.CategoryChilds.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbCategoryChild == null) return NotFound();
            return View(dbCategoryChild);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) return NotFound();
            CategoryChild dbCategoryChild = await _db.CategoryChilds.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbCategoryChild == null) return NotFound();


            dbCategoryChild.IsDeleted = true;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}