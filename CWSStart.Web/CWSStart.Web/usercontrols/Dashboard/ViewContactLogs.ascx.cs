using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CWSStart.Web.Pocos;
using Umbraco.Web.UI.Controls;


namespace CWSStart.Web.usercontrols.Dashboard
{
    public partial class ViewContactLogs : UmbracoUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get the Umbraco DB context
            var db = UmbracoContext.Application.DatabaseContext.Database;

            //Get all ContactForm items from the DB
            var logItems = db.Query<ContactForm>("SELECT * FROM ContactFormLogs");

            //Set the source & bind the repeater to our collection of logItems
            contactLogs.DataSource = logItems;
            contactLogs.DataBind();
        }
    }
}