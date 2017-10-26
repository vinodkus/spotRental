using System;
using System.Collections.Generic;
using SMT.SpotRental.Business.Entities;

namespace SMT.SpotRental.Business.Response
{
    public class VehicleTypeMasterResponse : ResponseBase
    {
        public IList<VehicleTypeMasterEntity> listVehicleType { get; set; }
    }
}
