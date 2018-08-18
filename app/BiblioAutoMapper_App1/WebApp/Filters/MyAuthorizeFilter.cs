using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Filters
{
    public class MyAuthorizeFilter : AuthorizeAttribute
    {
        string[] _users;

        public MyAuthorizeFilter(params string[] authUsers)
        {
            //_users = !string.IsNullOrEmpty(authUsers) ? authUsers.Split(',') : null;
            _users = authUsers;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {            
            if (httpContext.Request.IsLocal)
            {
                if (_users != null && _users.Length > 0)
                {
                    var cookieValue = "";
                    try
                    {
                        if (httpContext.Request.Cookies["biblio_user_id"] != null)
                            cookieValue = httpContext.Request.Cookies["biblio_user_id"].Value;
                    }
                    catch { }
                    var sessionValue = "";
                    if (!string.IsNullOrEmpty(cookieValue))
                    {
                        try
                        {
                            if (httpContext.Session[cookieValue] != null)
                                sessionValue = httpContext.Session[cookieValue].ToString();
                        }
                        catch { }
                    }
                    if (!string.IsNullOrEmpty(sessionValue))
                    {
                        var usuario = JsonConvert.DeserializeObject<dynamic>(sessionValue);
                        string nome = usuario.Nome;
                        if (_users.Count(x => x == nome) == 0)
                            return false;
                    }
                }
            }

            return true;
        }
    }
}