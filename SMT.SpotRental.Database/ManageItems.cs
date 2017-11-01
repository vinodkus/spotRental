
/**********************************************************************************************************************
Description : This class/module is used for managing masters/items for application. This also includes CRUD operation for same.
Date        : 01 AUG 2017
Created By  : Kapil D. Tripathi
Copyright by: iControlBiz Consulting Pvt. Ltd.
************************************************************************************************************************/

using Dapper;
using SMT.SpotRental.Database.interfaces;
using SMT.SpotRental.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
namespace SMT.SpotRental.Database
{
    public class ManageItems : ConstantBase, IItems
    {
        /// <summary>
        /// This method return all available designation list 
        /// </summary>
        /// <returns></returns>
        public IList<Designation> GetDesignationList()
        {
            IList<Designation> listDesignation = new List<Designation>();
            using (DBConnect dc = new DBConnect())
            {
                listDesignation = dc.ExecuteProc<Designation>(SR_USP_GETDESIGNATIONLIST);
            }

            return listDesignation;
        }

        /// <summary>
        /// This method return available base location
        /// </summary>
        /// <returns></returns>
        public IList<BaseLocation> GetBaseLocationList()
        {
            IList<BaseLocation> listBaseLocation = new List<BaseLocation>();
            using (DBConnect dc = new DBConnect())
            {
                listBaseLocation = dc.ExecuteProc<BaseLocation>(SR_USP_GETBASELOCATION);
            }
            return listBaseLocation;
        }

        /// <summary>
        /// This method return location name with rate details
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="RentalCityName"></param>
        /// <param name="CarType"></param>
        /// <param name="RouteType"></param>
        /// <returns></returns>
        public IList<SourceDestination> GetRateDetails(string UserID, string RentalCityName, string CarType, string RouteType)
        {
            IList<SourceDestination> listLocation = new List<SourceDestination>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@UserId", UserID);
                objparams.Add("@RentalCityName", RentalCityName);
                objparams.Add("@CarType", CarType);
                objparams.Add("@RouteType", RouteType);
                listLocation = dc.ExecuteProc<SourceDestination>(SR_USP_GETSOURCEANDDESTINATIONBYLOCCODE, objparams);
            }
            return listLocation;
        }

