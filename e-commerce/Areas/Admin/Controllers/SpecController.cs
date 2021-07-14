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
    public class SpecController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SpecController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Specs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Specs specs)
        {
            if (await _db.Specs.AnyAsync(c => c.IsDeleted == false &&
            c.ProducerValue.ToLower() == specs.ProducerValue.ToLower() && 
            c.ProductionYearValue.ToLower() == specs.ProductionYearValue.ToLower() &&
            c.TypeValue==specs.TypeValue && c.OSValue==specs.OSValue
            ))
            {
                ModelState.AddModelError("", "This specialities already exist!");
                return View();
            }

            await _db.Specs.AddAsync(specs);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Specs specs = await _db.Specs.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (specs == null) return NotFound();
            return View(specs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Specs specs)
        {
            if (id == null) return NotFound();
            Specs dbSpecs = await _db.Specs.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbSpecs == null) return NotFound();


            dbSpecs.ProducerValue = specs.ProducerValue;
            dbSpecs.ProductionYearValue = specs.ProductionYearValue;
            dbSpecs.TypeValue = specs.TypeValue;
            dbSpecs.OSValue = specs.OSValue;


            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Specs specs = await _db.Specs.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (specs == null) return NotFound();
            return View(specs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSpecs(int? id)
        {
            if (id == null) return NotFound();
            Specs dbSpecs = await _db.Specs.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbSpecs == null) return NotFound();


            dbSpecs.IsDeleted = true;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
