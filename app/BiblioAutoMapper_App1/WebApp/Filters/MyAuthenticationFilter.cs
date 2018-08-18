using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebApp.Filters
{
    public class MyAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //Check Session is Empty Then set as Result is HttpUnauthorizedResult     
            var cookieValue = "";
            try
            {
                if (filterContext.HttpContext.Request.Cookies["biblio_user_id"] != null)
                    cookieValue = filterContext.HttpContext.Request.Cookies["biblio_user_id"].Value;
            }
            catch { }
            var sessionValue = "";
            if (!string.IsNullOrEmpty(cookieValue))
            {
                try
                {
                    if (filterContext.HttpContext.Session[cookieValue] != null)
                        sessionValue = filterContext.HttpContext.Session[cookieValue].ToString();
                }
                catch { }
            }
            if (string.IsNullOrEmpty(sessionValue))
            {
                var url = filterContext.HttpContext.Request.Url.AbsoluteUri;
                if (url.ToLower().IndexOf("/login") < 0)
                    filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        //Runs after the OnAuthentication method  
        //------------//
        //OnAuthenticationChallenge:- if Method gets called when Authentication or Authorization is 
        //failed and this method is called after
        //Execution of Action Method but before rendering of View
        //------------//
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //We are checking Result is null or Result is HttpUnauthorizedResult 
            // if yes then we are Redirect to Error View
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult
                {
                    ViewData = new ViewDataDictionary
                    {
                        new KeyValuePair<string, object>("unauth",true)
                    },
                    ViewName = "Unauth"
                };
            }
        }
    }
}