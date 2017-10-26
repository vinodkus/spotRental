using SMT.SpotRental.Database.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SMT.SpotRental.Shared.Entities;
using System.Data;

/**********************************************************************************************************************
Description : This class/module is used for booking vehicle/car by user, get all details of booked vehicles etc.
Date        : 22 AUG 2017
Created By  : Kapil D. Tripathi
Copyright by: iControlBiz Consulting Pvt. Ltd.
************************************************************************************************************************/
namespace SMT.SpotRental.Database
{
    public class ManageVehicleBooking : ConstantBase, IVehicleBooking
    {
        public string CreateAdhocRequest(List<RouteItems> routeItems, AdhocRequestFor adhocRequestFor)
        {
            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                var dparams = new DynamicParameters();
                dparams.Add("@EmployeeID", adhocRequestFor.EmployeeID);
                dparams.Add("@PayMentMode", adhocRequestFor.PaymentMode);
                dparams.Add("@Remarks", adhocRequestFor.Remarks);
                dparams.Add("@RequestBy", adhocRequestFor.RequestBy);
                dparams.Add("@IsOfficialTrip", adhocRequestFor.IsOfficialTrip);

                var dtRouteItems = new DataTable();
                dtRouteItems.Columns.Add("RouteType", typeof(string));
                dtRouteItems.Columns.Add("CarType", typeof(string));
                dtRouteItems.Columns.Add("RentalCity", typeof(string));
                dtRouteItems.Columns.Add("PickupTime", typeof(DateTime));
                dtRouteItems.Columns.Add("SourceName", typeof(string));
                dtRouteItems.Columns.Add("DestinationName", typeof(string));
                dtRouteItems.Columns.Add("SourceLandMark", typeof(string));
                dtRouteItems.Columns.Add("DestinationLandMark", typeof(string));

                DataRow dr;
                foreach (var items in routeItems)
                {
                    dr = dtRouteItems.NewRow();
                    dr["RouteType"] = items.RouteType;
                    dr["CarType"] = items.CarType;
                    dr["RentalCity"] = items.RentalCity;
                    dr["PickupTime"] = items.PickUpTime;
                    dr["SourceName"] = items.Source;
                    dr["DestinationName"] = items.Destination;
                    dr["SourceLandMark"] = items.LandMarkSource;
                    dr["DestinationLandMark"] = items.LandMarkDestination;
                    dtRouteItems.Rows.Add(dr);
                }

                dparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                dparams.AddDynamicParams(new { @UDTT = dtRouteItems.AsTableValuedParameter("dbo.sr_udtt_AdhocRequest") });

                int iRes = dc.ExecuteProc(SR_USP_CREATEADHOCREQUEST, dparams);
                strResult = dparams.Get<string>("@Result");
            }

            return strResult;
        }

        public IList<RouteItems> GetAdhocRequest(string FromDate, string ToDate, string EmailID, string StatusCode, string UserID, string EmployeeCode=null, string ForInterFace=null,string VendorId="0",string GroupNo="0")
        {
            IList<RouteItems> objAdhochList = new List<RouteItems>();
            using (DBConnect dc = new DBConnect())
            {
                var dparams = new DynamicParameters();
                dparams.Add("@FromDate", FromDate);
                dparams.Add("@ToDate", ToDate);
                dparams.Add("@EmailID", EmailID);
                dparams.Add("@StatusCode", StatusCode);
                dparams.Add("@UserID", UserID);
                dparams.Add("@ForInterFace", ForInterFace);
                dparams.Add("@VendorId", VendorId);
                dparams.Add("@EmployeeCode", EmployeeCode);
                dparams.Add("@GroupNo", GroupNo);


                objAdhochList = dc.ExecuteProc<RouteItems>(SR_USP_SEARCHADHOCREQUEST, dparams);
            }
            return objAdhochList;
        }

