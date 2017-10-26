using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using SMT.SpotRental.Shared.Entities;

namespace SMT.SpotRental.UI.Request
{

    public class AdhocRequest
    {
       public  AdhocRequestFor adhocRequest { get; set; }
        public IList<RouteItems> routeList { get; set; }
    }
    
}