using SMT.SpotRental.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.Response
{
    public class DesignationResponse:ResponseBase
    {
        public IList<DesignationEntity> listDesignation { get; set; }
    }
}
