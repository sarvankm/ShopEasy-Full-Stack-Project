using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ContactForm contactForm = new ContactForm();
            return View(new ContactVM
            {
                Contacts=_db.Contacts,
                ContactForm=_db.ContactForms.FirstOrDefault()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Form(ContactForm contactForm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            await _db.ContactForms.AddAsync(contactForm);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
