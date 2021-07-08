using e_commerce.Data;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public ProductViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count = 4)
        {
            IEnumerable<Product> products = _db.Products.Take(count);

            return View(await Task.FromResult(products));
        }
    }
}
