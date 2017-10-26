using SMT.SpotRental.Business.Entities;
using System.Collections.Generic;

namespace SMT.SpotRental.Business.Response
{
    public class MenuResponse: ResponseBase
    {
        public IList<MenuEntity> menuItems;
    }
}
