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
        public IActionResult Index(int? id,int? childid,int? brendid)
        {
            if (childid==null && brendid==null)
            {
                return View(new CategoryVM
                {
                    Products = _db.Products.Where(p => p.CategoryId == id).Include(p => p.Images),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChild.CategoryId == id)
                });

            }
            else if(brendid==null)
            {
                ViewBag.CategoryColor = "#3B93F2";
                return View(new CategoryVM
                {
                    Products = _db.Products.Where(p => p.CategoryChildId == childid).Include(p => p.Images).Take(4),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChildId == childid)
                });
            }
            else
            {
                ViewBag.CategoryColor = "#3B93F2";
                ViewBag.Checked = "checked";

                return View(new CategoryVM
                {
                    Products = _db.Products.Where(p => p.BrendId == brendid).Include(p => p.Images).Take(5),
                    CategoryChildren = _db.CategoryChilds.Where(p => p.CategoryId == id).Include(c => c.Products).Include(p => p.Brends),
                    Brends = _db.Brends.Where(p => p.CategoryChildId == childid)
                });
            }
           
        }
    }
}
