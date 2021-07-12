using e_commerce.Data;
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
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? id,int? childid,int? brendid,int? minvalue=0,int? maxvalue=10000)
        {
            ViewBag.CategoryId = id;
            ViewBag.MinValue = minvalue;
            ViewBag.MaxValue = maxvalue;

            if (childid==null && brendid==null)
            {
                ViewBag.CategoryColor = "#3B93F2";
                ViewBag.DefaultChild = _db.CategoryChilds.FirstOrDefault(p => p.Name == "Smartfonlar").Name;
                CategoryVM categoryVM = new CategoryVM
                {
                    Products = _db.Products.Where(p => p.CategoryChild.Name == "Smartfonlar" && p.CategoryId == id && p.Price >= minvalue && p.Price <= maxvalue).Include(p => p.Images).Take(9),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChild.CategoryId == id)
                };
                return View(categoryVM);

            }
            else if(brendid==null)
            {
                ViewBag.CategoryColor = "#3B93F2";
                ViewBag.Childid = childid;
                return View(new CategoryVM
                {
                    Products = _db.Products.Where(p => p.CategoryChildId == childid && p.Price >= minvalue && p.Price <= maxvalue).Include(p => p.Images).Take(9),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChildId == childid)
                });
            }
            else
            {
                ViewBag.CategoryColor = "#3B93F2";
                ViewBag.Checked = "checked";
                ViewBag.Childid = childid;
                ViewBag.BrendId = brendid;
                return View(new CategoryVM
                {
                    Products = _db.Products.Where(p => p.BrendId == brendid && p.Price >= minvalue && p.Price <= maxvalue).Include(p => p.Images).Take(9),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChildId == childid)
                });
            }
           
        }
    }
}
