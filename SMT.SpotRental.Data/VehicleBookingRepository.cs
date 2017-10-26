using SMT.SpotRental.Data.interfaces;
using SMT.SpotRental.Database;
using SMT.SpotRental.Database.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT.SpotRental.Shared.Entities;

namespace SMT.SpotRental.Data
{
    public class VehicleBookingRepository : IVehicleBookingRepository
    {
        IVehicleBooking vehicle = null;
        public VehicleBookingRepository()
        {
            vehicle = new ManageVehicleBooking();
        }
        public string CreateAdhocRequest(List<RouteItems> routeItems, AdhocRequestFor request)
        {
            return vehicle.CreateAdhocRequest(routeItems, request);
        }
        public IList<RouteItems> GetAdhocRequest(string FromDate, string ToDate, string EmailID, string Status, string UserID, string EmployeeCode = null, string ForInterFace = null, string VendorId = "", string GroupNo = "0")
        {
            return vehicle.GetAdhocRequest(FromDate, ToDate, EmailID, Status, UserID, EmployeeCode, ForInterFace, VendorId, GroupNo);
        }
        public IList<RouteItems> GetAdhocRequestHistory(string RequestID)
        {
            return vehicle.GetAdhocRequestHistory(RequestID);
        }
        public string ManageTripStatus(TripStatus request)
        {
            return vehicle.ManageTripStatus(request);
        }
        public string ManageTripStatus(TripStatus request,string RequestIds)
        {
            return vehicle.ManageTripStatus(request, RequestIds);
        }
    }
}
