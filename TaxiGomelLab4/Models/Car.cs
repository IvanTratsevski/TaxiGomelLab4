using System;
using System.Collections.Generic;

namespace TaxiGomelLab4.Models
{
    public partial class Car
    {
        public Car()
        {
            Calls = new HashSet<Call>();
            CarDrivers = new HashSet<CarDriver>();
            CarMechanics = new HashSet<CarMechanic>();
        }

        public int CarId { get; set; }
        public string? RegistrationNumber { get; set; }
        public int? CarModelId { get; set; }
        public string? CarcaseNumber { get; set; }
        public string? EngineNumber { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public int? Mileage { get; set; }
        public DateTime? LastTi { get; set; }
        public string? SpecialMarks { get; set; }

        public virtual CarModel? CarModel { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public virtual ICollection<CarDriver> CarDrivers { get; set; }
        public virtual ICollection<CarMechanic> CarMechanics { get; set; }
    }
}
