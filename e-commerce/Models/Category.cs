using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string ClassName { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped,Required]
        public IFormFile File { get; set; }
        public ICollection<Product> Products { get; set; }
        public virtual ICollection<CategoryChild> CategoryChild { get; set; }

    }
}
