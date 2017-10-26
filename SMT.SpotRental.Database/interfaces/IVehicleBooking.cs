using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Database.interfaces
{
    public interface IVehicleBooking
    {
        string CreateAdhocRequest(List<RouteItems> routeItems, AdhocRequestFor adhocRequestFor);
        IList<RouteItems> GetAdhocRequest(string FromDate, string ToDate, string EmailID, string StatusCode, string UserID, string EmployeeCode = null, string ForInterFace = null, string VendorId = "0", string GroupNo = "0");
        string ManageTripStatus(TripStatus request);
        string ManageTripStatus(TripStatus request, string RequestIds);
        IList<RouteItems> GetAdhocRequestHistory(string RequestID);
    }
}
