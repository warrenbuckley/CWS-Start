using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using CWSStart.Web.CWSExtensions;
using CWSStart.Web.Models;
using CWSStart.Web.Pocos;
using umbraco.cms.businesslogic.member;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace CWSStart.Web.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {

        public ActionResult RenderContactForm()
        {
            //Generate a new instance of the ContactForm View Model, as we may want to pre-populate it
            var contactFormModel = new ContactFormViewModel();

            if (Member.IsLoggedOn())
            {
                //If we are logged on - get the current member
                var currentMember = Member.GetCurrentMember();

                //Pre-populate the form with the user's name from their member profile
                contactFormModel.Name = currentMember.Text;
            }

            //Return our partial view & pass our model
            return PartialView("ContactForm", contactFormModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleContactForm(ContactFormViewModel model)
        {
            //Check if data posted from form in model is valid
            if (!ModelState.IsValid)
            {
                //Not valid return to the form with the values the user entered
                return CurrentUmbracoPage();
            }

            //Get settings off current node (CWS-Contact-Form) node
            var emailTo         = CurrentPage.GetPropertyValue("emailAddressTo", "warren@creativewebspecialist.co.uk").ToString();
            var emailSubject    = CurrentPage.GetPropertyValue("emailSubject", "CWS Contact Form Request").ToString();

            //Send out email
            EmailHelper.SendContactEmail(model, emailTo, emailSubject);


            //Now let's add it to our DB table using the magical PetaPoco for a handy log
            var db = ApplicationContext.DatabaseContext.Database;

            //Create a new PetaPoco object representing our DB table
            var logEntry        = new ContactForm();
            logEntry.Date       = DateTime.Now;
            logEntry.Name       = model.Name;
            logEntry.Email      = model.Email;
            logEntry.Message    = model.Message;


            try
            {
                //Insert the record into the DB
                db.Insert(logEntry);
            }
            catch (Exception ex)
            {
                //Throw an exception if there is a problem adding the item to the DB table for logging with PetaPoco
                throw ex;
            }


            //Update success flag (in a TempData key)
            TempData["IsSuccessful"] = true;

            //All done - lets redirect to the current page & show our thanks/success message
            return RedirectToCurrentUmbracoPage();

        }
    }
}