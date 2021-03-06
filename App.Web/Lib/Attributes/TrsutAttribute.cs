﻿using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web.Lib.Attributes
{
    public class TrustAttribute : AuthorizeAttribute
    {
        public string Privilege { get; set; }

        /// <summary>
        /// Override the HandleUnauthorizedRequest atrtibute with custom actions.
        /// </summary>
        /// <param name="ctx"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext ctx)
        {
            if (!ctx.HttpContext.User.Identity.IsAuthenticated)
            {
                // The request is not authenticated.
                ctx.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Auth",
                    action = "SignIn"
                }));
            }
            else
            {
                // The request is authenticated, but access has been refused.
                ctx.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "Http403"
                }));
            }
        }

        /// <summary>
        /// Override the AuthorizeCore attribute with custom actions.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase ctx)
        {
            // Is the request authenticated?
            var isAuthorized = ctx.User.Identity.IsAuthenticated;

            // Grab the identity of the current request.
            var identity = (ClaimsPrincipal) Thread.CurrentPrincipal;

            // Check if the request has the required permissions.
            var doesUserHaveRequiredRole = identity.Claims.Count(c => c.Type == ClaimTypes.Role && c.Value == Privilege) > 0;
            if (isAuthorized && Privilege != null)
            {
                return doesUserHaveRequiredRole;
            }
            return isAuthorized;
        }
    }
}