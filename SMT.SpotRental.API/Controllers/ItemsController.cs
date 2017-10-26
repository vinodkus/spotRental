using System.Web.Http;
using SMT.SpotRental.Business;
using SMT.SpotRental.Business.Response;
using SMT.SpotRental.Business.Request;
using System;
using System.Collections.Generic;
using SMT.SpotRental.Business.Entities;
using System.Configuration;

namespace SMT.SpotRental.API.Controllers
{
    [RoutePrefix("api/items")]
    public class ItemsController : BaseController
    {
        [AllowAnonymous]
        [Route("getdesignationlist")]
        [HttpGet]
        public DesignationResponse GetDesignationList()
        {
            DesignationResponse objDesignation = new DesignationResponse();
            try
            {

                if (!VerifyUser())
                {

                }
                else
                {
                    BLItems objItems = new BLItems();

                    objDesignation.listDesignation = new List<DesignationEntity>();
                    objDesignation.listDesignation = objItems.GetDesignationList();
                    if (objDesignation.listDesignation != null && objDesignation.listDesignation.Count > 0)
                    {
                        objDesignation.ResultId = 1;
                        objDesignation.IsExcep = false;
                        objDesignation.Message = "TRUE";
                        objDesignation.Result = true;
                    }
                    else
                    {
                        objDesignation.ResultId = 0;
                        objDesignation.IsError = true;
                        objDesignation.Message = "";
                        objDesignation.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objDesignation.ResultId = -2;
                objDesignation.IsExcep = true;
                objDesignation.ExceptionMessage = ex.Message;
                objDesignation.Result = false;
            }
            return objDesignation;
        }

        [AllowAnonymous]
        [Route("getbaselocation")]
        [HttpGet]
        public BaseLocationResponse GetBaseLocation()
        {
            BaseLocationResponse objBaseLocation = new BaseLocationResponse();
            try
            {

                if (!VerifyUser())
                {

                }
                else
                {
                    BLItems objItems = new BLItems();
                    objBaseLocation.listBaseLocation = new List<BaseLocationEntity>();
                    objBaseLocation.listBaseLocation = objItems.GetBaseLocationList();
                    if (objBaseLocation.listBaseLocation != null && objBaseLocation.listBaseLocation.Count > 0)
                    {
                        objBaseLocation.ResultId = 1;
                        objBaseLocation.IsExcep = false;
                        objBaseLocation.Message = "TRUE";
                        objBaseLocation.Result = true;
                    }
                    else
                    {
                        objBaseLocation.ResultId = 0;
                        objBaseLocation.IsError = true;
                        objBaseLocation.Message = "";
                        objBaseLocation.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objBaseLocation.ResultId = -2;
                objBaseLocation.IsExcep = true;
                objBaseLocation.ExceptionMessage = ex.Message;
                objBaseLocation.Result = false;
            }
            return objBaseLocation;
        }

        [Route("getratedetails")]
        [HttpGet]
        public SourceDestinationRentalResponse GetRateDetails(string UserID, string RentalCityName, string CarType, string RouteType)
        {
            SourceDestinationRentalResponse objResponse = new SourceDestinationRentalResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.locationList = new List<SourceDestinationEntity>();
                    objResponse.locationList = objItems.GetRateDetails(UserID, RentalCityName, CarType, RouteType);
                    if (objResponse.locationList != null && objResponse.locationList.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("getreason")]
        [HttpGet]
        public ReasonResponse GetReason(string ReasonGroup)
        {
            ReasonResponse objResponse = new ReasonResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.reasonList = new List<ReasonEntity>();
                    objResponse.reasonList = objItems.GetReason(ReasonGroup);
                    if (objResponse.reasonList != null && objResponse.reasonList.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("getallvehicleTypes")]
        [HttpGet]
        public VehicleTypeMasterResponse GetAllVehicleTypes()
        {
            VehicleTypeMasterResponse objResponse = new VehicleTypeMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listVehicleType = new List<VehicleTypeMasterEntity>();
                    objResponse.listVehicleType = objItems.GetAllVehicleTypes();
                    if (objResponse.listVehicleType != null && objResponse.listVehicleType.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("gettripstatuslist")]
        [HttpGet]
        public TripStatusResponse GetTripStatusList(string DisplayFor)
        {
            TripStatusResponse objResponse = new TripStatusResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listTripStatus = new List<TripStatusEntity>();
                    objResponse.listTripStatus = objItems.GetTripStatusList(DisplayFor);
                    if (objResponse.listTripStatus != null && objResponse.listTripStatus.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("getvehiclelist")]
        [HttpGet]
        public VehicleMasterResponse GetVehicleList(string VendorID)
        {
            VehicleMasterResponse objResponse = new VehicleMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listVehicle = new List<VehicleMasterEntity>();
                    objResponse.listVehicle = objItems.GetVehicleList(VendorID);
                    if (objResponse.listVehicle != null && objResponse.listVehicle.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "NORES";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("getdriverGuardlist")]
        [HttpGet]
        public DriverGuardResponse GetDriverGuardList(string VendorID, string EmpType)
        {
            DriverGuardResponse objResponse = new DriverGuardResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listDriverGuard = new List<DriverGuardEntity>();
                    objResponse.listDriverGuard = objItems.GetDriverGuardList(VendorID, EmpType);
                    if (objResponse.listDriverGuard != null && objResponse.listDriverGuard.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("managesendemails")]
        [HttpPost]
        public string ManageSendEmails(EmailDetailsEntity request)
        {
            string response = "";
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    response = objItems.ManageSendEmails(request);
                }
            }
            catch (Exception ex)
            {
                response = "ERROR";
            }
            return response;
        }

        [Route("searchvenders")]
        [HttpPost]
        public VendorMasterResponse SearchVendors(VendorMasterEntity request)
        {
            VendorMasterResponse objResponse = new VendorMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listVendor = new List<VendorMasterEntity>();
                    objResponse.listVendor = objItems.SearchVendors(request);
                    if (objResponse.listVendor != null && objResponse.listVendor.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "NORES";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("changevendorstatus")]
        [HttpPost]
        public string ChangeVendorStatus(VendorMasterEntity request)
        {
            string Result = "";
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    Result = objItems.ChangeVendorStatus(request.VendorId);
                }
            }
            catch (Exception ex)
            {
                Result = "Error";
            }
            return Result;
        }

        [Route("registervendor")]
        [HttpPost]
        public VendorMasterResponse RegisterVendor(VendorMasterEntity request)
        {
            VendorMasterResponse objResponse = new VendorMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    string strRes = objItems.RegisterVendor(request);
                    if (!string.IsNullOrEmpty(strRes) && strRes == "TRUE")
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = strRes;
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("updatevendor")]
        [HttpPost]
        public VendorMasterResponse UpdateVenoder(VendorMasterEntity request)
        {
            VendorMasterResponse objResponse = new VendorMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    string strRes = objItems.UpdateVendor(request);
                    if (!string.IsNullOrEmpty(strRes) && strRes == "TRUE")
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = strRes;
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("searchvehicles")]
        [HttpPost]
        public VehicleMasterResponse SearchVehicles(VehicleMasterEntity request)
        {
            VehicleMasterResponse objResponse = new VehicleMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listVehicle = new List<VehicleMasterEntity>();
                    objResponse.listVehicle = objItems.SearchVehicles(request);
                    if (objResponse.listVehicle != null && objResponse.listVehicle.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "NORES";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("getvehiclehistorydetails")]
        [HttpPost]
        public VehicleHistoryResponse GetVehicleHistoryDetails(VehicleHistoryEntity request)
        {
            VehicleHistoryResponse objResponse = new VehicleHistoryResponse();
            try
            {
                if (!VerifyUser())
                { }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listVehicleHistory = new List<VehicleHistoryEntity>();
                    objResponse.listVehicleHistory = objItems.GetVehicleHistoryDetails(request);
                    if (objResponse.listVehicleHistory != null && objResponse.listVehicleHistory.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "NORES";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }


        [Route("registervehicle")]
        [HttpPost]
        public VehicleMasterResponse RegisterVehicle(VehicleMasterEntity request)
        {
            VehicleMasterResponse objResponse = new VehicleMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    string strRes = objItems.RegisterVehicle(request);
                    if (!string.IsNullOrEmpty(strRes) && strRes == "TRUE")
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = strRes;
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("updatevehicle")]
        [HttpPost]
        public VehicleMasterResponse UpdateVehicle(VehicleMasterEntity request)
        {
            VehicleMasterResponse objResponse = new VehicleMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    string strRes = objItems.UpdateVehicle(request);
                    if (!string.IsNullOrEmpty(strRes) && strRes == "TRUE")
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = strRes;
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("getdocumentlist")]
        [HttpPost]
        public DocumentMasterResponse GetDocumentList(DocumentMasterEntity request)
        {
            DocumentMasterResponse objResponse = new DocumentMasterResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listDocs = new List<DocumentMasterEntity>();
                    objResponse.listDocs = objItems.GetDocumentList(request);
                    if (objResponse.listDocs != null && objResponse.listDocs.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "NOREC";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("uploaddocuments")]
        [HttpPost]
        public string UploadDocuments(UploadedDocumentEntity request)
        {
            string Result = "";
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    Result = objItems.UploadDocuments(request);
                }
            }
            catch (Exception ex)
            {
                Result = "ERROR";
            }
            return Result;
        }

        [Route("getuploadeddocumentlist")]
        [HttpPost]
        public UploadedDocsResponse GetUploadedDocumentList(UploadedDocumentEntity request)
        {
            UploadedDocsResponse objResponse = new UploadedDocsResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listUploadedDocs = new List<UploadedDocumentEntity>();
                    objResponse.listUploadedDocs = objItems.GetUploadedDocumentList(request);
                    if (objResponse.listUploadedDocs != null && objResponse.listUploadedDocs.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "NOREC";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("searchdriverguard")]
        [HttpPost]
        public DriverGuardResponse SearchDriverGuard(DriverGuardEntity request)
        {
            DriverGuardResponse objResponse = new DriverGuardResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    objResponse.listDriverGuard = new List<DriverGuardEntity>();
                    objResponse.listDriverGuard = objItems.SearchDriverGuard(request);
                    if (objResponse.listDriverGuard != null && objResponse.listDriverGuard.Count > 0)
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = "NORES";
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("registerdriverguard")]
        [HttpPost]
        public DriverGuardResponse RegisterDriverGuard(DriverGuardEntity request)
        {
            DriverGuardResponse objResponse = new DriverGuardResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    string strRes = objItems.RegisterDriverGuard(request);
                    if (!string.IsNullOrEmpty(strRes) && strRes == "TRUE")
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = strRes;
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("updatedriverguard")]
        [HttpPost]
        public DriverGuardResponse UpdateDriverGuard(DriverGuardEntity request)
        {
            DriverGuardResponse objResponse = new DriverGuardResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    string strRes = objItems.UpdateDriverGuard(request);
                    if (!string.IsNullOrEmpty(strRes) && strRes == "TRUE")
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = strRes;
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

        [Route("deletedriverguard")]
        [HttpPost]
        public DriverGuardResponse DeleteDriverGuard(DriverGuardEntity request)
        {
            DriverGuardResponse objResponse = new DriverGuardResponse();
            try
            {

                if (!VerifyUser())
                {
                    // To do: Need to implement security details
                }
                else
                {
                    BLItems objItems = new BLItems();
                    string strRes = objItems.DeleteDriverGuard(request);
                    if (!string.IsNullOrEmpty(strRes) && strRes == "TRUE")
                    {
                        objResponse.ResultId = 1;
                        objResponse.IsExcep = false;
                        objResponse.Message = "TRUE";
                        objResponse.Result = true;
                    }
                    else
                    {
                        objResponse.ResultId = 0;
                        objResponse.IsError = true;
                        objResponse.Message = strRes;
                        objResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.ResultId = -2;
                objResponse.IsExcep = true;
                objResponse.ExceptionMessage = ex.Message;
                objResponse.Result = false;
            }
            return objResponse;
        }

    }
}
