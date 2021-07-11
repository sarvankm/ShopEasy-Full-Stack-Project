using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;

        }
        public async Task<IActionResult> Index(int? id)
        {

            return View(new ProductVM
            {
                Specs = _db.Specs.FirstOrDefault(p=>p.Products.FirstOrDefault(p=>p.Id==id).SpecsId==p.Id),
                ProductColors = _db.ProductColors.Where(p=>p.ProductId==id).Include(p=>p.Product).ThenInclude(p=>p.Images).Include(p => p.Color).Include(p=>p.Product.Category).ToList(),
                Product =await _db.Products.FindAsync(id),
                Images=await _db.Images.Where(i=>i.ProductId==id).ToListAsync()

            }); 
        }
    }
}
