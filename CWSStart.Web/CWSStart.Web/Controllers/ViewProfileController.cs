using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWSStart.Web.Models;
using umbraco.cms.businesslogic.member;
using Umbraco.Core.Models;
using umbraco.MacroEngines;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace CWSStart.Web.Controllers
{
    public class ViewProfileController : RenderMvcController
    {
        public ViewProfileController() : this(UmbracoContext.Current)
        {            
        }

        public ViewProfileController(UmbracoContext umbracoContext) : base(umbracoContext)
        {
        }

        public override ActionResult Index(RenderModel model)
        {
            //Get profileURLtoCheck
            string profileURLtoCheck = Request.RequestContext.RouteData.Values["profileURLtoCheck"].ToString();

            //Create a view model
            ViewProfileViewModel profile = new ViewProfileViewModel();

            //Check we have a value in the URL
            if (!String.IsNullOrEmpty(profileURLtoCheck))
            {
                //Try and find member with the QueryString value ?profileURLtoCheck=warrenbuckley
                Member findMember = Member.GetAllAsList().FirstOrDefault(x => x.getProperty("profileURL").Value.ToString() == profileURLtoCheck);

                //Check if we found member
                if (findMember != null)
                {
                    //Increment profile view counter by one
                    int noOfProfileViews = 0;
                    int.TryParse(findMember.getProperty("numberOfProfileViews").Value.ToString(), out noOfProfileViews);

                    //Increment counter by one
                    findMember.getProperty("numberOfProfileViews").Value = noOfProfileViews + 1;
                    
                    //Save it down to the member
                    findMember.Save();

                    int noOfLogins = 0;
                    int.TryParse(findMember.getProperty("numberOfLogins").Value.ToString(), out noOfLogins);

                    //Got the member lets bind the data to the view model
                    profile.Name                    = findMember.Text;
                    profile.MemberID                = findMember.Id;
                    profile.EmailAddress            = findMember.Email;

                    profile.Description             = findMember.getProperty("description").Value.ToString();

                    profile.LinkedIn                = findMember.getProperty("linkedIn").Value.ToString();
                    profile.Skype                   = findMember.getProperty("skype").Value.ToString();
                    profile.Twitter                 = findMember.getProperty("twitter").Value.ToString();

                    profile.NumberOfLogins          = noOfLogins;
                    profile.LastLoginDate           = DateTime.ParseExact(findMember.getProperty("lastLoggedIn").Value.ToString(), "dd/MM/yyyy @ HH:mm:ss", CultureInfo.InvariantCulture);
                    profile.NumberOfProfileViews    = noOfProfileViews;

                }
                else
                {
                    //Couldn't find the member return a 404
                    return new HttpNotFoundResult("The member profile does not exist");
                }
            }
            else
            {
                //Couldn't find the member return a 404
                return new HttpNotFoundResult("No profile URL parameter was provided");
            }

            

            //Return template with our profile model
            return CurrentTemplate(profile);
        }
    }
}
