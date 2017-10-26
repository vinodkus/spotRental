using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Shared.Entities
{
    public class Designation
    {
        public int designation_id { get; set; }
        public string designation_name { get; set; }
        public DateTime created_date { get; set; }
        public int created_by { get; set; }
    }
}
