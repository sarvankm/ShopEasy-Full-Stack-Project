using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped,Required]
        public IFormFile File { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
