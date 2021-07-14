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
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(p=>p.Images));
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = _db.Categories.Include(c=>c.CategoryChild).ThenInclude(c=>c.Brends).Where(c => c.IsDeleted == false).ToList();
            ViewBag.Color = _db.ProductColors.ToList();
            ViewBag.SpecsList = _db.Specs.Where(c => c.IsDeleted == false).ToList();

            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products
                .Include(p=>p.Category)
                .Include(p=>p.CategoryChild).Include(p=>p.Brend)
                .Include(p=>p.Specs)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product,int? CategoryId, int? CategoryChildId,int? BrendId,int? SpecsId)
        {
            ViewBag.CategoryList = _db.Categories.Include(c => c.CategoryChild).ThenInclude(c => c.Brends).Where(c => c.IsDeleted == false).ToList();
            ViewBag.SpecsList = _db.Specs.Where(c => c.IsDeleted == false).ToList();
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
                        }
                        else
                        {
                            ModelState.AddModelError("", "The brend in this name doesn't exist in this subcategory.Please check brends list!");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The subcategory in this name doesn't exist in this upper category.Please check subcategories list!");
                    return View();
                }
            }



            await _db.Products.AddAsync(product);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.CategoryChild).Include(p => p.Brend)
                .Include(p => p.Specs)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _db.Products.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbProduct == null) return NotFound();

            dbProduct.Name = product.Name;

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
            Product dbProduct = await _db.Products.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbProduct == null) return NotFound();


            dbProduct.IsDeleted = true;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
