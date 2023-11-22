using System;
using System.Collections.Generic;

namespace TaxiGomelLab4.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Calls = new HashSet<Call>();
            CarDrivers = new HashSet<CarDriver>();
            CarMechanics = new HashSet<CarMechanic>();
        }

        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public int? PositionId { get; set; }
        public int? Experience { get; set; }

        public virtual Position? Position { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public virtual ICollection<CarDriver> CarDrivers { get; set; }
        public virtual ICollection<CarMechanic> CarMechanics { get; set; }
    }
}
