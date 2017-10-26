using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
    public class Reason
    {
        public int ReasonID { get; set; }
        public string ReasonName { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonGroup { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
