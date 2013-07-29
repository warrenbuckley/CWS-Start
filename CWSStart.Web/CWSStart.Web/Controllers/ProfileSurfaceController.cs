using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWSStart.Web.Models;
using umbraco.cms.businesslogic.member;
using Umbraco.Web.Mvc;

namespace CWSStart.Web.Controllers
{
    public class ProfileSurfaceController : SurfaceController
    {
        /// <summary>
        /// Renders the Login view
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult RenderEditProfile()
        {
            ProfileViewModel profileModel = new ProfileViewModel();

            //If user is logged in then let's pre-populate the model
            if (Member.IsLoggedOn())
            {
                //Let's fill it up
                Member currentMember = Member.GetCurrentMember();

                profileModel.Name           = currentMember.Text;
                profileModel.EmailAddress   = currentMember.Email;
                profileModel.MemberID       = currentMember.Id;
                profileModel.Description    = currentMember.getProperty("description").Value.ToString();
                profileModel.ProfileURL     = currentMember.getProperty("profileURL").Value.ToString();
                profileModel.Twitter        = currentMember.getProperty("twitter").Value.ToString();
                profileModel.LinkedIn       = currentMember.getProperty("linkedIn").Value.ToString();
                profileModel.Skype          = currentMember.getProperty("skype").Value.ToString();
            }
            else
            {
                //They are not logged in, redirect to home
                return Redirect("/");
            }

            //Pass the model to the view
            return PartialView("EditProfile", profileModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleEditProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //Update the member with our data & save it down
            //Using member ID and not email address in case member has changed their email
            Member updateMember = new Member(model.MemberID);

            updateMember.Text                               = model.Name;
            updateMember.Email                              = model.EmailAddress;
            updateMember.getProperty("description").Value   = model.Description;
            updateMember.getProperty("profileURL").Value    = model.ProfileURL;
            updateMember.getProperty("twitter").Value       = model.Twitter;
            updateMember.getProperty("linkedIn").Value      = model.LinkedIn;
            updateMember.getProperty("skype").Value         = model.Skype;

            //Save the member
            updateMember.Save();

            //Return the view
            return RedirectToCurrentUmbracoPage();
        }



        public ActionResult RenderMemberProfile(string profileURLtoCheck)
        {
            //Try and find member with the QueryString value ?profileURLtoCheck=warrenbuckley
            Member findMember = Member.GetAllAsList().FirstOrDefault(x => x.getProperty("profileURL").Value.ToString() == profileURLtoCheck);

            //Create a view model
            ViewProfileViewModel profile = new ViewProfileViewModel();

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

                //Got the member lets bind the data to the view model
                profile.Name                    = findMember.Text;
                profile.MemberID                = findMember.Id;
                profile.EmailAddress            = findMember.Email;
                profile.MemberType              = findMember.Groups.Values.Cast<MemberGroup>().First().Text;

                profile.Description             = findMember.getProperty("description").Value.ToString();

                profile.LinkedIn                = findMember.getProperty("linkedIn").Value.ToString();
                profile.Skype                   = findMember.getProperty("skype").Value.ToString();
                profile.Twitter                 = findMember.getProperty("twitter").Value.ToString();

                profile.NumberOfLogins          = Convert.ToInt32(findMember.getProperty("numberOfLogins").Value.ToString());
                profile.LastLoginDate           = DateTime.ParseExact(findMember.getProperty("lastLoggedIn").Value.ToString(), "dd/MM/yyyy @ HH:mm:ss", null);
                profile.NumberOfProfileViews    = Convert.ToInt32(findMember.getProperty("numberOfProfileViews").Value.ToString());

            }
            else
            {
                //Couldn't find the member return a 404
                return new HttpNotFoundResult("The member profile does not exist");
            }

            return PartialView("ViewProfile", profile);

        }

        //REMOTE Validation
        public JsonResult CheckEmailIsUsed(string emailAddress)
        {
            //Get Current Member
            var member = Member.GetCurrentMember();

            //Sometimes inconsistent results with GetCurrent Member, unsure why?!
            if (member != null)
            {

                //if the email is the same as the one stored then it's OK
                if (member.Email == emailAddress)
                {
                    //Email is the same as one currently stored on the member - so email ok to use & rule valid (return true, back to validator)
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                //Try and get member by email typed in
                var checkEmail = Member.GetMemberFromEmail(emailAddress);

                if (checkEmail != null)
                {
                    return Json(String.Format("The email address '{0}' is already in use.", emailAddress),
                                JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            //Unable to get current member to check (just an OK for client side validation)
            //and let action in controller validate
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckProfileURLAvailable(string profileURL)
        {
            //Get Current Member
            var member = Member.GetCurrentMember();

            //Sometimes inconsistent results with GetCurrent Member, unsure why?!
            if (member != null)
            {
                if (member.getProperty("profileURL").Value.ToString() == profileURL)
                {
                    //profile URL is the same as one currently stored - so it's ok to use & rule valid (return true, back to validator)
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                //Get all members where profileURL property = value from Model
                Member checkProfileURL =
                    Member.GetAllAsList().FirstOrDefault(x => x.getProperty("profileURL").Value.ToString() == profileURL);

                //Check not null if not null then its got one in the system already
                if (checkProfileURL != null)
                {
                    return Json(String.Format("The profile URL '{0}' is already in use.", profileURL),
                                JsonRequestBehavior.AllowGet);
                }


                // no profile has this url so its all good in the hood
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            //Unable to get current member to check (just an OK for client side validation)
            //and let action in controller validate
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}