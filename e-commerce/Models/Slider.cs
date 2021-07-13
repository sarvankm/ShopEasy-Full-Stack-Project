using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped, Required]
        public IFormFile File { get; set; }

    }
}
