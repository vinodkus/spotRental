using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
   public class SourceDestination
    {
        public string LocationName { get; set; }
        public string LocationCode { get; set; }
        public string RentalCityName { get; set; }
        public string CarType { get; set; }
        public string RouteType { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Source { get; set; }
    }
}
