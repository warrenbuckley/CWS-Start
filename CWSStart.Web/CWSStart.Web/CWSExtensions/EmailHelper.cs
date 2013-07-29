using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using CWSStart.Web.Models;

namespace CWSStart.Web.CWSExtensions
{
    public static class EmailHelper
    {
        private const string SMTPServer     = "smtp.mandrillapp.com";
        private const string SMTPUser       = "warren@creativewebspecialist.co.uk";
        private const string SMTPPassword   = "h4GMK-gX9CB7KXjUePMNaA";


        public static void SendContactEmail(ContactFormViewModel model, string emailTo, string emailSubject)
        {
            //Create email address with friednyl displat names
            MailAddress emailAddressFrom    = new MailAddress(model.Email, model.Name);
            MailAddress emailAddressTo      = new MailAddress(emailTo, "CWS Contact Form");

            //Generate an email message object to send
            MailMessage email   = new MailMessage(emailAddressFrom, emailAddressTo);
            email.Subject       = emailSubject;
            email.Body          = model.Message;

            try
            {
                //Connect to SMTP using MailChimp transactional email service Mandrill
                //This uses a test account - please use your own SMTP settings or set them in the web.config please
                SmtpClient smtp     = new SmtpClient();
                smtp.Host           = SMTPServer;
                smtp.Credentials    = new NetworkCredential(SMTPUser, SMTPPassword);

                //Try & send the email with the SMTP settings
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                //Throw an exception if there is a problem sending the email
                throw ex;
            }

        }

        public static void SendResetPasswordEmail(string memberEmail, string resetGUID)
        {
        }

        public static void SendVerifyEmail(string memberEmail, string verifyGUID)
        {
        }

    }
}
