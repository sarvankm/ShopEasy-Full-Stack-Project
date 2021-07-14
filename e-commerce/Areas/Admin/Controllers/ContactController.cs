using e_commerce.Data;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Contacts);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = await _db.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (contact == null) return NotFound();
            return View(contact);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid) return View();
            Contact dbContact = await _db.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Branch.ToLower() == contact.Branch.ToLower());
            if (dbContact !=null )
            {
                if (dbContact.ContactLocation == contact.ContactLocation)
                {
                    ModelState.AddModelError("Name", "This location already exist in this branch!");
                    return View();
                }
                
            }

            await _db.Contacts.AddAsync(contact);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = await _db.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (contact == null) return NotFound();
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Contact contact)
        {
            if (id == null) return NotFound();
            Contact dbContact = await _db.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbContact == null) return NotFound();


            dbContact.Branch = contact.Branch;
            List<Contact> contacts = _db.Contacts.ToList();
            foreach (Contact item in contacts)
            {
                if (item.ContactLocation == contact.ContactLocation)
                {
                    ModelState.AddModelError("", "This location already exist in some branch!");
                    return View();
                }
            }
            dbContact.ContactLocation = contact.ContactLocation;
            dbContact.ContactEmail = contact.ContactEmail;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Contact dbContact = await _db.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbContact == null) return NotFound();
            return View(dbContact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteContact(int? id)
        {
            if (id == null) return NotFound();
            Contact dbContact = await _db.Contacts.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (dbContact == null) return NotFound();


            dbContact.IsDeleted = true;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