        public string ManageTripStatus(TripStatus request)
        {
            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                var dparams = new DynamicParameters();
                dparams.Add("@StatusCode", request.StatusCode);
                dparams.Add("@ReasonID", request.ReasonID);
                dparams.Add("@RequestID", request.RequestID);
                dparams.Add("@ReasonRemarks", request.ReasonRemarks);
                dparams.Add("@UserID", request.UserID);
                dparams.Add("@VendorID", request.VendorId);
                dparams.Add("@VehicleID", request.VehicleID);
                dparams.Add("@DriverID", request.DriverID);
                dparams.Add("@GuardID", request.GuardID);
                dparams.Add("@ActionBy", request.ActionBy);
                dparams.Add("@ActionById", request.ActionById);

                dparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                int iRes = dc.ExecuteProc(SR_USP_MANAGETRIPSTATUS, dparams);
                strResult = dparams.Get<string>("@Result");

            }

            return strResult;
        }

        public string ManageTripStatus(TripStatus request,string RequestIds)
        {
            string strResult = "";
            using (DBConnect dc = new DBConnect())
            {
                var dparams = new DynamicParameters();
                dparams.Add("@StatusCode", request.StatusCode);
                dparams.Add("@ReasonID", request.ReasonID);
                dparams.Add("@ReasonRemarks", request.ReasonRemarks);
                dparams.Add("@UserID", request.UserID);
                dparams.Add("@ActionBy", request.ActionBy);
                dparams.Add("@ActionById", request.ActionById);

                if(RequestIds!="" && RequestIds.Contains(","))
                {
                    var dtRequestDetails = new DataTable();
                    dtRequestDetails.Columns.Add("RequestID", typeof(int));
                    dtRequestDetails.Columns.Add("ActionByID", typeof(int));
                    dtRequestDetails.Columns.Add("ActionBy", typeof(string));
                    dtRequestDetails.Columns.Add("StatusCode", typeof(string));
                    dtRequestDetails.Columns.Add("DriverID", typeof(int));
                    dtRequestDetails.Columns.Add("VehicleID", typeof(int));
                    dtRequestDetails.Columns.Add("VendorID", typeof(int));
                    dtRequestDetails.Columns.Add("ReasonID", typeof(int));
                    dtRequestDetails.Columns.Add("ReasonRemarks", typeof(string));

                    DataRow dr;
                    foreach (var items in RequestIds.Split(','))
                    {
                        dr = dtRequestDetails.NewRow();
                        dr["RequestID"] = items;
                        dr["ActionByID"] = request.ActionById;
                        dr["ActionBy"] = request.ActionBy;
                        dr["StatusCode"] = request.StatusCode;
                        dr["DriverID"] = request.DriverID;
                        dr["VehicleID"] = request.VehicleID;
                        dr["VendorID"] = request.VendorId;
                        dr["ReasonID"] = request.ReasonID;
                        dr["ReasonRemarks"] = request.ReasonRemarks;

                        
                        dtRequestDetails.Rows.Add(dr);
                    }

                    dparams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                    dparams.AddDynamicParams(new { @UDTT = dtRequestDetails.AsTableValuedParameter("dbo.sr_udtt_requestIds") });

                    int iRes = dc.ExecuteProc(SR_USP_MANAGETRIPSTATUS_FORMULTIPLETRIPS, dparams);
                    strResult = dparams.Get<string>("@Result");
                }
                else
                {
                    strResult = "NO_REQUEST_IDS";
                }
            }

            return strResult;
        }

        public IList<RouteItems> GetAdhocRequestHistory(string RequestID)
        {
            IList<RouteItems> objAdhochList = new List<RouteItems>();
            using (DBConnect dc = new DBConnect())
            {
                var dparams = new DynamicParameters();
                dparams.Add("@RequestId", RequestID);
                objAdhochList = dc.ExecuteProc<RouteItems>(SR_USP_GETADHOCREQUESTHISTORY, dparams);
            }
            return objAdhochList;
        }
    }
}
