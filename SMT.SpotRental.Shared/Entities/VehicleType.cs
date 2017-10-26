using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
   public class VehicleTypeMaster
    {
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
        public int Capacity { get; set; }
        public string Category { get; set; }
    }
}
