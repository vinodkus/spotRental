using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Entities
{
   public class UploadedDocumentEntity
    {
        public int UploadedDocsID { get; set; }
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFileName { get; set; }
        public int VehicleID { get; set; }
        public string VehicleNo { get; set; }
        public string RegistrationNo { get; set; }
        public int DriverGuardID { get; set; }
        public string DriverGuardName { get; set; }
        public DateTime UploadedOn { get; set; }
        public char Active { get; set; }
        public DateTime DeletedOn { get; set; }
        public int UploadedBy { get; set; }
        public int DeletedBy { get; set; }
    }
}
