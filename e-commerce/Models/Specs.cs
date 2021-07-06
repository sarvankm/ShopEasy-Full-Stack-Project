using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Specs
    {
        public int Id { get; set; }
        public string ProducerForView { get; set; }
        public string ProducerValue { get; set; }
        public string ProductionYearForView { get; set; }
        public string ProductionYearValue { get; set; }
        public string TypeForView { get; set; }
        public string TypeValue { get; set; }
        public string OSForView { get; set; }
        public string OSValue { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
