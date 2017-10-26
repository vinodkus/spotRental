using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Business.EntityMapper;
using SMT.SpotRental.Data.Factory;
using SMT.SpotRental.Data.interfaces;
using System.Collections.Generic;


namespace SMT.SpotRental.Business
{
    public class BLItems
    {
        IItemsRepository itemRepository = null;

        public BLItems()
        {
            itemRepository = DataRepositoryFactory.CreateItemRepository();
        }
        public IList<DesignationEntity> GetDesignationList()
        {
            return ItemsMapper.MapDesignationEntity(itemRepository.GetDesignationList());
        }
        public IList<BaseLocationEntity> GetBaseLocationList()
        {
            return ItemsMapper.MapBaseLocationEntity(itemRepository.GetBaseLocationList());
        }
        public IList<SourceDestinationEntity> GetRateDetails(string UserID, string RentalCityName, string CarType, string RouteType)
        {
            return ItemsMapper.MapRentDetails(itemRepository.GetRateDetails(UserID, RentalCityName, CarType, RouteType));
        }
        public IList<ReasonEntity> GetReason(string ReasonGroup)
        {
            return ItemsMapper.MapReason(itemRepository.GetReason(ReasonGroup));
        }
        public IList<VehicleTypeMasterEntity> GetAllVehicleTypes()
        {
            return ItemsMapper.MapVehicleTypeList(itemRepository.GetAllVehicleTypes());
        }
        public IList<TripStatusEntity> GetTripStatusList(string DisplayFor)
        {
            return ItemsMapper.MapTripStatus(itemRepository.GetTripStatusList(DisplayFor));
        }
        public IList<VehicleMasterEntity> GetVehicleList(string VendorID)
        {
            return ItemsMapper.MapVehicleMasterList(itemRepository.GetVehicleList(VendorID));
        }
        public IList<DriverGuardEntity> GetDriverGuardList(string VendorID, string EmpType)
        {
            return ItemsMapper.MapDriverGuardEntityList(itemRepository.GetDriverGuardList(VendorID, EmpType));
        }
        public string ManageSendEmails(EmailDetailsEntity response)
        {
            return itemRepository.ManageSendEmails(ItemsMapper.MapEmailsDetails(response));
        }
        public IList<VendorMasterEntity> SearchVendors(VendorMasterEntity request)
        {
            return ItemsMapper.MapVendorMasterEntityList(itemRepository.SearchVendors(ItemsMapper.MapVendorMasterEntity(request)));
        }
        public string ChangeVendorStatus(string VendorID)
        {
            return itemRepository.ChangeVendorStatus(VendorID);
        }
        public string RegisterVendor(VendorMasterEntity request)
        {
            return itemRepository.RegisterVendor(ItemsMapper.MapVendorMasterEntity(request));
        }
        public string UpdateVendor(VendorMasterEntity request)
        {
            return itemRepository.UpdateVendor(ItemsMapper.MapVendorMasterEntity(request));
        }
        public IList<VehicleMasterEntity> SearchVehicles(VehicleMasterEntity request)
        {
            return ItemsMapper.MapVehicleMasterEntityList(itemRepository.SearchVehicles(ItemsMapper.MapVehicleMasterEntity(request)));
        }
        public IList<VehicleHistoryEntity> GetVehicleHistoryDetails(VehicleHistoryEntity request)
        {
            return ItemsMapper.MapVehicleHistoryList(itemRepository.GetVehicleHistoryDetails(ItemsMapper.MapVehicleHistoryEntity(request)));
        }
        public string RegisterVehicle(VehicleMasterEntity request)
        {
            return itemRepository.RegisterVehicle(ItemsMapper.MapVehicleMasterEntity(request));
        }
        public string UpdateVehicle(VehicleMasterEntity request)
        {
            return itemRepository.UpdateVehicle(ItemsMapper.MapVehicleMasterEntity(request));
        }
        public IList<DocumentMasterEntity> GetDocumentList(DocumentMasterEntity request)
        {
            return ItemsMapper.MapDocumentMasterEntityList(itemRepository.GetDocumentList(ItemsMapper.MapDocumentMaster(request)));
        }
        public string UploadDocuments(UploadedDocumentEntity request)
        {
            return itemRepository.UploadDocuments(ItemsMapper.MapUploadedDocumentEntity(request));
        }
        public IList<UploadedDocumentEntity> GetUploadedDocumentList(UploadedDocumentEntity request)
        {
            return ItemsMapper.MapUploadedDocumentEntityList(itemRepository.GetUploadedDocumentDetails(ItemsMapper.MapUploadedDocumentEntity(request)));
        }
        public IList<DriverGuardEntity> SearchDriverGuard(DriverGuardEntity request)
        {
            return ItemsMapper.MapDriverGuardEntityList(itemRepository.SearchDriverGuard(ItemsMapper.MapDriverGuardEntity(request)));
        }
        public string RegisterDriverGuard(DriverGuardEntity request)
        {
            return itemRepository.RegisterDriverGuard(ItemsMapper.MapDriverGuardEntity(request));
        }
        public string UpdateDriverGuard(DriverGuardEntity request)
        {
            return itemRepository.UpdateDriverGuard(ItemsMapper.MapDriverGuardEntity(request));
        }
        public string DeleteDriverGuard(DriverGuardEntity request)
        {
            return itemRepository.DeleteDriverGuard(ItemsMapper.MapDriverGuardEntity(request));
        }

    }
}
