using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.SpotRental.Business.EntityMapper
{
    public class ItemsMapper
    {
        public static IList<DesignationEntity> MapDesignationEntity(IList<Designation> m)
        {
            return m.AsEnumerable().Select(u => new DesignationEntity()
            {
                designation_name = u.designation_name,
                created_by = u.created_by,
                created_date = u.created_date,
                designation_id = u.designation_id
            }).ToList();
        }
        public static IList<BaseLocationEntity> MapBaseLocationEntity(IList<BaseLocation> m)
        {
            return m.AsEnumerable().Select(u => new BaseLocationEntity()
            {
                Active = u.Active,
                City = u.City,
                EmailId = u.EmailId,
                LocCode = u.LocCode,
                LocName = u.LocName,
                ShortName = u.ShortName,
                Visible = u.Visible
            }).ToList();
        }
        public static IList<SourceDestinationEntity> MapRentDetails(IList<SourceDestination> m)
        {
            return m.AsEnumerable().Select(u => new SourceDestinationEntity()
            {
                CarType = u.CarType,
                LocationCode = u.LocationCode,
                LocationName = u.LocationName,
                Rate = u.Rate,
                RentalCityName = u.RentalCityName,
                RouteType = u.RouteType,
                Source = u.Source,
                UserId = u.UserId
            }).ToList();
        }
        public static IList<ReasonEntity> MapReason(IList<Reason> m)
        {
            return m.AsEnumerable().Select(u => new ReasonEntity()
            {
                ReasonCode = u.ReasonCode,
                CreatedDate = u.CreatedDate,
                ReasonGroup = u.ReasonGroup,
                ReasonID = u.ReasonID,
                ReasonName = u.ReasonName,
                Status = u.Status
            }).ToList();
        }
        public static IList<VehicleTypeMasterEntity> MapVehicleTypeList(IList<VehicleTypeMaster> m)
        {
            return m.AsEnumerable().Select(u => new VehicleTypeMasterEntity()
            {
                Capacity = u.Capacity,
                Category = u.Category,
                VehicleType = u.VehicleType,
                VehicleTypeID = u.VehicleTypeID
            }).ToList();
        }
        public static IList<TripStatusEntity> MapTripStatus(IList<TripStatus> m)
        {
            return m.AsEnumerable().Select(u => new TripStatusEntity()
            {
                ActionBy = u.ActionBy,
                ActionById = u.ActionById,
                ActionName = u.ActionName,
                DisplayFor = u.DisplayFor,
                DriverID = u.DriverID,
                ReasonID = u.ReasonID,
                ReasonRemarks = u.ReasonRemarks,
                RequestID = u.RequestID,
                StatusID = u.StatusID,
                StatusName = u.StatusName,
                UserID = u.UserID,
                VehicleID = u.VehicleID,
                VendorId = u.VendorId,
                StatusCode = u.StatusCode,
                GuardID = u.GuardID,
                GroupName=u.GroupName,
                RequestIDs=u.RequestIDs

            }).ToList();
        }
        public static VehicleMaster MapVehicleMasterEntity(VehicleMasterEntity u)
        {
            return new VehicleMaster()
            {
                Active = u.Active,
                AssociatedDriverID = u.AssociatedDriverID,
                Capacity = u.Capacity,
                DriverName = u.DriverName,
                LocCode = u.LocCode,
                RegistrationNo = u.RegistrationNo,
                VehicleID = u.VehicleID,
                VehicleType = u.VehicleType,
                VehicleTypeID = u.VehicleTypeID,
                VendorID = u.VendorID,
                VendorName = u.VendorName,
                Destination = u.Destination,
                PickTime = u.PickTime,
                Source = u.Source,
                CreatedBy = u.CreatedBy,
                CreatedOn = u.CreatedOn,
                EffectiveDate = u.EffectiveDate,
                Fit_Crt_Expiry_Date = u.Fit_Crt_Expiry_Date,
                GPSInstallDate = u.GPSInstallDate,
                GPSInstalledBy = u.GPSInstalledBy,
                INS_Expiry_Date = u.INS_Expiry_Date,
                IsGPSInstalled = u.IsGPSInstalled,
                KMLimit = u.KMLimit,
                ModOn = u.ModOn,
                ODO_Reading = u.ODO_Reading,
                Own_Attached = u.Own_Attached,
                Permit_Expiry_Date = u.Permit_Expiry_Date,
                RC_Year = u.RC_Year,
                Remarks = u.Remarks,
                SubVendor = u.SubVendor,
                VehicleNo = u.VehicleNo,
                BillingPlanId = u.BillingPlanId,
                BillingPlanName = u.BillingPlanName,
                LocationName = u.LocationName,
                ManufacturerDate = u.ManufacturerDate,
                PUC_Expiry_Date = u.PUC_Expiry_Date
            };
        }
        public static VehicleHistory MapVehicleHistoryEntity(VehicleHistoryEntity u)
        {
            return new VehicleHistory()
            {
                Active = u.Active,
                BillingName = u.BillingName,
                EffectiveDate = u.EffectiveDate,
                Fit_Crt_Expiry_Date = u.Fit_Crt_Expiry_Date,
                GPSInstallDate = u.GPSInstallDate,
                GPSInstalledBy = u.GPSInstalledBy,
                INS_Expiry_Date = u.INS_Expiry_Date,
                IsGPSInstalled = u.IsGPSInstalled,
                KmLimit = u.KmLimit,
                LocationName = u.LocationName,
                ManufactureDate = u.ManufactureDate,
                ModOn = u.ModOn,
                ODO_Reading = u.ODO_Reading,
                Own_Attached = u.Own_Attached,
                Permit_Expiry_Date = u.Permit_Expiry_Date,
                PUC_Expiry_Date = u.PUC_Expiry_Date,
                RC_Year = u.RC_Year,
                RegistrationNo = u.RegistrationNo,
                SubVendor = u.SubVendor,
                VehicleHistoryID = u.VehicleHistoryID,
                VehicleID = u.VehicleID,
                VehicleNo = u.VehicleNo,
                VehicleType = u.VehicleType,
                VendorName = u.VendorName,
                HistoryDate = u.HistoryDate

            };
        }
        public static IList<VehicleHistoryEntity> MapVehicleHistoryList(IList<VehicleHistory> m)
        {
            return m.AsEnumerable().Select(u => new VehicleHistoryEntity()
            {
                Active = u.Active,
                BillingName = u.BillingName,
                EffectiveDate = u.EffectiveDate,
                Fit_Crt_Expiry_Date = u.Fit_Crt_Expiry_Date,
                GPSInstallDate = u.GPSInstallDate,
                GPSInstalledBy = u.GPSInstalledBy,
                INS_Expiry_Date = u.INS_Expiry_Date,
                IsGPSInstalled = u.IsGPSInstalled,
                KmLimit = u.KmLimit,
                LocationName = u.LocationName,
                ManufactureDate = u.ManufactureDate,
                ModOn = u.ModOn,
                ODO_Reading = u.ODO_Reading,
                Own_Attached = u.Own_Attached,
                Permit_Expiry_Date = u.Permit_Expiry_Date,
                PUC_Expiry_Date = u.PUC_Expiry_Date,
                RC_Year = u.RC_Year,
                RegistrationNo = u.RegistrationNo,
                SubVendor = u.SubVendor,
                VehicleHistoryID = u.VehicleHistoryID,
                VehicleID = u.VehicleID,
                VehicleNo = u.VehicleNo,
                VehicleType = u.VehicleType,
                VendorName = u.VendorName,
                HistoryDate = u.HistoryDate
            }).ToList();
        }
        public static IList<VehicleMasterEntity> MapVehicleMasterList(IList<VehicleMaster> m)
        {
            return m.AsEnumerable().Select(u => new VehicleMasterEntity()
            {
                Active = u.Active,
                AssociatedDriverID = u.AssociatedDriverID,
                Capacity = u.Capacity,
                DriverName = u.DriverName,
                LocCode = u.LocCode,
                RegistrationNo = u.RegistrationNo,
                VehicleID = u.VehicleID,
                VehicleType = u.VehicleType,
                VehicleTypeID = u.VehicleTypeID,
                VendorID = u.VendorID,
                VendorName = u.VendorName,
                Destination = u.Destination,
                PickTime = u.PickTime,
                Source = u.Source,
                CreatedBy = u.CreatedBy,
                CreatedOn = u.CreatedOn,
                EffectiveDate = u.EffectiveDate,
                Fit_Crt_Expiry_Date = u.Fit_Crt_Expiry_Date,
                GPSInstallDate = u.GPSInstallDate,
                GPSInstalledBy = u.GPSInstalledBy,
                INS_Expiry_Date = u.INS_Expiry_Date,
                IsGPSInstalled = u.IsGPSInstalled,
                KMLimit = u.KMLimit,
                ModOn = u.ModOn,
                ODO_Reading = u.ODO_Reading,
                Own_Attached = u.Own_Attached,
                Permit_Expiry_Date = u.Permit_Expiry_Date,
                RC_Year = u.RC_Year,
                Remarks = u.Remarks,
                SubVendor = u.SubVendor,
                VehicleNo = u.VehicleNo,
                BillingPlanId = u.BillingPlanId,
                BillingPlanName = u.BillingPlanName,
                LocationName = u.LocationName,
                PUC_Expiry_Date = u.PUC_Expiry_Date,
                ManufacturerDate = u.ManufacturerDate

            }).ToList();
        }
        public static IList<VehicleMasterEntity> MapVehicleMasterEntityList(IList<VehicleMaster> m)
        {
            return m.AsEnumerable().Select(u => new VehicleMasterEntity()
            {
                Active = u.Active,
                AssociatedDriverID = u.AssociatedDriverID,
                Capacity = u.Capacity,
                DriverName = u.DriverName,
                LocCode = u.LocCode,
                RegistrationNo = u.RegistrationNo,
                VehicleID = u.VehicleID,
                VehicleType = u.VehicleType,
                VehicleTypeID = u.VehicleTypeID,
                VendorID = u.VendorID,
                VendorName = u.VendorName,
                Destination = u.Destination,
                PickTime = u.PickTime,
                Source = u.Source,
                CreatedBy = u.CreatedBy,
                CreatedOn = u.CreatedOn,
                EffectiveDate = u.EffectiveDate,
                Fit_Crt_Expiry_Date = u.Fit_Crt_Expiry_Date,
                GPSInstallDate = u.GPSInstallDate,
                GPSInstalledBy = u.GPSInstalledBy,
                INS_Expiry_Date = u.INS_Expiry_Date,
                IsGPSInstalled = u.IsGPSInstalled,
                KMLimit = u.KMLimit,
                ModOn = u.ModOn,
                ODO_Reading = u.ODO_Reading,
                Own_Attached = u.Own_Attached,
                Permit_Expiry_Date = u.Permit_Expiry_Date,
                RC_Year = u.RC_Year,
                Remarks = u.Remarks,
                SubVendor = u.SubVendor,
                VehicleNo = u.VehicleNo,
                BillingPlanId = u.BillingPlanId,
                BillingPlanName = u.BillingPlanName,
                LocationName = u.LocationName,
                ManufacturerDate = u.ManufacturerDate,
                PUC_Expiry_Date = u.PUC_Expiry_Date

            }).ToList();
        }
        public static IList<DriverGuardEntity> MapDriverGuardEntityList(IList<DriverGuard> m)
        {
            return m.AsEnumerable().Select(u => new DriverGuardEntity()
            {
                Active = u.Active,
                Address = u.Address,
                AddVerification = u.AddVerification,
                BadgeExpiryDate = u.BadgeExpiryDate,
                BadgeNo = u.BadgeNo,
                ContactPerson = u.ContactPerson,
                ContactPersonAddress = u.ContactPersonAddress,
                CreatedBy = u.CreatedBy,
                CreatedOn = u.CreatedOn,
                DOB = u.DOB,
                DOJ = u.DOJ,
                DOL = u.DOL,
                DriverGuardId = u.DriverGuardId,
                Education = u.Education,
                EmpCode = u.EmpCode,
                EmpType = u.EmpType,
                FatherName = u.FatherName,
                FingerPrint = u.FingerPrint,
                FirstName = u.FirstName,
                Height = u.Height,
                LastName = u.LastName,
                LeftFingerPrint = u.LeftFingerPrint,
                LicenceExpiryDate = u.LicenceExpiryDate,
                LicenceNo = u.LicenceNo,
                LocationName = u.LocationName,
                LocCode = u.LocCode,
                MaritalStatus = u.MaritalStatus,
                MobileNo = u.MobileNo,
                ModBy = u.ModBy,
                ModOn = u.ModOn,
                Password = u.Password,
                PinCode = u.PinCode,
                PoliceVerification = u.PoliceVerification,
                RightFingerPrint = u.RightFingerPrint,
                SkinColor = u.SkinColor,
                VendorId = u.VendorId,
                VendorName = u.VendorName,
                Weight = u.Weight
            }).ToList();
        }
        public static EmailDetails MapEmailsDetails(EmailDetailsEntity entity)
        {
            return new EmailDetails()
            {
                ActionLink = entity.ActionLink,
                CreatedOn = entity.CreatedOn,
                EmailBCC = entity.EmailBCC,
                EmailBody = entity.EmailBody,
                EmailCC = entity.EmailCC,
                EmailSubject = entity.EmailSubject,
                EmailTemplateCode = entity.EmailTemplateCode,
                EmailType = entity.EmailType,
                IsSend = entity.IsSend,
                RecID = entity.RecID,
                SendEmailOn = entity.SendEmailOn,
                SMSBody = entity.SMSBody,
                TripLink = entity.TripLink,
                UserEmailID = entity.UserEmailID,
                UserName = entity.UserName,
                UserMobile = entity.UserMobile,
                QueryNo = entity.QueryNo
            };
        }
        public static IList<VendorMasterEntity> MapVendorMasterEntityList(IList<VendorMaster> e)
        {
            return e.AsEnumerable().Select(u => new VendorMasterEntity()
            {
                VendorId = u.VendorId,
                LocationCode = u.LocationCode,
                LocationName = u.LocationName,
                ShortName = u.ShortName,
                PrefixTag = u.PrefixTag,
                IsActive = u.IsActive,
                CompanyName = u.CompanyName,
                EffectiveDate = u.EffectiveDate,
                FName = u.FName,
                MName = u.MName,
                LName = u.LName,
                Address1 = u.Address1,
                Address2 = u.Address2,
                City = u.City,
                ZIPCode = u.ZIPCode,
                Phone1 = u.Phone1,
                Phone2 = u.Phone2,
                EmailID = u.EmailID,
                ValidFrom = u.ValidFrom,
                ValidTo = u.ValidTo,
                CreatedBy = u.CreatedBy,
                UserId = u.UserId
            }).ToList();
        }
        public static VendorMaster MapVendorMasterEntity(VendorMasterEntity e)
        {
            return new VendorMaster()
            {
                VendorId = e.VendorId,
                LocationCode = e.LocationCode,
                LocationName = e.LocationName,
                ShortName = e.ShortName,
                PrefixTag = e.PrefixTag,
                IsActive = e.IsActive,
                CompanyName = e.CompanyName,
                EffectiveDate = e.EffectiveDate,
                FName = e.FName,
                MName = e.MName,
                LName = e.LName,
                Address1 = e.Address1,
                Address2 = e.Address2,
                City = e.City,
                ZIPCode = e.ZIPCode,
                Phone1 = e.Phone1,
                Phone2 = e.Phone2,
                EmailID = e.EmailID,
                ValidFrom = e.ValidFrom,
                ValidTo = e.ValidTo,
                CreatedBy = e.CreatedBy,
                UserId = e.UserId

            };
        }
        public static IList<DocumentMasterEntity> MapDocumentMasterEntityList(IList<DocumentMaster> m)
        {
            return m.AsEnumerable().Select(u => new DocumentMasterEntity()
            {
                Active = u.Active,
                DocDesc = u.DocDesc,
                DocType = u.DocType,
                DocumentID = u.DocumentID,
                Mandatory = u.Mandatory,
                ShortCode = u.ShortCode
            }).ToList();
        }
        public static DocumentMaster MapDocumentMaster(DocumentMasterEntity m)
        {
            return new DocumentMaster()
            {
                Active = m.Active,
                DocDesc = m.DocDesc,
                DocType = m.DocType,
                DocumentID = m.DocumentID,
                Mandatory = m.Mandatory,
                ShortCode = m.ShortCode
            };
        }
        public static UploadedDocument MapUploadedDocumentEntity(UploadedDocumentEntity u)
        {
            return new UploadedDocument()
            {
                Active = u.Active,
                DeletedBy = u.DeletedBy,
                DeletedOn = u.DeletedOn,
                DocumentID = u.DocumentID,
                DocumentName = u.DocumentName,
                DriverGuardID = u.DriverGuardID,
                DriverGuardName = u.DriverGuardName,
                RegistrationNo = u.RegistrationNo,
                UploadedBy = u.UploadedBy,
                UploadedDocsID = u.UploadedDocsID,
                UploadedOn = u.UploadedOn,
                VehicleID = u.VehicleID,
                VehicleNo = u.VehicleNo,
                DocumentFileName=u.DocumentFileName
            };
        }
        public static IList<UploadedDocumentEntity> MapUploadedDocumentEntityList(IList<UploadedDocument> m)
        {
            return m.AsEnumerable().Select(u => new UploadedDocumentEntity()
            {
                Active = u.Active,
                DeletedBy = u.DeletedBy,
                DeletedOn = u.DeletedOn,
                DocumentID = u.DocumentID,
                DocumentName = u.DocumentName,
                DriverGuardID = u.DriverGuardID,
                DriverGuardName = u.DriverGuardName,
                RegistrationNo = u.RegistrationNo,
                UploadedBy = u.UploadedBy,
                UploadedDocsID = u.UploadedDocsID,
                UploadedOn = u.UploadedOn,
                VehicleID = u.VehicleID,
                VehicleNo = u.VehicleNo,
                DocumentFileName = u.DocumentFileName
            }).ToList();
        }
        public static DriverGuard MapDriverGuardEntity(DriverGuardEntity u)
        {
            return new DriverGuard()
            {
                Active = u.Active,
                Address = u.Address,
                AddVerification = u.AddVerification,
                BadgeExpiryDate = u.BadgeExpiryDate,
                BadgeNo = u.BadgeNo,
                ContactPerson = u.ContactPerson,
                ContactPersonAddress = u.ContactPersonAddress,
                CreatedBy = u.CreatedBy,
                CreatedOn = u.CreatedOn,
                DOB = u.DOB,
                DOJ = u.DOJ,
                DOL = u.DOL,
                DriverGuardId = u.DriverGuardId,
                Education = u.Education,
                EmpCode = u.EmpCode,
                EmpType = u.EmpType,
                FatherName = u.FatherName,
                FingerPrint = u.FingerPrint,
                FirstName = u.FirstName,
                Height = u.Height,
                LastName = u.LastName,
                LeftFingerPrint = u.LeftFingerPrint,
                LicenceExpiryDate = u.LicenceExpiryDate,
                LicenceNo = u.LicenceNo,
                LocationName = u.LocationName,
                LocCode = u.LocCode,
                MaritalStatus = u.MaritalStatus,
                MobileNo = u.MobileNo,
                ModBy = u.ModBy,
                ModOn = u.ModOn,
                Password = u.Password,
                PinCode = u.PinCode,
                PoliceVerification = u.PoliceVerification,
                RightFingerPrint = u.RightFingerPrint,
                SkinColor = u.SkinColor,
                VendorId = u.VendorId,
                VendorName = u.VendorName,
                Weight = u.Weight
            };
        }

    }
}
