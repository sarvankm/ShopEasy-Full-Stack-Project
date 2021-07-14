using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Branch { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        public string ContactLocation { get; set; }
        public bool IsDeleted { get; set; }

    }
}
