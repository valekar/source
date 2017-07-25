using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Stuart_V2.Models.Entities;

namespace Stuart_V2.Models
{
    //srini
    public class AuthRoles : ActionFilterAttribute
    {
        Logger log = LogManager.GetCurrentClassLogger();

        string[] AllowedTypes;

        public AuthRoles(params string[] allowedTypes)
        {
            this.AllowedTypes = allowedTypes;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool roleExists = false;
            IPrincipal p = HttpContext.Current.User;

            //try
            //{
            //    FormsIdentity id = (System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity;
            //}
            //catch
            //{
            //    System.Security.Principal.WindowsIdentity id = (System.Security.Principal.WindowsIdentity) HttpContext.Current.User.Identity;
                
            //}
            
            //FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;

            string UsrData = ""; // id.Ticket.UserData.ToString().ToUpper();

          //*****Identity Version ******//
            if (p.Identity.IsAuthenticated)
            {
                /*if (p.Identity is WindowsIdentity)
                {
                    FormsAuthentication.SignOut();
                    //Url.Content("~\ ")
                    HttpContext.Current.Response.Redirect("~\\Login\\Index", true);
                }*/
                if (HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName) !=null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    UsrData = ticket.UserData;

                }

                foreach (string str in AllowedTypes)
                {
                    if (UsrData.ToUpper().Contains(str.ToUpper() + ","))
                    {
                        roleExists = true;
                        break;
                    }
                }
                if (roleExists == false)
                {
                    FormsAuthentication.SignOut();
                    //Url.Content("~\ ")
                    HttpContext.Current.Response.Redirect( "~\\Login\\Login?Role=Denied", true);
                }

            }

            //****** COOKIE Version ******//
           /* if (HttpContext.Current.Request.Cookies["UserData"].Value != null)
            {
                UsrData = HttpContext.Current.Request.Cookies["UserData"].Value.ToString();

            }
            if (p.Identity.IsAuthenticated)
            {
                foreach (string str in AllowedTypes)
                {
                    if (UsrData.ToUpper().Contains(str.ToUpper() + ","))
                    {
                        roleExists = true;
                        break;
                    }
                }
                if (roleExists == false)
                {
                    FormsAuthentication.SignOut();
                    //Url.Content("~\ ")
                    HttpContext.Current.Response.Redirect("~\\Login\\Login?Role=Denied", true);
                }
            }*/

        }
    }
}