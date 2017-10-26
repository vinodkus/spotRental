using System.Configuration;

namespace SMT_Amazon_UI.APIGateway
{
    public class APIConstantBase
    {
        public string BASE_URL { get; set; }
        public string API_ITEM_CONTROLLER { get; set; }
        public string API_USER_CONTROLLER { get; set; }
        public string API_OTHER_CONTROLLER { get; set; } // Need to change name 
        public string API_VEHICLE_BOOKING_CONTROLLER { get; set; }
        public APIConstantBase()
        {
            BASE_URL = ConfigurationManager.AppSettings["API_BASE_URL"].ToString();
            API_ITEM_CONTROLLER = ConfigurationManager.AppSettings["API_ITEM_CONTROLLER"].ToString();
            API_USER_CONTROLLER = ConfigurationManager.AppSettings["API_USER_CONTROLLER"].ToString();
            API_VEHICLE_BOOKING_CONTROLLER = ConfigurationManager.AppSettings["API_VEHICLE_BOOKING_CONTROLLER"].ToString(); 
            API_OTHER_CONTROLLER = ConfigurationManager.AppSettings["API_OTHER_CONTROLLER"].ToString(); // Need to change name 
        }
    }
}