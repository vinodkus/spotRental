using SMT.SpotRental.Business;
using SMT.SpotRental.Business.Entities;
using SMT.SpotRental.Business.Request;
using SMT.SpotRental.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMT.SpotRental.API.Controllers
{
    [RoutePrefix("api/vehiclebooking")]
    public class VehicleBookingController : BaseController
    {
        [Route("createadhocrequest")]
        [HttpPost]
        public AdhocResponse CreateAdhocRequest(AdhocRequest request)
        {
            AdhocResponse adhocResponse = new AdhocResponse();
            try
            {

                if (!VerifyUser())
                {

                }
                else
                {
                    BLBookVehicle objbookVehicle = new BLBookVehicle();
                    adhocResponse.Message = objbookVehicle.CreateAdhocRequest(request.routeList, request.adhocRequest);
                    if (adhocResponse != null)
                    {
                        if (adhocResponse.Message == "TRUE")
                        {
                            adhocResponse.ResultId = 1;
                            adhocResponse.IsExcep = false;
                            adhocResponse.Message = "TRUE";
                            adhocResponse.Result = true;
                        }
                        else if (adhocResponse.Message== "DUPLICATE")
                        {
                            adhocResponse.ResultId = 0;
                            adhocResponse.IsExcep = false;
                            adhocResponse.Message = "DUPLICATE";
                            adhocResponse.Result = false;
                        }
                        else if (adhocResponse.Message.Contains("ERROR"))
                        {
                            adhocResponse.ResultId = 0;
                            adhocResponse.IsExcep = false;
                            adhocResponse.Message = "ERROR";
                            adhocResponse.Result = false;
                        }

                    }
                    else
                    {
                        adhocResponse.ResultId = 0;
                        adhocResponse.IsError = true;
                        adhocResponse.Message = "";
                        adhocResponse.Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                adhocResponse.ResultId = -2;
                adhocResponse.IsExcep = true;
                adhocResponse.ExceptionMessage = ex.Message;
                adhocResponse.Result = false;
            }
            return adhocResponse;
        }

        [Route("getadhocrequest")]
        [HttpPost]
        public AdhocResponse GetAdhocRequest(string FromDate, string ToDate, string EmailID, string StatusCode, string UserID, string EmployeeCode = null, string ForInterFace = null, string VendorId = "0", string GroupNo = "0")
        {
            AdhocResponse adhocResponse = new AdhocResponse();
            try
            {

                if (!VerifyUser())
                {

                }
                else
                {
                    BLBookVehicle objbookVehicle = new BLBookVehicle();
                    adhocResponse.itemsList= objbookVehicle.GetAdhocRequest(FromDate, ToDate, EmailID, StatusCode, UserID, EmployeeCode, ForInterFace, VendorId,GroupNo);
                    if (adhocResponse.itemsList != null && adhocResponse.itemsList.Count>0)
                    {
                        adhocResponse.Result = true;
                        adhocResponse.ResultId = 1;
                        adhocResponse.Message = "TRUE";
                    }
                    else if (adhocResponse.itemsList != null && adhocResponse.itemsList.Count == 0)
                    {
                        adhocResponse.Result = false;
                        adhocResponse.ResultId = 0;
                        adhocResponse.Message = "NORES";
                    }
                    else
                    {
                        adhocResponse.Result = false;
                        adhocResponse.ResultId = -1;
                        adhocResponse.Message = "ERROR";
                        adhocResponse.IsError = true;

                    }
                }
            }
            catch (Exception ex)
            {
                adhocResponse.ResultId = -2;
                adhocResponse.IsExcep = true;
                adhocResponse.ExceptionMessage = ex.Message;
                adhocResponse.Result = false;
            }
            return adhocResponse;
        }

        [Route("managetripstatus")]
        [HttpPost]

        public ResponseBase ManageTripStatus(TripStatusChangeRequest request)
        {
            ResponseBase objResponse = new ResponseBase();
            try
            {

                if (!VerifyUser())
                {
                    // TO DO:: Need to impl security details........
                }
                else
                {
                    BLBookVehicle objbookVehicle = new BLBookVehicle();


                    // -Both method can be made into one but to avaoid complexity and mapping issue separated at API level
                    if (request.entity.RequestIDs==null || request.entity.RequestIDs == "" || request.entity.RequestIDs == "0")
                    {
                        string Result = objbookVehicle.ManageTripStatus(request.entity);
                        if (Result != null && Result == "TRUE")
                        {
                            objResponse.Result = true;
                            objResponse.ResultId = 1;
                            objResponse.Message = "TRUE";
                        }
                        else
                        {
                            objResponse.Result = false;
                            objResponse.ResultId = 0;
                            objResponse.Message = "FALSE";
                        }
                    }
                    else
                    {
                        // To cancel multiple trips
                        string Result = objbookVehicle.ManageTripStatus(request.entity, request.entity.RequestIDs);
                        if (Result != null && Result == "TRUE")
                        {
                            objResponse.Result = true;
                            objResponse.ResultId = 1;
                            objResponse.Message = "TRUE";
                        }
                        else
                        {
                            objResponse.Result = false;
                            objResponse.ResultId = 0;
                            objResponse.Message = "FALSE";
                        }
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

        [Route("getadhocrequesthistory")]
        [HttpGet]
        public AdhocResponse GetAdhocRequestHistory(string RequestID)
        {
            AdhocResponse adhocResponse = new AdhocResponse();
            try
            {

                if (!VerifyUser())
                {

                }
                else
                {
                    BLBookVehicle objbookVehicle = new BLBookVehicle();
                    adhocResponse.itemsList = objbookVehicle.GetAdhocRequestHistory(RequestID);
                    if (adhocResponse.itemsList != null && adhocResponse.itemsList.Count > 0)
                    {
                        adhocResponse.Result = true;
                        adhocResponse.ResultId = 1;
                        adhocResponse.Message = "TRUE";
                    }
                    else if (adhocResponse.itemsList != null && adhocResponse.itemsList.Count == 0)
                    {
                        adhocResponse.Result = false;
                        adhocResponse.ResultId = 0;
                        adhocResponse.Message = "NORES";
                    }
                    else
                    {
                        adhocResponse.Result = false;
                        adhocResponse.ResultId = -1;
                        adhocResponse.Message = "ERROR";
                        adhocResponse.IsError = true;

                    }
                }
            }
            catch (Exception ex)
            {
                adhocResponse.ResultId = -2;
                adhocResponse.IsExcep = true;
                adhocResponse.ExceptionMessage = ex.Message;
                adhocResponse.Result = false;
            }
            return adhocResponse;
        }
    }
}