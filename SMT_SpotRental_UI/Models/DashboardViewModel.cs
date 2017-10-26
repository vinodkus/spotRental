using SMT.SpotRental.Shared.Entities;
using SMT.SpotRental.UI.Models;
using SMT.SpotRental.UI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMT.SpotRental.UI.Models
{
    public class DashboardViewModel
    {
        public MenuResponse objMenu { get; set; }
        public UserViewModel userDetails { get; set; }
        public RouteDetailsList routeDetails { get; set; }
        public IList<Roles> listRoles { get; set; }
    }

    public class RouteDetailsList
    {
        public IList<RouteDetails> routeList { get; set; }
        public string Remarks { get; set; }
    }
    public class RouteDetails
    {
        public string RouteTypeName { get; set; }
        public int RouteTypeID { get; set; }
        public string CarTypeName { get; set; }
        public int CarTypeID { get; set; }
        public string CarTypeNameOther { get; set; }
        public string RentalCityName { get; set; }
        public int RentalCityID { get; set; }
        public string RentalCityNameOther { get; set; }
        public DateTime ReportingDateTime { get; set; }
        public string ReportingDate { get; set; }
        public string ReportingTime { get; set; }
        public int SourceID { get; set; }
        public string SourceName { get; set; }
        public string SourceOtherName { get; set; }
        public string SourceLandMark { get; set; }
        public int DestinationID { get; set; }
        public string DestinationName { get; set; }
        public string DestinationOtherName { get; set; }
        public string DestinationLandMark { get; set; }
        public decimal EstimatedAmount { get; set; }

    }
}