using e_commerce.Data;
using e_commerce.Models;
using e_commerce.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using FrontToBack.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public SliderController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Sliders.OrderByDescending(p => p.Id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["File"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("", "Please choose image !");
                return View();
            }

            if (!slider.File.IsImage())
            {
                ModelState.AddModelError("File", $"This {slider.File.FileName} name file format not correct !");
                return View();
            }

            if (slider.File.CheckFileSize(2000))
            {
                ModelState.AddModelError("File", $" This {slider.File.FileName} name file size is greater than 150 kb !");
                return View();
            }

            slider.Image = await slider.File.SaveFileAsync(_env.WebRootPath, "img");

            await _db.Sliders.AddAsync(slider);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbSlider == null) return NotFound();
            if (ModelState["File"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("", "Please choose image !");
                return View();
            }

            if (!slider.File.IsImage())
            {
                ModelState.AddModelError("File", $"In this {slider.File.FileName} name file format not correct !");
                return View();
            }

            if (slider.File.CheckFileSize(2000))
            {
                ModelState.AddModelError("File", $" In this {slider.File.FileName} name file size is greater than 150 kb !");
                return View();
            }


            Helper.DeleteFile(_env.WebRootPath, "img", dbSlider.Image);
            if (slider.IsActive==true)
            {
              Slider activeSlider=await _db.Sliders.FirstOrDefaultAsync(p=>p.IsActive == true);
                activeSlider.IsActive = false;
                dbSlider.IsActive = true;
            }

            dbSlider.Url = slider.Url;
            dbSlider.Image = await slider.File.SaveFileAsync(_env.WebRootPath, "img");

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbSlider == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", dbSlider.Image);

            dbSlider.IsDeleted = true;
            if (dbSlider.IsActive==true)
            {
                dbSlider.IsActive = false;
                int slider = _db.Sliders.ToList().IndexOf(dbSlider);
                _db.Sliders.ToList()[slider + 1].IsActive = true;
            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
