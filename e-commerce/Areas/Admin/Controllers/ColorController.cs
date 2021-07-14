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
    public class ColorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ColorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Colors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Color color)
        {
            if (!ModelState.IsValid) return View();
            if (await _db.Colors.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == color.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "This color name already exist!");
                return View();
            }

            await _db.Colors.AddAsync(color);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Color color = await _db.Colors.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Color color)
        {
            if (id == null) return NotFound();
            Color dbColor = await _db.Colors.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbColor == null) return NotFound();


            dbColor.Name = color.Name;
            List<Color> colors = _db.Colors.ToList();
            foreach (Color item in colors)
            {
                if (item.ColorCode == color.ColorCode)
                {
                    ModelState.AddModelError("", "This color code already exist!");
                    return View();
                }
            }
            dbColor.ColorCode = color.ColorCode;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Color color = await _db.Colors.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteColor(int? id)
        {
            if (id == null) return NotFound();
            Color color = await _db.Colors.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (color == null) return NotFound();


            color.IsDeleted = true;
        
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
