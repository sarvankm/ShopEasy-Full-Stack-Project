using e_commerce.Data;
using e_commerce.Extensions;
using e_commerce.Models;
using FrontToBack.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = "Admin,Moderator")]

    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Products.OrderByDescending(p => p.Id).Include(p=>p.Images));
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = _db.Categories.Include(c=>c.CategoryChild).ThenInclude(c=>c.Brends).Where(c => c.IsDeleted == false).ToList();
            ViewBag.Color = _db.Colors.ToList();
            ViewBag.SpecsList = _db.Specs.Where(c => c.IsDeleted == false).ToList();
            ViewBag.Image = _db.Images.FirstOrDefault(c => c.IsDeleted == false);

            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products
                .Include(p=>p.Category)
                .Include(p=>p.CategoryChild).Include(p=>p.Brend)
                .Include(p=>p.Specs)
                .Include(p=>p.ProductColors).ThenInclude(c=>c.Color)
                .Include(p => p.Images.Where(c=>c.IsDeleted == false))
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product,int? CategoryId, int? CategoryChildId,int? BrendId,int? SpecsId,List<int> ids,IFormFile[] Files)
        {
            ViewBag.CategoryList = _db.Categories.Include(c => c.CategoryChild).ThenInclude(c => c.Brends).Where(c => c.IsDeleted == false).ToList();
            ViewBag.Color = _db.Colors.ToList();
            ViewBag.SpecsList = _db.Specs.Where(c => c.IsDeleted == false).ToList();
            ViewBag.Image = _db.Images.FirstOrDefault(c => c.IsDeleted == false);

            if (ids.Count() != Files.Length)
            {
                ModelState.AddModelError("Files", $"You must import {ids.Count()} picture!");
                return View();
            }
     

            if (!ModelState.IsValid) return View();
            if (CategoryChildId == null)
            {
                ModelState.AddModelError("", "Category must be selected!");
                return View();
            }
            Category category = await _db.Categories.Include(c => c.CategoryChild).ThenInclude(c=>c.Brends).FirstOrDefaultAsync(c => c.Id == CategoryId);
            if (category == null) return NotFound();
            foreach (CategoryChild item in category.CategoryChild)
            {
                if (item.Id == CategoryChildId)
                {
                    foreach (var item2 in item.Brends)
                    {
                        if (item2.Id == BrendId)
                        {
                            product.CategoryId = (int)CategoryId;
                            product.CategoryChildId = (int)CategoryChildId;
                            product.BrendId = (int)BrendId;
                            break;
                        }
                       
                    }
                    if (product.BrendId == null)
                    {
                        ModelState.AddModelError("", "The brend in this name doesn't exist in this subcategory.Please check brends list!");
                        return View();
                    }
                    break;
                }
              
            }
            if(product.CategoryChildId == 0)
            {
                ModelState.AddModelError("", "The subcategory in this name doesn't exist in this upper category.Please check subcategories list!");
                return View();
            }
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();


            for (int i = 0; i < ids.Count(); i++)
            {
                ProductColor productColor = new ProductColor
                {
                    ColorId = ids[i],
                    ProductId = product.Id
                };
               await _db.ProductColors.AddAsync(productColor);
            }

            foreach (IFormFile file in Files)
            {
                if (ModelState["Files"].ValidationState == ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("", "Please choose image !");
                    return View();
                }

                if (!file.IsImage())
                {
                    ModelState.AddModelError("Files", $"This {file.FileName} name file format not correct !");
                    return View();
                }

                if (file.CheckFileSize(3000))
                {
                    ModelState.AddModelError("Files", $" This {file.FileName} name file size is greater than 150 kb !");
                    return View();
                }
                Image image = new Image
                {
                    ImageName = await file.SaveFileAsync(_env.WebRootPath, "img/")
                };
                image.ProductId = (int)product.Id;

                await _db.Images.AddAsync(image);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products
                .Include(p => p.Category)
                .Include(p=>p.Images)
                .Include(p => p.CategoryChild).Include(p => p.Brend)
                .Include(p=>p.ProductColors).ThenInclude(c=>c.Color)
                .Include(p => p.Specs)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            ViewBag.Color = _db.ProductColors.Where(c=>c.ProductId == id).ToList();
            ViewBag.CategoryList = _db.Categories.Include(c=>c.CategoryChild).ThenInclude(c=>c.Brends).Where(c => c.IsDeleted == false).ToList();
            ViewBag.SpecsList = _db.Specs.Where(c => c.IsDeleted == false).ToList();
            ViewBag.Image = _db.Images.FirstOrDefault(c => c.IsDeleted == false);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product, int CategoryId, int CategoryChildId, int BrendId, int? SpecsId, IFormFile[] Files)
        {
            ViewBag.Color = _db.ProductColors.Where(c => c.ProductId == id).ToList();
            ViewBag.CategoryList = _db.Categories.Include(c => c.CategoryChild).ThenInclude(c => c.Brends).Where(c => c.IsDeleted == false).ToList();
            ViewBag.SpecsList = _db.Specs.Where(c => c.IsDeleted == false).ToList();
            ViewBag.Image = _db.Images.FirstOrDefault(c => c.IsDeleted == false);

            if (_db.ProductColors.Where(c=>c.ProductId==id).Count() != Files.Length)
            {
                ModelState.AddModelError("Files", $"You must import {_db.ProductColors.Where(c => c.ProductId == id).Count()} picture!");
                return View();
            }
            if (id == null) return NotFound();
            Product dbProduct = await _db.Products.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbProduct == null) return NotFound();

            foreach (IFormFile file in Files)
            {
                if (ModelState["Files"].ValidationState == ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("", "Please choose image !");
                    return View();
                }

                if (!file.IsImage())
                {
                    ModelState.AddModelError("Files", $"This {file.FileName} name file format not correct !");
                    return View();
                }

                if (file.CheckFileSize(3000))
                {
                    ModelState.AddModelError("Files", $" This {file.FileName} name file size is greater than 150 kb !");
                    return View();
                }
                List<Image> dbImage = await _db.Images.Where(p => p.ProductId == id).ToListAsync();
                if (dbImage != null)
                {
                    foreach (var item in dbImage)
                    {
                        Helper.DeleteFile(_env.WebRootPath, "img", item.ImageName);
                        item.IsDeleted = true;
                    }
                }
              
                Image image = new Image
                {
                    ImageName = await file.SaveFileAsync(_env.WebRootPath, "img/")
                };
                image.ProductId = (int)id;

                await _db.Images.AddAsync(image);
            }

            dbProduct.Name = product.Name;
            dbProduct.CategoryId = CategoryId;
            dbProduct.CategoryChildId = CategoryChildId;
            dbProduct.BrendId = BrendId;
            dbProduct.SpecsId = SpecsId;


            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.CategoryChild).Include(p => p.Brend)
                .Include(p => p.Specs)
                .Include(p=>p.Images).FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbProduct == null) return NotFound();
            return View(dbProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _db.Products.Include(p=>p.Images).Include(p=>p.ProductColors).FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbProduct == null) return NotFound();


            dbProduct.IsDeleted = true;
            foreach (var item in dbProduct.Images)
            {
                Helper.DeleteFile(_env.WebRootPath, "img", item.ImageName);
                item.IsDeleted = true;
            }
            foreach (var item in dbProduct.ProductColors)
            {
                item.IsDeleted = true;
            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
