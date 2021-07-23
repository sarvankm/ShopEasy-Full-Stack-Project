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
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly int _count;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            _count = _db.Products.Count();

        }
        public IActionResult Index(int? id,int? childid,int? brendid,int? minvalue=0,int? maxvalue=10000)
        {
            ViewBag.CategoryId = id;
            ViewBag.MinValue = minvalue;
            ViewBag.MaxValue = maxvalue;
            TempData["id"] = id;
            TempData["childid"] = childid;
            TempData["brendid"] = brendid;
            TempData["minvalue"] = minvalue;
            TempData["maxvalue"] = maxvalue;
            ViewBag.ProductCount = _count;

            if (childid==null && brendid==null)
            {
                ViewBag.CategoryColor = "#3B93F2";
                CategoryChild categoryChild= _db.CategoryChilds.FirstOrDefault(c => c.CategoryId == id && c.IsDeleted == false);
                if (categoryChild != null)
                {
                    ViewBag.DefaultChild = categoryChild.Name;
                }
                CategoryVM categoryVM = new CategoryVM
                {
                    Products = _db.Products.Where(p => p.CategoryChild.Name== categoryChild.Name && p.CategoryId == id && p.Price >= minvalue && p.Price <= maxvalue && p.IsDeleted == false).OrderByDescending(p => p.Id).Include(p => p.Images.Where(i => i.IsDeleted == false)).Take(9),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChild.Name == categoryChild.Name)
                };

                return View(categoryVM);

            }
            else if(brendid==null)
            {
                ViewBag.CategoryColor = "#3B93F2";
                ViewBag.Childid = childid;
                CategoryVM categoryVM = new CategoryVM
                {
                    Products = _db.Products.Where(p => p.CategoryChildId == childid && p.Price >= minvalue && p.Price <= maxvalue).OrderByDescending(p => p.Id).Include(p => p.Images.Where(i => i.IsDeleted == false)).Take(9),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChildId == childid)
                };

                return View(categoryVM);

            }
            else
            {
                ViewBag.CategoryColor = "#3B93F2";
                ViewBag.Checked = "checked";
                ViewBag.Childid = childid;
                ViewBag.BrendId = brendid;
                CategoryVM categoryVM = new CategoryVM
                {
                    Products = _db.Products.Where(p => p.BrendId == brendid && p.Price >= minvalue && p.Price <= maxvalue).OrderByDescending(p => p.Id).Include(p => p.Images.Where(i=>i.IsDeleted == false)).Take(9),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChildId == childid)
                };

                return View(categoryVM);
            }
           
        }
        public IActionResult Load(int skip)
        {
            if (skip >= _count)
            {
                return NoContent();
            }
            if (TempData["childid"] == null && TempData["brendid"] == null)
            {
                IEnumerable<Product> products = _db.Products.Where(p => p.IsDeleted == false && p.CategoryChild.CategoryId == (int)TempData["id"] && p.CategoryId == (int)TempData["id"] && p.Price >= (int)TempData["minvalue"] && p.Price <= (int)TempData["maxvalue"]).OrderByDescending(p => p.Id).Include(p => p.Images).Skip(skip).Take(6);
                return PartialView("_ProductByCategoryPartial", products);


            }
            else if (TempData["brendid"] == null)
            {
                IEnumerable<Product> products = _db.Products.Where(p => p.IsDeleted == false && p.CategoryChildId == (int)TempData["childid"] && p.Price >= (int)TempData["minvalue"] && p.Price <= (int)TempData["maxvalue"]).OrderByDescending(p => p.Id).Include(p => p.Images).Skip(skip).Take(6);
                return PartialView("_ProductByCategoryPartial", products);


            }
            else
            {
                IEnumerable<Product> products = _db.Products.Where(p => p.IsDeleted == false && p.BrendId == (int)TempData["brendid"] && p.Price >= (int)TempData["minvalue"] && p.Price <= (int)TempData["maxvalue"]).OrderByDescending(p => p.Id).Include(p => p.Images).Skip(skip).Take(6);
                return PartialView("_ProductByCategoryPartial", products);

            }

        }
    }
}
