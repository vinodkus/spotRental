using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMT.SpotRental.UI.Response
{
    public class MenuResponse : ResponseBase
    {
        public IList<Menu> menuItems { get; set; }
    }
}