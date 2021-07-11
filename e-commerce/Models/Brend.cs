using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Brend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryChildId { get; set; }
        public CategoryChild CategoryChild { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
