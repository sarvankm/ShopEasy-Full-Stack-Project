using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class ContactForm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress,Required]
        public string Email { get; set; }
        [Phone]
        public int PhoneNumber { get; set; }
        public string Message { get; set; }

    }
}
