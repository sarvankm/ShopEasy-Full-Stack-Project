using e_commerce.Data;
using e_commerce.Extensions;
using e_commerce.Models;
using FrontToBack.Helpers;
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
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ImageController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Images.Include(p=>p.Product));
        }
        public IActionResult Create()
        {
            ViewBag.ProductList = _db.Products.Where(c => c.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Image image,int? ProductId)
        {
            if (ModelState["File"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("", "Please choose image !");
                return View();
            }

            if (!image.File.IsImage())
            {
                ModelState.AddModelError("File", $"This {image.File.FileName} name file format not correct !");
                return View();
            }

            if (image.File.CheckFileSize(2000))
            {
                ModelState.AddModelError("File", $" This {image.File.FileName} name file size is greater than 150 kb !");
                return View();
            }
            Product product = await _db.Products.Include(p=>p.Images).FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == ProductId);
            foreach (Image item in product.Images)
            {
                if (item.ImageName == image.ImageName)
                {
                    ModelState.AddModelError("", "This image already exist in this product!");
                    return View();
                }
            }
            image.ImageName = await image.File.SaveFileAsync(_env.WebRootPath, "img/");
            image.ProductId = (int)ProductId;

            await _db.Images.AddAsync(image);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Image img = await _db.Images.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (img == null) return NotFound();
            List<Product> products = await _db.Products.Include(c => c.Images).Where(c => c.IsDeleted == false).ToListAsync();
            foreach (Product item in products)
            {
                foreach (Image item2 in item.Images)
                {
                    if (item2.Id == id)
                    {
                        ViewBag.Product = item;
                    }
                }
            }
            return View(img);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Image image)
        {
            if (id == null) return NotFound();
            Image dbImage = await _db.Images.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbImage == null) return NotFound();

            if (ModelState["File"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("", "Please choose image !");
                return View();
            }

            if (!image.File.IsImage())
            {
                ModelState.AddModelError("File", $"In this {image.File.FileName} name file format not correct !");
                return View();
            }

            if (image.File.CheckFileSize(2000))
            {
                ModelState.AddModelError("File", $" In this {image.File.FileName} name file size is greater than 150 kb !");
                return View();
            }

            Helper.DeleteFile(_env.WebRootPath, "img", dbImage.ImageName);

            dbImage.ImageName = await image.File.SaveFileAsync(_env.WebRootPath, "img");

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Image dbImage = await _db.Images.Include(i=>i.Product).FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbImage == null) return NotFound();
            return View(dbImage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return NotFound();
            Image dbImage = await _db.Images.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbImage == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", dbImage.ImageName);


            dbImage.IsDeleted = true;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
