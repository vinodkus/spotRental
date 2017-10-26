using Newtonsoft.Json;
using SMT_Amazon_UI.APIGateway;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SMT.SpotRental.UI.APIGateway
{
    public class WebAPICommunicator : APIConstantBase
    {
        public TRes PostRequest_Object<TRes, TReq>(TRes response, TReq request, string strServiceType, string API_NAME)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(request);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(data);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.BaseAddress = new Uri(BASE_URL + (strServiceType == "I" ? API_ITEM_CONTROLLER : (strServiceType == "U" ? API_USER_CONTROLLER : (strServiceType == "V" ? API_VEHICLE_BOOKING_CONTROLLER : API_OTHER_CONTROLLER))));
                    var getUserDetails = client.PostAsync(API_NAME, byteContent)
                    .ContinueWith((taskWith) =>
                    {
                        var res = taskWith.Result;
                        var jsonString = res.Content.ReadAsStringAsync();
                        jsonString.Wait();
                        response = JsonConvert.DeserializeObject<TRes>(jsonString.Result);
                    });

                    getUserDetails.Wait();
                    
                }
                catch (Exception err)
                {
                }

                return response;

            }


        }
        public string PostRequest<TData>(TData request, string strServiceType, string API_NAME, ref string Result)
        {
            string strResult = "";
            using (var client = new HttpClient())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(request);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(data);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.BaseAddress = new Uri(BASE_URL + (strServiceType == "I" ? API_ITEM_CONTROLLER : (strServiceType == "U" ? API_USER_CONTROLLER : (strServiceType=="V"? API_VEHICLE_BOOKING_CONTROLLER : API_OTHER_CONTROLLER))));
                    var getUserDetails = client.PostAsync(API_NAME, byteContent).Result;                   
                    if (getUserDetails.IsSuccessStatusCode)
                    {
                        var response = getUserDetails.Content.ReadAsStringAsync();
                        string strCotes = @"""";

                        if (response.Result.ToString().Replace(strCotes, "").ToLower().Contains("message:true"))
                        {
                            strResult = "TRUE";
                        }
                        else
                        {
                            strResult = "FALSE";
                        }
                        Result = response.Result.ToString();

                    }
                }
                catch (Exception err)
                {
                    strResult = "ERROR";
                }

            }

            return strResult;

        }
        public string PostRequest<TData>(TData request, string strServiceType, string API_NAME)
        {
            string strResult = "";
            using (var client = new HttpClient())
            {
                try
                {
                    var data = JsonConvert.SerializeObject(request);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(data);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.BaseAddress = new Uri(BASE_URL + (strServiceType == "I" ? API_ITEM_CONTROLLER : (strServiceType == "U" ? API_USER_CONTROLLER : (strServiceType == "V" ? API_VEHICLE_BOOKING_CONTROLLER : API_OTHER_CONTROLLER))));
                    var getUserDetails = client.PostAsync(API_NAME, byteContent).Result;

                    if (getUserDetails.IsSuccessStatusCode)
                    {
                        var response = getUserDetails.Content.ReadAsStringAsync();
                        string strCotes = @"""";

                        if (response.Result.ToString().Replace(strCotes, "").ToLower().Contains("true") || response.Result.ToString().Replace(strCotes, "").ToLower().Contains("message:true") || response.Result.ToString().Replace(strCotes, "").ToLower().Contains("1"))
                        {
                            strResult = "TRUE";
                        }
                        else if (response.Result.ToString().Replace(strCotes, "").ToLower().Contains("message:fkey"))
                        {
                            strResult = "FKEY";
                        }
                        else
                        {
                            strResult = "FALSE";
                        }

                    }
                }
                catch (Exception err)
                {
                    strResult = "ERROR";
                }

            }

            return strResult;

        }
        public TData GetResponse<TData>(TData response, string strServiceType, string API_NAME, object[] parmValue, string[] paramName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL + (strServiceType == "I" ? API_ITEM_CONTROLLER : (strServiceType == "U" ? API_USER_CONTROLLER : (strServiceType == "V" ? API_VEHICLE_BOOKING_CONTROLLER : API_OTHER_CONTROLLER))));
                    string _queryString = "";
                    int iIndex = 0;
                    for (; iIndex < parmValue.Length && iIndex < paramName.Length;)
                    {
                        _queryString += paramName[iIndex] + "=" + parmValue[iIndex] + "";
                        iIndex++;
                        _queryString += (iIndex < parmValue.Length && iIndex < paramName.Length) ? "&" : "";
                    }

                    var getUserDetails = client.GetAsync(API_NAME + (_queryString != "" ? "?" + _queryString : ""))
                        .ContinueWith((taskWith) =>
                        {
                            var res = taskWith.Result;
                            var jsonString = res.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            response = JsonConvert.DeserializeObject<TData>(jsonString.Result);
                        });

                    getUserDetails.Wait();
                }
                catch (Exception err)
                {


                }
            }

            return response;

        }
        public TData GetResponse<TData>(TData response, string strServiceType, string API_NAME)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL + (strServiceType == "I" ? API_ITEM_CONTROLLER : (strServiceType == "U" ? API_USER_CONTROLLER : (strServiceType == "V" ? API_VEHICLE_BOOKING_CONTROLLER : API_OTHER_CONTROLLER))));
                    var getUserDetails = client.GetAsync(API_NAME)
                        .ContinueWith((taskWith) =>
                        {
                            var res = taskWith.Result;
                            var jsonString = res.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            response = JsonConvert.DeserializeObject<TData>(jsonString.Result);
                        });

                    getUserDetails.Wait();
                }
                catch (Exception err)
                {


                }
            }

            return response;

        }
        public string GetResponse(string strServiceType, string API_NAME, object[] parmValue, string[] paramName)
        {
            string strResponse = "";
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL + (strServiceType == "I" ? API_ITEM_CONTROLLER : (strServiceType == "U" ? API_USER_CONTROLLER : (strServiceType == "V" ? API_VEHICLE_BOOKING_CONTROLLER : API_OTHER_CONTROLLER))));
                    string _queryString = "";
                    int iIndex = 0;
                    for (; iIndex < parmValue.Length && iIndex < paramName.Length;)
                    {
                        _queryString += paramName[iIndex] + "=" + parmValue[iIndex] + "";
                        iIndex++;
                        _queryString += (iIndex < parmValue.Length && iIndex < paramName.Length) ? "&" : "";
                    }

                    var getUserDetails = client.GetAsync(API_NAME + (_queryString != "" ? "?" + _queryString : ""))
                        .ContinueWith((taskWith) =>
                        {
                            var res = taskWith.Result;
                            var jsonString = res.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            strResponse = JsonConvert.DeserializeObject<string>(jsonString.Result);
                        });

                    getUserDetails.Wait();
                }
                catch (Exception err)
                {


                }
            }

            return strResponse;

        }
    }
}