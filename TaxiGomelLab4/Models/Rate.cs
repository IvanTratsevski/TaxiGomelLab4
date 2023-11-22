using System;
using System.Collections.Generic;

namespace TaxiGomelLab4.Models
{
    public partial class Rate
    {
        public Rate()
        {
            Calls = new HashSet<Call>();
        }

        public int RateId { get; set; }
        public string? RateDescription { get; set; }
        public decimal? RatePrice { get; set; }

        public virtual ICollection<Call> Calls { get; set; }
    }
}
