using System;
using System.Collections.Generic;

namespace TaxiGomelLab4.Models
{
    public partial class CarModel
    {
        public CarModel()
        {
            Cars = new HashSet<Car>();
        }

        public int CarModelId { get; set; }
        public string? ModelName { get; set; }
        public string? TechStats { get; set; }
        public decimal? Price { get; set; }
        public string? Specifications { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
