using SMT.SpotRental.Data.interfaces;
using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using SMT.SpotRental.Database.interfaces;

namespace SMT.SpotRental.Data
{
    public class ItemsRepository : IItemsRepository
    {
        IItems items = null;

        public ItemsRepository()
        {
            items = new ManageItems();
        }
        public IList<Designation> GetDesignationList()
        {
            return items.GetDesignationList();
        }

        public IList<BaseLocation> GetBaseLocationList()
        {
            return items.GetBaseLocationList();
        }

        public IList<SourceDestination> GetRateDetails(string UserID, string RentalCityName, string CarType, string RouteType)
        {
            return items.GetRateDetails(UserID, RentalCityName, CarType, RouteType);
        }
        public IList<Reason> GetReason(string ReasonGroup)
        {
            return items.GetReason(ReasonGroup);
        }
        public IList<VehicleTypeMaster> GetAllVehicleTypes()
        {
            return items.GetAllVehicleTypes();
        }
        public IList<TripStatus> GetTripStatusList(string DisplayFor)
        {
            return items.GetTripStatusList(DisplayFor);
        }

        public IList<VehicleMaster> GetVehicleList(string VendorID)
        {
            return items.GetVehicleList(VendorID);
        }

        public IList<DriverGuard> GetDriverGuardList(string VendorID, string EmpType)
        {
            return items.GetDriverGuardList(VendorID, EmpType);
        }

        public string ManageSendEmails(EmailDetails request)
        {
            return items.ManageSendEmails(request);
        }

        public IList<VendorMaster> SearchVendors(VendorMaster request)
        {
            return items.SearchVendors(request);
        }

        public string ChangeVendorStatus(string VendorID)
        {
            return items.ChangeVendorStatus(VendorID);
        }

        public string RegisterVendor(VendorMaster request)
        {
            return items.RegisterVendor(request);
        }
        public string UpdateVendor(VendorMaster request)
        {
            return items.UpdateVendor(request);
        }
        public IList<VehicleMaster> SearchVehicles(VehicleMaster request)
        {
            return items.SearchVehicles(request);
        }
        public string RegisterVehicle(VehicleMaster request)
        {
            return items.RegisterVehicle(request);
        }
        public string UpdateVehicle(VehicleMaster request)
        {
            return items.UpdateVehicle(request);
        }
        public IList<DocumentMaster> GetDocumentList(DocumentMaster request)
        {
            return items.GetDocumentList(request);
        }

        public string UploadDocuments(UploadedDocument request)
        {
            return items.UploadDocuments(request);
        }

        public IList<UploadedDocument> GetUploadedDocumentDetails(UploadedDocument request)
        {
            return items.GetUploadedDocumentDetails(request);
        }

        public IList<VehicleHistory> GetVehicleHistoryDetails(VehicleHistory request)
        {
            return items.GetVehicleHistoryDetails(request);
        }

        public IList<DriverGuard> SearchDriverGuard(DriverGuard request)
        {
            return items.SearchDriverGuard(request);
        }

        public string RegisterDriverGuard(DriverGuard request)
        {
            return items.RegisterDriverGuard(request);
        }
        public string UpdateDriverGuard(DriverGuard request)
        {
            return items.UpdateDriverGuard(request);
        }
        public string DeleteDriverGuard(DriverGuard request)
        {
            return items.DeleteDriverGuard(request);
        }
    }
}
