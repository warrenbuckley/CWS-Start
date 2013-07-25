using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return CurrentUmbracoPage();
        }


        //Register


        //Forgotten Password


        //Reset Password


        //Verify EEmail
    }
}