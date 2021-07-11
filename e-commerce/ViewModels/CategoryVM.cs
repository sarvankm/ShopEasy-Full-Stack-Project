using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModels
{
    public class CategoryVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<CategoryChild> CategoryChildren { get; set; }
        public IEnumerable<Brend> Brends { get; set; }
    }
}
