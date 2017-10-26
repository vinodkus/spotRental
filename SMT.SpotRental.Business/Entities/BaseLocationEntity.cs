using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Entities
{
   public class BaseLocationEntity
    {
        public string LocCode { get; set; }
        public string LocName { get; set; }
        public string ShortName { get; set; }
        public string City { get; set; }
        public string Active { get; set; }
        public string Visible { get; set; }
        public string EmailId { get; set; }

    }
}
