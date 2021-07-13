using e_commerce.Data;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrendController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BrendController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Brends.Include(b=>b.CategoryChild));
        }
        public IActionResult Create()
        {
            ViewBag.CategoryChildList = _db.CategoryChilds.Where(c => c.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brend brend, int? CategoryChildId)
        {
            if (!ModelState.IsValid) return View();
            CategoryChild category = await _db.CategoryChilds.Include(c => c.Brends).FirstOrDefaultAsync(c => c.Id == CategoryChildId);
            foreach (Brend item in category.Brends)
            {
                if (item.Name == brend.Name)
                {
                    ModelState.AddModelError("Name", "This name already exist in this top category!");
                    return View();
                }
            }

            if (CategoryChildId == null)
            {
                ModelState.AddModelError("", "Category must be selected!");
                return View();
            }

            brend.CategoryChildId = (int)CategoryChildId;

            await _db.AddAsync(brend);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Brend brend = await _db.Brends.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (brend == null) return NotFound();
            List<CategoryChild> categories = await _db.CategoryChilds.Include(c => c.Brends).Where(c => c.IsDeleted == false).ToListAsync();
            foreach (CategoryChild item in categories)
            {
                foreach (Brend item2 in item.Brends)
                {
                    if (item2.Id == id)
                    {
                        ViewBag.CategoryChild = item;
                    }
                }
            }
            return View(brend);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Brend brend)
        {
            if (id == null) return NotFound();
            Brend dbBrend = await _db.Brends.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbBrend == null) return NotFound();

            dbBrend.Name = brend.Name;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Brend brend = await _db.Brends.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (brend == null) return NotFound();
            return View(brend);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBrend(int? id)
        {
            if (id == null) return NotFound();
            Brend brend = await _db.Brends.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (brend == null) return NotFound();


            brend.IsDeleted = true;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
