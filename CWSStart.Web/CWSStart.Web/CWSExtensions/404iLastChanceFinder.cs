using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Umbraco.Web.Routing;

namespace CWSStart.Web.CWSExtensions
{
    public class _404iLastChanceFinder : IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            //Check request is a 404
            if (contentRequest.Is404)
            {
                //Get the home node
                var home = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetAtRoot().Single(x => x.DocumentTypeAlias == "CWS-Home");

                //Get the 404 node
                var notFoundNode = home.Children.Single(x => x.DocumentTypeAlias == "CWS-404");

                //Set Response Status to be HTTP 404
                contentRequest.SetResponseStatus(404, "404 Page Not Found");

                //Set the node to be the not found node
                contentRequest.PublishedContent = notFoundNode;
            }

            //Not sure about this line - copied from Lee K's GIST
            //https://gist.github.com/leekelleher/5966488
            return contentRequest.PublishedContent != null;
        }
    }
}