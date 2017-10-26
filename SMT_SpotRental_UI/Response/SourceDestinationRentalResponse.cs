using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMT.SpotRental.UI.Response
{
    public class SourceDestinationRentalResponse : ResponseBase
    {
        public IList<SourceDestination> locationList { get; set; }
    }
}