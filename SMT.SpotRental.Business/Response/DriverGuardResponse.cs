using SMT.SpotRental.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Response
{
   public class DriverGuardResponse:ResponseBase
    {
        public IList<DriverGuardEntity> listDriverGuard { get; set; }
    }
}
