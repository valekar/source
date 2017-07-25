using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Orgler.Security
{
    public static class Extentions
    {
        public static string GetUserName(this string strUserName)
        {
            if (strUserName.Contains("\\"))
            {
                int index = strUserName.IndexOf("\\");
                return strUserName.Substring(index + 1);
            }
            return strUserName;
        }

        public static string GetUserName(this IPrincipal ctx)
        {
            if (ctx.Identity is FormsIdentity)
            {
                return ctx.Identity.Name;
            }
            else if (ctx.Identity is WindowsIdentity)
            {
                return ctx.Identity.Name.GetUserName();
            }

            return ctx.Identity.Name;
        }

        public static string GetUserEmail(this IPrincipal ctx)
        {
            if (ctx.Identity is FormsIdentity)
            {
                FormsIdentity formsIdentity = (FormsIdentity)ctx.Identity;
                Dictionary<string, string> userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(formsIdentity.Ticket.UserData);
                return userData["userEmail"].ToString();
            }
            else if (ctx.Identity is WindowsIdentity)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                Dictionary<string, string> userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(ticket.UserData);
                return userData["userEmail"].ToString();
            }

            return ctx.Identity.Name;
        }

        public static string GetUserFullName(this IPrincipal ctx)
        {
            if (ctx.Identity is FormsIdentity)
            {
                FormsIdentity formsIdentity = (FormsIdentity)ctx.Identity;
                Dictionary<string, string> userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(formsIdentity.Ticket.UserData);
                return userData["userName"].ToString();
            }
            else if (ctx.Identity is WindowsIdentity)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                Dictionary<string, string> userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(ticket.UserData);
                return userData["userName"].ToString();
            }

            return ctx.Identity.Name;
        }

        public static string GetUserGrpName(this string strGrpName, IIdentity identity)
        {
            string UsrData = "";
            //string strGrpName = "";
            if (identity is FormsIdentity)
            {
                FormsIdentity id = (FormsIdentity)identity;
                UsrData = id.Ticket.UserData;
            }
            else if (HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName) != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                UsrData = ticket.UserData;

            }

            if (!string.IsNullOrEmpty(UsrData))
            {
                //UsrData = HttpContext.Current.Request.Cookies["UserData"].Value.ToString();
                if (UsrData.ToLower().Contains("admin"))
                {
                    strGrpName = "Orgler Admin";
                }
                else if (UsrData.ToLower().Contains("writer"))
                {
                    strGrpName = "Orgler Writer";
                }
                else if (UsrData.ToLower().Contains("user"))
                {
                    strGrpName = "Orgler";
                }
            }


            return strGrpName;
        }


        public static string GetUserGrpName(this string strGrpName, HttpCookie authCookie)
        {
            string UsrData = "";
            //string strGrpName ="";
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            UsrData = ticket.UserData;
            if (!string.IsNullOrEmpty(UsrData))
            {
                //UsrData = HttpContext.Current.Request.Cookies["UserData"].Value.ToString();
                if (UsrData.ToLower().Contains("admin"))
                {
                    strGrpName = "Orgler Admin";
                }
                else if (UsrData.ToLower().Contains("writer"))
                {
                    strGrpName = "Orgler Writer";
                }
                else if (UsrData.ToLower().Contains("user"))
                {
                    strGrpName = "Orgler";
                }
            }


            return strGrpName;
        }

        public static string GetUserGrpName(this string strGrpName)
        {
            string UsrData = "";
            //string strGrpName ="";
            IPrincipal p = HttpContext.Current.User;           
            if (p.Identity.IsAuthenticated)
            {

                if (HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName) != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    UsrData = ticket.UserData;
                }
            }                  
            if (!string.IsNullOrEmpty(UsrData))
            {
                //UsrData = HttpContext.Current.Request.Cookies["UserData"].Value.ToString();
                if (UsrData.ToLower().Contains("admin"))
                {
                    strGrpName = "Orgler Admin";
                }
                else if (UsrData.ToLower().Contains("writer"))
                {
                    strGrpName = "Orgler Writer";
                }
                else if (UsrData.ToLower().Contains("user"))
                {
                    strGrpName = "Orgler";
                }
            }
            return strGrpName;
        }
        
        public static string GetUserEmail(this string strEmailName, HttpCookie authCookie)
        {
            string UsrData;
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            UsrData = ticket.UserData;
            Dictionary<string, string> userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(UsrData);
            strEmailName = userData["userEmail"].ToString();

            return strEmailName;
        }

    }
}