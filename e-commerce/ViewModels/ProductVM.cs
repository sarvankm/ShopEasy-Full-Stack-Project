using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModels
{
    public class ProductVM
    {
        public ICollection<ProductColor> ProductColors { get; set; }
        public Specs Specs { get; set; }
        public Product Product { get; set; }
        public ICollection<Image> Images { get; set; }


    }
}
