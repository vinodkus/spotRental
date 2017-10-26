using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Database.interfaces
{
    public interface IItems
    {
        IList<Designation> GetDesignationList();
        IList<BaseLocation> GetBaseLocationList();
        IList<SourceDestination> GetRateDetails(string UserID, string RentalCityName, string CarType, string RouteType);
        IList<Reason> GetReason(string ReasonGroup);
        IList<VehicleTypeMaster> GetAllVehicleTypes();
        IList<TripStatus> GetTripStatusList(string DisplayFor);
        IList<VehicleMaster> GetVehicleList(string VendorID);
        IList<DriverGuard> GetDriverGuardList(string VendorID, string EmpType);
        string ManageSendEmails(EmailDetails request);
        IList<VendorMaster> SearchVendors(VendorMaster request);
        string ChangeVendorStatus(string VendorID);
        string RegisterVendor(VendorMaster request);
        string UpdateVendor(VendorMaster request);
        IList<VehicleMaster> SearchVehicles(VehicleMaster request);
        string RegisterVehicle(VehicleMaster request);
        string UpdateVehicle(VehicleMaster request);
        IList<VehicleHistory> GetVehicleHistoryDetails(VehicleHistory request);
        IList<DocumentMaster> GetDocumentList(DocumentMaster request);
        string UploadDocuments(UploadedDocument request);
        IList<UploadedDocument> GetUploadedDocumentDetails(UploadedDocument request);
        IList<DriverGuard> SearchDriverGuard(DriverGuard request);
        string RegisterDriverGuard(DriverGuard request);
        string UpdateDriverGuard(DriverGuard request);
        string DeleteDriverGuard(DriverGuard request);
    }
}
