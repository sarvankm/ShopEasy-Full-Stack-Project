using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModels
{
    public class ContactVM
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public ContactForm ContactForm { get; set; }

    }
}
