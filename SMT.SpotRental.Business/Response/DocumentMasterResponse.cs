using SMT.SpotRental.Business.Entities;
using System.Collections.Generic;


namespace SMT.SpotRental.Business.Response
{
    public class DocumentMasterResponse : ResponseBase
    {
        public IList<DocumentMasterEntity> listDocs { get; set; }
    }
}
