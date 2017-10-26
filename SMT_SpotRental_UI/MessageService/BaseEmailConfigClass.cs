using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace SMT.SpotRental.UI.MessageService
{
    public abstract class BaseEmailConfigClass
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SMTP_Server { get; set; }
        public int SMTP_Port { get; set; }
        public string IsSSL_Enable { get; set; }
      
        public BaseEmailConfigClass()
        {
            SMTP_Server = ConfigurationManager.AppSettings["SmtpServerStr"].ToString();
            SMTP_Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString());
            UserName = ConfigurationManager.AppSettings["Username"].ToString();
            Password = ConfigurationManager.AppSettings["Password"].ToString();
            IsSSL_Enable = ConfigurationManager.AppSettings["SSL"].ToString();
        }

        public abstract bool SendMail(string strEmailTemplates, string EmailType);
    }
}