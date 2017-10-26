using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Entities
{
    public class DocumentMasterEntity
    {
        public int DocumentID { get; set; }
        public string ShortCode { get; set; }
        public string DocDesc { get; set; }
        public char DocType { get; set; }
        public char Mandatory { get; set; }
        public char Active { get; set; }
    }
}
