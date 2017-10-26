using SMT.SpotRental.Shared.Entities;
using System.Collections.Generic;

namespace SMT.SpotRental.UI.Response
{
    public class AdhocResponse : ResponseBase
    {
        public IList<RouteItems> itemsList { get; set; }
    }
}