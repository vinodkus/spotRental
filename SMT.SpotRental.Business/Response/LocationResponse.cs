using SMT.SpotRental.Business.Entities;
using System.Collections.Generic;

namespace SMT.SpotRental.Business.Response
{
    public class LocationResponse:ResponseBase
    {
        public IList<LocationEntity> LocationItems;
    }
}
