using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Username { get; set; }
        public int Count { get; set; }
    }

}
