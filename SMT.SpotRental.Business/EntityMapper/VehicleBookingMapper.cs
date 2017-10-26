using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.EntityMapper
{
    public class VehicleBookingMapper
    {
        public static List<RouteItems> MapRouteItemsEntity(List<RouteItemsEntity> m)
        {
            return m.AsEnumerable().Select(u => new RouteItems()
            {
                CarType = u.CarType,
                Destination = u.Destination,
                EstimatedCost = u.EstimatedCost,
                LandMarkDestination = u.LandMarkDestination,
                LandMarkSource = u.LandMarkSource,
                RentalCity = u.RentalCity,
                PickUpTime = u.PickUpTime,
                ReportingDate = u.ReportingDate,
                ReportingTime = u.ReportingTime,
                RouteType = u.RouteType,
                Source = u.Source,
                ESG = u.ESG,
                RequestId = u.RequestId,
                RequestOn = u.RequestOn,
                StatusID = u.StatusID,
                StatusName = u.StatusName,
                Time = u.Time,
                VendorID = u.VendorID,
                VendorName = u.VendorName,
                EmployeeID = u.EmployeeID,
                Gender = u.Gender,
                Remarks = u.Remarks,
                EmployeeName = u.EmployeeName,
                RequestBy = u.RequestBy,
                DriverId = u.DriverId,
                DriverName = u.DriverName,
                VehicleType = u.VehicleType,
                VehicleID = u.VehicleID,
                ActionBy = u.ActionBy,
                RequestByName = u.RequestByName,
                CreatedDate = u.CreatedDate,
                ReasonID = u.ReasonID,
                ReasonName = u.ReasonName,
                ReasonRemarks = u.ReasonRemarks,
                RequestHistoryId = u.RequestHistoryId,
                StatusCode=u.StatusCode
            }).ToList();
        }
        public static IList<RouteItemsEntity> MapRouteItemsEntity(IList<RouteItems> m)
        {
            return m.AsEnumerable().Select(u => new RouteItemsEntity()
            {
                CarType = u.CarType,
                
                Destination = u.Destination,
                EstimatedCost = u.EstimatedCost,
                LandMarkDestination = u.LandMarkDestination,
                LandMarkSource = u.LandMarkSource,
                RentalCity = u.RentalCity,
                PickUpTime = u.PickUpTime,
                ReportingDate = u.ReportingDate,
                ReportingTime = u.ReportingTime,
                RouteType = u.RouteType,
                Source = u.Source,
                ESG = u.ESG,
                RequestId = u.RequestId,
                RequestOn = u.RequestOn,
                StatusID = u.StatusID,
                StatusName = u.StatusName,
                StatusCode=u.StatusCode,
                Time = u.Time,
                VendorID = u.VendorID,
                VendorName = u.VendorName,
                EmployeeID = u.EmployeeID,
                EmployeeName = u.EmployeeName,
                Remarks = u.Remarks,
                Gender = u.Gender,
                RequestBy = u.RequestBy,
                DriverId = u.DriverId,
                DriverName = u.DriverName,
                VehicleType = u.VehicleType,
                VehicleID = u.VehicleID,
                RequestByName = u.RequestByName,
                ActionBy = u.ActionBy,
                CreatedDate = u.CreatedDate,
                ReasonID = u.ReasonID,
                ReasonName = u.ReasonName,
                ReasonRemarks = u.ReasonRemarks,
                RequestHistoryId = u.RequestHistoryId,
                IsOfficialTrip=u.IsOfficialTrip
               
            }).ToList();
        }
        public static AdhocRequestFor MapAdhocRequestForEntity(AdhocRequestForEntity m)
        {
            return new AdhocRequestFor()
            {
                CostCenter = m.CostCenter,
                CreditCard = m.CreditCard,
                EmailID = m.EmailID,
                EmployeeID = m.EmployeeID,
                Gender = m.Gender,
                HomeAddress = m.HomeAddress,
                MobileNo = m.MobileNo,
                PaymentMode = m.PaymentMode,
                Remarks = m.Remarks,
                UserName = m.UserName,
                RequestBy = m.RequestBy,
                IsOfficialTrip=m.IsOfficialTrip
                
            };
        }
        public static TripStatus MapTripStatusEntity(TripStatusEntity m)
        {
            return new TripStatus()
            {
                DriverID = m.DriverID,
                ReasonID = m.ReasonID,
                ReasonRemarks = m.ReasonRemarks,
                RequestID = m.RequestID,
                StatusID = m.StatusID,
                UserID = m.UserID,
                VehicleID = m.VehicleID,
                VendorId = m.VendorId,
                ActionBy = m.ActionBy,
                ActionById = m.ActionById,
                ActionName=m.ActionName,
                StatusCode=m.StatusCode,
                DisplayFor=m.DisplayFor,
                StatusName=m.StatusName,
                GuardID=m.GuardID,
                RequestIDs=m.RequestIDs

            };
        }
    }
}
