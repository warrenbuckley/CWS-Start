using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWSStart.Web.CWSExtensions;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace CWSStart.Web.Controllers
{
    public class SiteChromeSurfaceController : SurfaceController
    {

        public ActionResult RenderNavi()
        {
            //Get the homepage node
            var home    = Umbraco.TypedContentAtRoot().SingleOrDefault(x => x.DocumentTypeAlias == ConfigHelper.GetCWSHomeDocTypeAlias());
            var pages   = home.Children;
            
            //Return our collection of pages to the view
            return PartialView("Navi", pages);
        }

    }
}