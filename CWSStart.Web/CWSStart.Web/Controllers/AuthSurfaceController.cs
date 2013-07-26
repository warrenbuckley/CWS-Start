using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using umbraco.cms.businesslogic.member;
using Umbraco.Web.Mvc;
using CWSStart.Web.Models;

namespace CWSStart.Web.Controllers
{
    public class AuthSurfaceController : SurfaceController
    {
        //Login
        public ActionResult RenderLogin()
        {
            //Create a new Login View Model
            var loginModel = new LoginViewModel();

            //If the returnURL is empty...
            if (string.IsNullOrEmpty(HttpContext.Request["ReturnUrl"]))
            {
                //Then set it to root - '/'
                loginModel.ReturnUrl = "/";
            }
            else
            {
                //Lets use the return URL in the querystring or form post
                loginModel.ReturnUrl = HttpContext.Request["ReturnUrl"];
            }

            return PartialView("Login", loginModel);
        }

        /// <summary>
        /// Handles the login form when user posts the form/attempts to login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //Member already logged in - redirect to home
            if (Member.IsLoggedOn())
            {
                return Redirect("/");
            }

            //Lets TRY to log the user in
            try
            {
                //Try and login the user...
                if (Membership.ValidateUser(model.EmailAddress, model.Password))
                {
                    //Valid credentials

                    //Get the member from their email address
                    var checkMember = Member.GetMemberFromEmail(model.EmailAddress);

                    //Check the member exists
                    if (checkMember != null)
                    {
                        //Let's check they have verified their email address
                        if (Convert.ToBoolean(checkMember.getProperty("hasVerifiedEmail").Value))
                        {
                            //Update number of logins counter
                            int noLogins = 0;
                            if (int.TryParse(checkMember.getProperty("numberOfLogins").Value.ToString(), out noLogins))
                            {
                                //Managed to parse it to a number
                                //Don't need to do anything as we have default value of 0
                            }

                            //Update the counter
                            checkMember.getProperty("numberOfLogins").Value = noLogins + 1;

                            //Update label with last login date to now
                            checkMember.getProperty("lastLoggedIn").Value = DateTime.Now.ToString("dd/MM/yyyy @ HH:mm:ss");

                            //Update label with last logged in IP address & Host Name
                            string hostName         = Dns.GetHostName();
                            string clientIPAddress  = Dns.GetHostAddresses(hostName).GetValue(0).ToString();

                            checkMember.getProperty("hostNameOfLastLogin").Value    = hostName;
                            checkMember.getProperty("IPofLastLogin").Value          = clientIPAddress;

                            //Save the details
                            checkMember.Save();

                            //If they have verified then lets log them in
                            //Set Auth cookie
                            FormsAuthentication.SetAuthCookie(model.EmailAddress, true);

                            //Once logged in - redirect them back to the return URL
                            return new RedirectResult(model.ReturnUrl);
                        }
                        else
                        {
                            //User has not verified their email yet
                            ModelState.AddModelError("LoginForm.", "Email account has not been verified");
                            return CurrentUmbracoPage();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginForm.", "Invalid details");
                    return CurrentUmbracoPage();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoginForm.", "Error: " + ex.ToString());
                return CurrentUmbracoPage();
            }

            //In theory should never hit this, but you never know...
            return RedirectToCurrentUmbracoPage();
        }


        //Register


        //Forgotten Password


        //Reset Password


        //Verify EEmail
    }
}