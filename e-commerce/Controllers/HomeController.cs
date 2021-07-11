using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly int _count;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
            _count = _db.Products.Count();

        }
        public IActionResult Index(int count=4)
        {
            ViewBag.ProductCount = _count;

            return View(new HomeVM { 
                Sliders=_db.Sliders,
                Categories=_db.Categories,
                Products=_db.Products.Include(p=>p.Images).Take(count)
            });
        }
        public IActionResult Load(int skip)
        {
            if (skip >= _db.Products.Count())
            {
                return NotFound();
            }

            IEnumerable<Product> model = _db.Products.Include(p=>p.Images).Skip(skip).Take(4);

            return PartialView("_ProductPartial", model);
        }
        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            Order order = new Order
            {
                Name = product.Name,
                Price = product.Price,
                Count = 1

            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return NoContent();
        }
            public async Task<IActionResult> AddToBasket(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            List<BasketVM> products;
            string existBasket = Request.Cookies["basket"];
            if (existBasket == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(existBasket);
            }

            BasketVM existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null)
            {
                BasketVM newProduct = new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                };
                products.Add(newProduct);
            }
            else
            {
                existProduct.Count++;
            }


            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return NoContent();
        }

        public async Task<IActionResult> Basket(int value)
        {


            ViewBag.count = value;
            
            string basket = Request.Cookies["Basket"];
            List<BasketVM> products = new List<BasketVM>();
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (BasketVM item in products)
                {
                    Product dbProduct = await _db.Products.FindAsync(item.Id);

                    item.Price = dbProduct.Price;
                    item.Image = _db.Images.FirstOrDefault(i => i.ProductId == item.Id).ImageName;
                    item.Name = dbProduct.Name;
                }
            }

            return View(products);
        }
        public IActionResult RemoveProduct(int? id)
        {
            List<BasketVM> products = new List<BasketVM>();
            Product product = _db.Products.Find(id);

            string existBasket = Request.Cookies["basket"];
            products = JsonConvert.DeserializeObject<List<BasketVM>>(existBasket);

            BasketVM existProduct = products.FirstOrDefault(p => p.Id == id);

            products.Remove(existProduct);

            string basket = JsonConvert.SerializeObject(products);

            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return RedirectToAction("Basket");


        }
        public async Task<IActionResult> AddToFavorite(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            List<FavoriteVM> products;
            string existFavorite = Request.Cookies["favorite"];
            if (existFavorite == null)
            {
                products = new List<FavoriteVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<FavoriteVM>>(existFavorite);
            }

            FavoriteVM existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null)
            {
                FavoriteVM newProduct = new FavoriteVM
                {
                    Id = product.Id,
                    Count = 1
                };
                products.Add(newProduct);
            }
            else
            {
                existProduct.Count++;
            }


            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("favorite", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Favorite(int value)
        {
      

            ViewBag.count = value;

            string basket = Request.Cookies["favorite"];
            List<FavoriteVM> products = new List<FavoriteVM>();
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<FavoriteVM>>(basket);
                foreach (FavoriteVM item in products)
                {
                    Product dbProduct = await _db.Products.FindAsync(item.Id);

                    item.Price = dbProduct.Price;
                    item.Image = _db.Images.FirstOrDefault(i => i.ProductId == item.Id).ImageName;
                    item.Name = dbProduct.Name;
                }
            }

            return View(products);
        }
        public IActionResult RemoveFavoriteProduct(int? id)
        {
            List<FavoriteVM> products = new List<FavoriteVM>();
            Product product = _db.Products.Find(id);

            string existBasket = Request.Cookies["favorite"];
            products = JsonConvert.DeserializeObject<List<FavoriteVM>>(existBasket);

            FavoriteVM existProduct = products.FirstOrDefault(p => p.Id == id);

            products.Remove(existProduct);

            string basket = JsonConvert.SerializeObject(products);

            Response.Cookies.Append("favorite", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return RedirectToAction("Favorite");
        }
        public IActionResult Search(string search)
        {
            IEnumerable<Product> model = _db.Products
                .Include(p => p.Images)
                .Where(p => p.Name.ToLower().Contains(search.ToLower()))
                .OrderByDescending(p => p.Id)
                .Take(10);
                
            var a = model.Count();
            return PartialView("_SearchPartial", model);
        }
    }
}
