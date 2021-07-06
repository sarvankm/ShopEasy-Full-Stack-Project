﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        [ForeignKey("SpecsId")]
        public int? SpecsId { get; set; }
        public virtual Specs Specs { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        [ForeignKey("CategoryChildId")]
        public int? CategoryChildId { get; set; }
        public virtual CategoryChild CategoryChild { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
