using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMT.SpotRental.UI.Request
{
    public class TripStatusChangeRequest
    {
        public TripStatus entity { get; set; }
    }
}