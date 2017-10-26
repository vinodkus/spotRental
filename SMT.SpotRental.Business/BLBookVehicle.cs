using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Business.EntityMapper;
using SMT.SpotRental.Data.Factory;
using SMT.SpotRental.Data.interfaces;
using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business
{
    public class BLBookVehicle
    {
        IVehicleBookingRepository vehicleRepository = null;
        public BLBookVehicle()
        {
            vehicleRepository = DataRepositoryFactory.CreateVehicleBookingRepository();
        }
        public string CreateAdhocRequest(List<RouteItemsEntity> routeItems, AdhocRequestForEntity request)
        {
            return vehicleRepository.CreateAdhocRequest(VehicleBookingMapper.MapRouteItemsEntity(routeItems), VehicleBookingMapper.MapAdhocRequestForEntity(request));
        }
        public IList<RouteItemsEntity> GetAdhocRequest(string FromDate, string ToDate, string EmailID, string StatusCode, string UserID, string EmployeeCode = null, string ForInterFace = null, string VendorId = "0",string GroupNo="0")
        {
            return VehicleBookingMapper.MapRouteItemsEntity(vehicleRepository.GetAdhocRequest(FromDate, ToDate, EmailID, StatusCode, UserID, EmployeeCode, ForInterFace, VendorId, GroupNo));
        }
        public string ManageTripStatus(TripStatusEntity request)
        {
            return vehicleRepository.ManageTripStatus(VehicleBookingMapper.MapTripStatusEntity(request));
        }
        public string ManageTripStatus(TripStatusEntity request,string RequestIds)
        {
            return vehicleRepository.ManageTripStatus(VehicleBookingMapper.MapTripStatusEntity(request), RequestIds);
        }
        public IList<RouteItemsEntity> GetAdhocRequestHistory(string RequestID)
        {
            return VehicleBookingMapper.MapRouteItemsEntity(vehicleRepository.GetAdhocRequestHistory(RequestID));
        }
    }
}
