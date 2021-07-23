using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsFavorite { get; set; }
        public string Image { get; set; }
    }
}
