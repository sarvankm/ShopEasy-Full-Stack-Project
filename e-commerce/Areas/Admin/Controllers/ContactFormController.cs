using e_commerce.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Areas.Admin.Controllers
{
    public class ContactFormController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactFormController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ContactForms);
        }
    }
}
