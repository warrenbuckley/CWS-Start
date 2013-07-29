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

        public static void SendResetPasswordEmail(string memberEmail, string emailFrom, string emailSubject, string resetGUID)
        {
            //Reset link
            string baseURL  = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, string.Empty);
            var resetURL    = baseURL + "/reset-password?resetGUID=" + resetGUID;

            var message = string.Format(
                                "<h3>Reset Your Password</h3>" +
                                "<p>You have requested to reset your password<br/>" +
                                "If you have not requested to reste your password, simply ignore this email and delete it</p>" +
                                "<p><a href='{0}'>Reset your password</a></p>",
                                resetURL);

            //Create email message to send
            var email           = new MailMessage(emailFrom, memberEmail);
            email.Subject       = emailSubject;
            email.IsBodyHtml    = true;
            email.Body          = message;

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

        public static void SendVerifyEmail(string memberEmail, string emailFrom, string emailSubject, string verifyGUID)
        {
            //Verify link
            string baseURL  = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, string.Empty);
            var verifyURL   = baseURL + "/verify-email?verifyGUID=" + verifyGUID;

            var message = string.Format(
                                "<h3>Verify Your Email</h3>" +
                                "<p>Click here to verify your email address and active your account today</p>" +
                                "<p><a href='{0}'>Verify your email & active your account</a></p>",
                                verifyURL);

            //Create email message to send
            var email           = new MailMessage(emailFrom, memberEmail);
            email.Subject       = emailSubject;
            email.IsBodyHtml    = true;
            email.Body          = message;

            try
            {
                //Connect to SMTP using MailChimp transactional email service Mandrill
                //This uses a test account - please use your own SMTP settings or set them in the web.config please
                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTPServer;
                smtp.Credentials = new NetworkCredential(SMTPUser, SMTPPassword);

                //Try & send the email with the SMTP settings
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                //Throw an exception if there is a problem sending the email
                throw ex;
            }
        }

    }
}
