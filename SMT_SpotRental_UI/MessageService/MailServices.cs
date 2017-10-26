using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SMT.SpotRental.UI.MessageService
{
    /******************************************************************************************************************************************************
    Description  : This class is being used to send different emails. Please note that this call is inheriting base abstract class BaseEmailConfigClass 
                   which contains all email configuration setting. In future this configaration may be changed.
    Developed by : Kapil D. Tripathi
    Date         : AUG 11 2017
    Copyright    : iCtrlBiz Consulting pvt. ltd.
    *******************************************************************************************************************************************************/
    public class MailServices : BaseEmailConfigClass
    {
        public SmtpClient smtp { get; set; }
        public MailServices() : base()
        {
            smtp = new SmtpClient(SMTP_Server);
            smtp.Port = SMTP_Port;
            smtp.Credentials = new NetworkCredential(UserName, Password);
            smtp.EnableSsl = IsSSL_Enable.ToUpper() == "YES" ? true : false;
        }

        public override bool SendMail(string strEmailTemplates, string EmailType)
        {
            bool blRes = false;
            try
            {
                string[] arrayTemplate = strEmailTemplates.Split('~');
                if(EmailType=="ER")
                {
                    blRes = SendMail(arrayTemplate[2], arrayTemplate[4], arrayTemplate[3], "",arrayTemplate[0], arrayTemplate[1]);
                }
            }
            catch
            {

            }
            return blRes;

        }
        private bool SendMail(string To, string From, string Cc, string Bcc, string mailBody, string subject, bool isHTML=true)
        {
            var objEmail = new MailMessage();
            bool blResult = false;

            try
            {
                objEmail.To.Add(To.Replace(";", ","));
                objEmail.From = new MailAddress(From);
                if (Cc != "" && Cc != null)
                    objEmail.CC.Add(Cc.Replace(";", ","));

                if (Bcc != "" && Bcc != null)
                    objEmail.Bcc.Add(Bcc.Replace(";", ","));

                objEmail.Subject = subject;
                objEmail.Body = mailBody;
                objEmail.IsBodyHtml = isHTML;

                smtp.Send(objEmail);
                blResult = true;

            }
            catch (Exception err)
            {
                // TO DO:: Need to handle exception......
            }
            finally
            {
                objEmail.Dispose();
                objEmail = null;

            }

            return blResult;
        }
        private bool SendAttachmentMail(string To, string From, string Cc, string Bcc, string mailBody, string subject, bool isHTML, Attachment attach)
        {
            bool blResult = false;
            var objEmail = new MailMessage();
            try
            {

                objEmail.To.Add(To.Replace(";", ","));
                objEmail.From = new MailAddress(From);
                if (Cc != "" && Cc != null)
                    objEmail.CC.Add(Cc.Replace(";", ","));

                if (Bcc != "" && Bcc != null)
                    objEmail.Bcc.Add(Bcc.Replace(";", ","));



                objEmail.Attachments.Add(attach);
                objEmail.Subject = subject;
                objEmail.Body = mailBody;
                objEmail.IsBodyHtml = isHTML;

                smtp.Send(objEmail);
                attach.Dispose();
                attach = null;
                objEmail.Dispose();
                objEmail = null;


            }//end of try
            catch (Exception ex)
            {
            }
            finally
            {

            }
            return blResult;
        }
    }
}