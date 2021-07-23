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
                Sliders=_db.Sliders.Where(c=>c.IsDeleted == false),
                Categories=_db.Categories.Where(c => c.IsDeleted == false),
                Products=_db.Products.Where(c => c.IsDeleted == false).Include(p=>p.Images.Where(i=>i.IsDeleted == false)).OrderByDescending(p=>p.Id).Take(count)
            });
        }
        public IActionResult Load(int skip)
        {
            if (skip >= _db.Products.Count())
            {
                return NoContent();
            }

            IEnumerable<Product> model = _db.Products.Where(c => c.IsDeleted == false).OrderByDescending(p => p.Id).Include(p => p.Images.Where(i=>i.IsDeleted == false)).Skip(skip).Take(4);

            return PartialView("_ProductPartial", model);
        }
        public async Task<IActionResult> Buy(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
               return RedirectToAction("Login", "Account");
            }
            if (id == null) return NotFound();
            Product product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            Order order = new Order
            {
                Name = product.Name,
                Username=User.Identity.Name,
                Price = product.Price,
                Count = 1

            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        public async Task<IActionResult> BuyFromBasket(string ids,string counts)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "Product");
            }
            var ProductIds = JsonConvert.DeserializeObject<List<BasketOrderVM>>(ids);
            var ProductCounts = JsonConvert.DeserializeObject<List<BasketOrderVM>>(counts);

            List<BasketVM> products = new List<BasketVM>();

            string existBasket = Request.Cookies["basket"];
            products = JsonConvert.DeserializeObject<List<BasketVM>>(existBasket);

            List<BasketOrderVM> basketOrderVM = new List<BasketOrderVM>();
            for (int i = 0; i < ProductIds.Count(); i++)
            {
                BasketOrderVM basketOrder = new BasketOrderVM
                {
                    Id = ProductIds[i].Id,
                    count = ProductCounts[i].count
                };
                basketOrderVM.Add(basketOrder);

            }
            foreach (BasketOrderVM item in basketOrderVM)
            {
                if (item.Id == null) return NotFound();
                Product product = await _db.Products.FindAsync(item.Id);
                if (product == null) return NotFound();

                Order order = new Order
                {
                    Name = product.Name,
                    Price = product.Price,
                    Username=User.Identity.Name,
                    Count = item.count

                };
                BasketVM existProduct = products.FirstOrDefault(p => p.Id == item.Id);

                products.Remove(existProduct);
                await _db.Orders.AddAsync(order);
            }
            string basket = JsonConvert.SerializeObject(products);

            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
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
                    Id = product.Id
                };

                products.Add(newProduct);
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
                    item.IsFavorite = dbProduct.IsFavorite;
                    item.Image = _db.Images.FirstOrDefault(i => i.ProductId == item.Id && i.IsDeleted == false).ImageName;
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

            product.IsFavorite = true;
            await _db.SaveChangesAsync();

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
                    IsFavorite=product.IsFavorite
                };
                products.Add(newProduct);
            }


            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("favorite", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return NoContent();
        }
        public async Task<IActionResult> Favorite(int value)
        {
            ViewBag.count = value;

            string favorite = Request.Cookies["favorite"];
            List<FavoriteVM> products = new List<FavoriteVM>();
            if (favorite != null)
            {
                products = JsonConvert.DeserializeObject<List<FavoriteVM>>(favorite);
                
                foreach (FavoriteVM item in products)
                {
                    Product dbProduct = await _db.Products.FindAsync(item.Id);

                    item.Price = dbProduct.Price;
                    item.IsFavorite = dbProduct.IsFavorite;
                    item.Image = _db.Images.FirstOrDefault(i => i.ProductId == item.Id && i.IsDeleted == false).ImageName;
                    item.Name = dbProduct.Name;
                }
                ViewBag.ProductCount = products.Count();
            }
            return View(products);
        }
        public async Task<IActionResult> RemoveFavoriteProduct(int? id,string actionname)
        {
            List<FavoriteVM> products = new List<FavoriteVM>();
            Product product = _db.Products.Find(id);

            product.IsFavorite = false;
            await _db.SaveChangesAsync();

            string existBasket = Request.Cookies["favorite"];
            products = JsonConvert.DeserializeObject<List<FavoriteVM>>(existBasket);

            FavoriteVM existProduct = products.FirstOrDefault(p => p.Id == id);

            products.Remove(existProduct);

            string basket = JsonConvert.SerializeObject(products);

            Response.Cookies.Append("favorite", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
            if (actionname == "Favorite")
            {
                return RedirectToAction("Favorite");
            }
            else
            {
                return NoContent();
            }
        }
        public IActionResult Search(string search)
        {
            IEnumerable<Product> model = _db.Products
                .Include(p => p.Images.Where(c=>c.IsDeleted == false))
                .Where(p => p.Name.ToLower().Contains(search.ToLower()))
                .OrderByDescending(p => p.Id)
                .Take(10);
                
            var a = model.Count();
            return PartialView("_SearchPartial", model);
        }
    }
}