        /// <summary>
        /// This method return all available reason for particular group
        /// </summary>
        /// <returns>List of reasons</returns>
        public IList<Reason> GetReason(string ReasonGroup)
        {
            IList<Reason> listReason = new List<Reason>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@ReasonGroup", ReasonGroup);
                listReason = dc.ExecuteProc<Reason>(SR_USP_GETREASONDETAILS, objparams);
            }
            return listReason;
        }

        /// <summary>
        /// This stored procedure returns all available vehicle list
        /// </summary>       
        /// <returns>List of vehicle types</returns>
        public IList<VehicleTypeMaster> GetAllVehicleTypes()
        {
            IList<VehicleTypeMaster> listVehicleTypes = new List<VehicleTypeMaster>();
            using (DBConnect dc = new DBConnect())
            {
                listVehicleTypes = dc.ExecuteProc<VehicleTypeMaster>(SR_USP_GETVEHICLETYPE);
            }
            return listVehicleTypes;
        }

        /// <summary>
        /// This method return all available status for particular group/role
        /// </summary>
        /// <returns>List of reasons</returns>
        public IList<TripStatus> GetTripStatusList(string DisplayFor,string GroupName="")
        {
            IList<TripStatus> listTripStatus = new List<TripStatus>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@DisplayFor", DisplayFor);
                objparams.Add("@GroupName", GroupName);
                listTripStatus = dc.ExecuteProc<TripStatus>(SR_USP_GETSTATUSDETAILS, objparams);
            }
            return listTripStatus;
        }

        /// <summary>
        /// This method return all active vehicle list for a vendor
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>
        public IList<VehicleMaster> GetVehicleList(string VendorID)
        {
            IList<VehicleMaster> listVehicle = new List<VehicleMaster>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@VendorID", VendorID);
                listVehicle = dc.ExecuteProc<VehicleMaster>(SR_USP_GETALLVEHICLEFORVENDOR, objparams);
            }
            return listVehicle;
        }

        /// <summary>
        /// This stored procedure get driver and Guard details
        /// </summary>
        /// <param name="VendorID"></param>
        /// <param name="EmpType"></param>
        /// <returns></returns>
        public IList<DriverGuard> GetDriverGuardList(string VendorID, string EmpType)
        {
            IList<DriverGuard> listdriverGuard = new List<DriverGuard>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@VendorID", VendorID);
                objparams.Add("@EmpType", EmpType);
                listdriverGuard = dc.ExecuteProc<DriverGuard>(SR_USP_GETDRIVERGuardFORVENDOR, objparams);
            }
            return listdriverGuard;
        }

        /// <summary>
        /// This method insert email details into email bucket to send emails
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string ManageSendEmails(EmailDetails request)
        {
            string response = "";

            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@UserEmailID", request.UserEmailID);
                objparams.Add("@UserName", request.UserName);
                objparams.Add("@EmailSubject", request.EmailSubject);
                objparams.Add("@EmailBody", request.EmailBody);
                objparams.Add("@EmailType", request.EmailType);
                objparams.Add("@EmailTemplateCode", request.EmailTemplateCode);
                objparams.Add("@ActionLink", request.ActionLink);
                objparams.Add("@QueryNo", request.QueryNo);
                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);


                int iRes = dc.ExecuteProc(SR_USP_MANAGESENDINGEMAILS, objparams);
                response = objparams.Get<string>("@Result");

            }
            return response;
        }

        /// <summary>
        /// This method return all vendor list for searched criteria
        /// </summary>
        /// <returns>List of vendors </returns>
        public IList<VendorMaster> SearchVendors(VendorMaster request)
        {
            IList<VendorMaster> listVendor = new List<VendorMaster>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@QueryNo", 1);
                objparams.Add("@LocationCode", request.LocationCode);
                objparams.Add("@MobileNo", request.Phone1);
                objparams.Add("@FName", request.FName);
                objparams.Add("@LName", request.LName);
                listVendor = dc.ExecuteProc<VendorMaster>(SR_USP_MANAGEVENDORS, objparams);
            }
            return listVendor;
        }

        /// <summary>
        /// This method change status of particular vendor
        /// </summary>
        /// <returns>changed status of vendor </returns>
        public string ChangeVendorStatus(string VendorID)
        {
            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@vendorid", VendorID);
                objparams.Add("@QueryNo", 2);
                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_MANAGEVENDORS, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>
        /// This method register new vendor 
        /// </summary>
        /// <returns>return status of operation </returns>
        public string RegisterVendor(VendorMaster request)
        {

            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@LocationCode", request.LocationCode);
                objparams.Add("@FName", request.FName);
                objparams.Add("@MName", request.MName);
                objparams.Add("@LName", request.LName);
                objparams.Add("@ShortName", request.ShortName);
                objparams.Add("@company_name", request.CompanyName);
                objparams.Add("@prefixtag", request.PrefixTag);
                objparams.Add("@IsActive", request.IsActive);
                objparams.Add("@validfrom", request.ValidFrom);
                objparams.Add("@validtill", request.ValidTo);
                objparams.Add("@address1", request.Address1);
                objparams.Add("@address2", request.Address2);
                objparams.Add("@city", request.City);
                objparams.Add("@phone1", request.Phone1);
                objparams.Add("@phone2", request.Phone2);
                objparams.Add("@zipcode", request.ZIPCode);
                objparams.Add("@emailid", request.EmailID);
                objparams.Add("@effdt", request.EffectiveDate);
                objparams.Add("@CreatedBy", request.CreatedBy);


                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_CREATEVENDOR, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>
        /// This method update vendor details 
        /// </summary>
        /// <returns>return status of operation </returns>
        public string UpdateVendor(VendorMaster request)
        {

            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@VendorId", request.VendorId);
                objparams.Add("@FName", request.FName);
                objparams.Add("@MName", request.MName);
                objparams.Add("@LName", request.LName);
                objparams.Add("@company_name", request.LocationCode);
                objparams.Add("@validfrom", request.ValidFrom);
                objparams.Add("@validtill", request.ValidTo);
                objparams.Add("@address1", request.Address1);
                objparams.Add("@address2", request.Address2);
                objparams.Add("@city", request.City);
                objparams.Add("@phone1", request.Phone1);
                objparams.Add("@phone2", request.Phone2);
                objparams.Add("@zipcode", request.ZIPCode);
                objparams.Add("@emailid", request.EmailID);
                objparams.Add("@modby", request.CreatedBy);


                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_UPDATEVENDOR, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>This method returned searched vehicles
        /// </summary>
        /// <returns>list of vehicles </returns>
        public IList<VehicleMaster> SearchVehicles(VehicleMaster request)
        {
            IList<VehicleMaster> listVehicle = new List<VehicleMaster>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@LocationCode", request.LocCode);
                objparams.Add("@VehicleNo", request.VehicleNo);
                objparams.Add("@RegistrationNo", request.RegistrationNo);
                listVehicle = dc.ExecuteProc<VehicleMaster>(SR_USP_SEARCHVEHICLE, objparams);
            }
            return listVehicle;
        }

        /// <summary>
        /// This method register/add new vehicles for a vendor
        /// </summary>
        /// <returns>return status of operation </returns>
        public string RegisterVehicle(VehicleMaster request)
        {

            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@loccode", request.LocCode);
                objparams.Add("@vendorid", request.VendorID);
                objparams.Add("@vehicle_type_id", request.VehicleTypeID);
                objparams.Add("@bpid", request.BillingPlanId);
                objparams.Add("@regno", request.RegistrationNo);
                objparams.Add("@vehicleno", request.VehicleNo);
                objparams.Add("@associateddriverid", request.AssociatedDriverID);
                objparams.Add("@active", request.Active);
                objparams.Add("@kmlimit", request.KMLimit);
                objparams.Add("@modby", request.CreatedBy);

                if (request.EffectiveDate != null && request.EffectiveDate.Year != 1)
                {
                    objparams.Add("@effdt", request.EffectiveDate);
                }

                if (request.GPSInstallDate != null && request.GPSInstallDate.Year != 1)
                {
                    objparams.Add("@gps_install_date", request.GPSInstallDate);
                }

                objparams.Add("@isgpsinstalled", request.IsGPSInstalled);
                objparams.Add("@remarks", request.Remarks);
                objparams.Add("@gpsinstalledby", request.GPSInstalledBy);
                objparams.Add("@subvendor", request.SubVendor);
                objparams.Add("@rc_year", request.RC_Year);

                if (request.INS_Expiry_Date != null && request.INS_Expiry_Date.Year != 1)
                {
                    objparams.Add("@ins_expiry_dt", request.INS_Expiry_Date);
                }
                if (request.ManufacturerDate != null && request.ManufacturerDate.Year != 1)
                {
                    objparams.Add("@dtmanufacture", request.ManufacturerDate);
                }
                if (request.Fit_Crt_Expiry_Date != null && request.Fit_Crt_Expiry_Date.Year != 1)
                {
                    objparams.Add("@fit_crt_expiry_dt", request.Fit_Crt_Expiry_Date);
                }
                if (request.Permit_Expiry_Date != null && request.Permit_Expiry_Date.Year != 1)
                {
                    objparams.Add("@permit_expiry_dt", request.Permit_Expiry_Date);
                }
                if (request.PUC_Expiry_Date != null && request.PUC_Expiry_Date.Year != 1)
                {
                    objparams.Add("@puc_expiry_dt", request.PUC_Expiry_Date);
                }


                objparams.Add("@odo_reading", request.ODO_Reading);
                objparams.Add("@own_attached", request.Own_Attached);


                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_CREATEVEHICLE, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>
        /// This method returns Vehicle History Details on basis of VehicleID
        /// </summary>
        /// <returns>return status of operation </returns>
        /// 
        public IList<VehicleHistory> GetVehicleHistoryDetails(VehicleHistory request)
        {
            IList<VehicleHistory> listVehicleHistory = new List<VehicleHistory>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@vehicleId", request.VehicleID);
                listVehicleHistory = dc.ExecuteProc<VehicleHistory>(SR_USP_GETVEHICLEHISTORY, objparams);
            }
            return listVehicleHistory;
        }

        /// <summary>
        /// This method update vehicles details
        /// </summary>
        /// <returns>return status of operation </returns>
        public string UpdateVehicle(VehicleMaster request)
        {

            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();

                objparams.Add("@VehicleID", request.VehicleID);
                objparams.Add("@associateddriverid", request.AssociatedDriverID);
                objparams.Add("@bpid", request.BillingPlanId);
                objparams.Add("@active", request.Active);
                objparams.Add("@kmlimit", request.KMLimit);
                objparams.Add("@modby", request.CreatedBy);

                if (request.EffectiveDate != null && request.EffectiveDate.Year != 1)
                {
                    objparams.Add("@effdt", request.EffectiveDate);
                }

                if (request.GPSInstallDate != null && request.GPSInstallDate.Year != 1)
                {
                    objparams.Add("@gps_install_date", request.GPSInstallDate);
                }

                objparams.Add("@isgpsinstalled", request.IsGPSInstalled);
                objparams.Add("@remarks", request.Remarks);
                objparams.Add("@gpsinstalledby", request.GPSInstalledBy);
                objparams.Add("@subvendor", request.SubVendor);
                objparams.Add("@rc_year", request.RC_Year);

                if (request.INS_Expiry_Date != null && request.INS_Expiry_Date.Year != 1)
                {
                    objparams.Add("@ins_expiry_dt", request.INS_Expiry_Date);
                }
                if (request.ManufacturerDate != null && request.ManufacturerDate.Year != 1)
                {
                    objparams.Add("@dtmanufacture", request.ManufacturerDate);
                }
                if (request.Fit_Crt_Expiry_Date != null && request.Fit_Crt_Expiry_Date.Year != 1)
                {
                    objparams.Add("@fit_crt_expiry_dt", request.Fit_Crt_Expiry_Date);
                }
                if (request.Permit_Expiry_Date != null && request.Permit_Expiry_Date.Year != 1)
                {
                    objparams.Add("@permit_expiry_dt", request.Permit_Expiry_Date);
                }
                if (request.PUC_Expiry_Date != null && request.PUC_Expiry_Date.Year != 1)
                {
                    objparams.Add("@puc_expiry_dt", request.PUC_Expiry_Date);
                }


                objparams.Add("@odo_reading", request.ODO_Reading);
                objparams.Add("@own_attached", request.Own_Attached);


                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_UPDATEVEHICLE, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>
        /// This method return list of all document types
        /// </summary>
        /// <returns>return status of operation </returns>
        public IList<DocumentMaster> GetDocumentList(DocumentMaster request)
        {

            IList<DocumentMaster> listDocsMaster = new List<DocumentMaster>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@DocType", request.DocType);
                objparams.Add("@ActiveStatus", request.Active);
                listDocsMaster = dc.ExecuteProc<DocumentMaster>(SR_USP_GETDOCUMENTLIST, objparams);

            }
            return listDocsMaster;
        }

        /// <summary>
        /// This method upload documents details for driver/Guard or vehicles
        /// </summary>
        /// <returns>return status of operation as string</returns>
        public string UploadDocuments(UploadedDocument request)
        {

            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();

                objparams.Add("@document_id", request.DocumentID);
                objparams.Add("@document_file_name", request.DocumentFileName);
                objparams.Add("@vehicleid", request.VehicleID);
                objparams.Add("@driverGuardid", request.DriverGuardID);
                objparams.Add("@active", request.Active);
                objparams.Add("@uploadedby", request.UploadedBy);

                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_UPLOADDOCUMENTS, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>
        /// This method return all document details for vehicle or dirver/Guard
        /// </summary>
        /// <param name="VendorId">string having int value</param>
        /// <param name="DriverGuardID">string having int value</param>
        public IList<UploadedDocument> GetUploadedDocumentDetails(UploadedDocument request)
        {
            IList<UploadedDocument> listUploadedDocs = new List<UploadedDocument>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@vehicleid", request.VehicleID);
                objparams.Add("@driverGuardid", request.DriverGuardID);
                listUploadedDocs = dc.ExecuteProc<UploadedDocument>(SR_USP_GETUPLOADEDDOCS, objparams);
            }
            return listUploadedDocs;
        }
        public IList<DriverGuard> SearchDriverGuard(DriverGuard request)
        {
            IList<DriverGuard> listDriverGuard = new List<DriverGuard>();
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@LocationCode", request.LocCode);
                objparams.Add("@VendorCode", request.VendorId);
                listDriverGuard = dc.ExecuteProc<DriverGuard>(SR_USP_SEARCHDRIVERGUARD, objparams);
            }
            return listDriverGuard;
        }

        /// <summary>
        /// This method register/add new Driver/Guard
        /// </summary>
        /// <returns>return status of operation </returns>
        public string RegisterDriverGuard(DriverGuard request)
        {

            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@VendorId", request.VendorId);
                objparams.Add("@EmpCode", request.EmpCode);
                objparams.Add("@LocationCode", request.LocCode);
                objparams.Add("@FirstName", request.FirstName);
                objparams.Add("@LastName", request.LastName);
                if (request.DOB != null && request.DOB.Year != 1)
                {
                    objparams.Add("@DOB", request.DOB);
                }
                objparams.Add("@FatherName", request.FatherName);
                objparams.Add("@Address", request.Address);
                objparams.Add("@PinCode", request.PinCode);
                objparams.Add("@ContactPerson", request.ContactPerson);
                objparams.Add("@ContactPersonAddress", request.ContactPersonAddress);
                objparams.Add("@MobileNo", request.MobileNo);
                objparams.Add("@LicenceNo", request.LicenceNo);
                if (request.LicenceExpiryDate != null && request.LicenceExpiryDate.Year != 1)
                {
                    objparams.Add("@LicenceExpiryDate", request.LicenceExpiryDate);
                }
                if (request.DOJ != null && request.DOJ.Year != 1)
                {
                    objparams.Add("@DOJ", request.DOJ);
                }
                if (request.DOL != null && request.DOL.Year != 1)
                {
                    objparams.Add("@DOL", request.DOL);
                }
                objparams.Add("@EmpType", request.EmpType);
                if (request.ModOn != null && request.ModOn.Year != 1)
                {
                    objparams.Add("@ModOn", request.ModOn);
                }
                objparams.Add("@ModBy", request.ModBy);
                objparams.Add("@Active", request.Active);
                objparams.Add("@Height", request.Height);
                objparams.Add("@Weight", request.Weight);
                objparams.Add("@SkinColor", request.SkinColor);
                objparams.Add("@MaritalStatus", request.MaritalStatus);
                objparams.Add("@PoliceVerification", request.PoliceVerification);
                objparams.Add("@Education", request.Education);
                objparams.Add("@FingerPrint", request.FingerPrint);
                objparams.Add("@LeftFingerPrint", request.LeftFingerPrint);
                objparams.Add("@RightFingerPrint", request.RightFingerPrint);
                objparams.Add("@BadgeNo", request.BadgeNo);
                if (request.BadgeExpiryDate != null && request.BadgeExpiryDate.Year != 1)
                {
                    objparams.Add("@BadgeExpDate", request.BadgeExpiryDate);
                }
                objparams.Add("@AddVerification", request.AddVerification);
                objparams.Add("@CreatedBy", request.CreatedBy);
                objparams.Add("@BadgeNo", request.BadgeNo);
                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_CREATEDRIVERGUARD, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;
        }

        /// <summary>
        /// This method update Driver/Guard details
        /// </summary>
        /// <returns>return status of operation </returns>
        public string UpdateDriverGuard(DriverGuard request)
        {
            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@DriverGuardId", request.DriverGuardId);
                objparams.Add("@VendorId", request.VendorId);
                objparams.Add("@EmpCode", request.EmpCode);
                objparams.Add("@LocationCode", request.LocCode);
                objparams.Add("@FirstName", request.FirstName);
                objparams.Add("@LastName", request.LastName);
                if (request.DOB != null && request.DOB.Year != 1)
                {
                    objparams.Add("@DOB", request.DOB);
                }
                objparams.Add("@FatherName", request.FatherName);
                objparams.Add("@Address", request.Address);
                objparams.Add("@PinCode", request.PinCode);
                objparams.Add("@ContactPerson", request.ContactPerson);
                objparams.Add("@ContactPersonAddress", request.ContactPersonAddress);
                objparams.Add("@MobileNo", request.MobileNo);
                objparams.Add("@LicenceNo", request.LicenceNo);
                if (request.LicenceExpiryDate != null && request.LicenceExpiryDate.Year != 1)
                {
                    objparams.Add("@LicenceExpiryDate", request.LicenceExpiryDate);
                }
                if (request.DOJ != null && request.DOJ.Year != 1)
                {
                    objparams.Add("@DOJ", request.DOJ);
                }
                if (request.DOL != null && request.DOL.Year != 1)
                {
                    objparams.Add("@DOL", request.DOL);
                }
                objparams.Add("@EmpType", request.EmpType);
                if (request.ModOn != null && request.ModOn.Year != 1)
                {
                    objparams.Add("@ModOn", request.ModOn);
                }
                objparams.Add("@ModBy", request.ModBy);
                objparams.Add("@Active", request.Active);
                objparams.Add("@Height", request.Height);
                objparams.Add("@Weight", request.Weight);
                objparams.Add("@SkinColor", request.SkinColor);
                objparams.Add("@MaritalStatus", request.MaritalStatus);
                objparams.Add("@PoliceVerification", request.PoliceVerification);
                objparams.Add("@Education", request.Education);
                objparams.Add("@FingerPrint", request.FingerPrint);
                objparams.Add("@LeftFingerPrint", request.LeftFingerPrint);
                objparams.Add("@RightFingerPrint", request.RightFingerPrint);
                objparams.Add("@BadgeNo", request.BadgeNo);
                if (request.BadgeExpiryDate != null && request.BadgeExpiryDate.Year != 1)
                {
                    objparams.Add("@BadgeExpDate", request.BadgeExpiryDate);
                }
                objparams.Add("@AddVerification", request.AddVerification);
                objparams.Add("@BadgeNo", request.BadgeNo);
                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_UPDATEDRIVERGUARD, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;

        }

        /// <summary>
        /// This method delete Driver/Guard details
        /// </summary>
        /// <returns>return status of operation </returns>
        public string DeleteDriverGuard(DriverGuard request)
        {
            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                DynamicParameters objparams = new DynamicParameters();
                objparams.Add("@DriverGuardId", request.DriverGuardId);
                objparams.Add("@ModBy", request.ModBy);
                objparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                strResult = Convert.ToString(dc.ExecuteProc(SR_USP_DELETEDRIVERGUARD, objparams));
                strResult = objparams.Get<string>("@Result");
            }
            return strResult;

        }

    }
}
