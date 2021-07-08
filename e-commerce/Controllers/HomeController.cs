using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           
            return View(new HomeVM { 
                Sliders=_db.Sliders,
                Categories=_db.Categories,
                Products=_db.Products.OrderByDescending(a=>a.Id)
            });
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

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Basket(int value)
        {
            //Session Get
            //string session = HttpContext.Session.GetString("Freedi");
            //Cookie Get
            //string cookie = Request.Cookies["Sarvan"];

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
                    item.Image = dbProduct.Image;
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
    }
}
