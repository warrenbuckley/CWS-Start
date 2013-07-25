using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.cms.businesslogic.member;
using Umbraco.Core;

namespace CWSStart.Web.CWSExtensions
{
    public class RegisterStartupEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Umbraco App is starting up...

            //Ensure our custom member type & it's properties are setup in Umbraco
            //If not let's create it


            //Create Contact Form Logging DB Table with Umbraco flavoured PetaPoco

        }
        
    }
}