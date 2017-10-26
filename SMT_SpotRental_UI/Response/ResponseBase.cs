using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMT.SpotRental.UI.Response
{
    public class ResponseBase
    {
        public int ResultId { get; set; } //1: For Succ # 0: For Erorr # -1: For Exception 
        public bool Result { get; set; }
        public bool IsError { get; set; }
        public bool IsExcep { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string[] OtherMessages { get; set; }
        public string ExceptionMessage { get; set; }
        public string [] paramValue { get; set; }
        public object[] paramName { get; set; }
    }
}