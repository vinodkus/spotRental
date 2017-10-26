using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Entities
{
    public class VehicleTypeMasterEntity
    {
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
        public int Capacity { get; set; }
        public string Category { get; set; }
    }
}
