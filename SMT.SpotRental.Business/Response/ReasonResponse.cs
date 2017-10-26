using SMT.SpotRental.Business.Entities;
using System.Collections.Generic;


namespace SMT.SpotRental.Business.Response
{
    public class ReasonResponse : ResponseBase
    {
        public IList<ReasonEntity> reasonList { get; set; }
    }
}
