using NLog;
using Orgler.Controllers.MVC;
using Orgler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orgler.Models.Entities;
using System.Security.Principal;
using System.IO;
using System.Configuration;
using System.Web.Security;
using System.Web.Configuration;

namespace Orgler.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private Logger log = LogManager.GetCurrentClassLogger();
        private string AdmGroup = "";
        private string UsrGroup = "";
        private string WriterGroup = "";
        LDAPAuthentication ldapAuth = null;
        LoginHistoryInput loginHistory = null;
        private string BaseURL = "";

        public LoginController()
        {
            BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
            //AdmGroup = System.Configuration.ConfigurationManager.AppSettings["StuartAdmin"]; //DEPLOYMENT STEP
            AdmGroup = System.Configuration.ConfigurationManager.AppSettings["StuartDevelopmentTeam"];
            UsrGroup = System.Configuration.ConfigurationManager.AppSettings["StuartReader"];
            WriterGroup = System.Configuration.ConfigurationManager.AppSettings["StuartWriter"];
            ldapAuth = new LDAPAuthentication("LDAP://archq.ri.redcross.net");
            ldapAuth.AdminGroup = AdmGroup;
            ldapAuth.UserGroup = UsrGroup;
            ldapAuth.WriterGroup = WriterGroup;
            //below two are used for logging the history
            loginHistory = new LoginHistoryInput();            
        }       

        public ActionResult Index(string returnUrl)
        {
            string UserID = Request.LogonUserIdentity.Name;
            //UserID = "rc\\zack.melvin@redcross.org";
           // commenting this out as this is a hardcoded value - 7/19/2016
            // UserID = "BIO\\BrownJe";
            string userName = getUserName(UserID);

            bool isAuthenticated = false;
            if (ldapAuth.IsGroupsAuthenticated(userName))
            {
                isAuthenticated = true;  
            }
            if (isAuthenticated || ldapAuth.AuthenticatedOnly)
            {
                StoreUserCookie(UserID);
                loginUserHistory(UserID,"Success");
                return RedirectToAction("NewAccount", "Home");
            }
            else
            {
                loginUserHistory(UserID,"Failure");
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            ViewBag.Message = "Invalid User Name or Password";
            
            String acceptRequestType = Request.Headers.Get("Accept");

            if (acceptRequestType.Equals("application/json"))
            {
                Response.Headers.Remove("Accept");
                Response.Headers.Remove("Content-Type");
                Response.Headers.Set("redirect", "true");

            }

            return View("Index");
        }

        private void StoreUserCookie(string UserID)
        {
            String usrAuthType = "";
            string strGrpName = string.Empty;
            if (ldapAuth.IsUser)
            {
                usrAuthType += "User,";              
            }
            if (ldapAuth.IsAdmin)
            {
                usrAuthType += "Admin,";
            }
            if (ldapAuth.IsWriter)
            {
                usrAuthType += "Writer,";
            }


            string userName = getUserName(UserID);            
           log.Info("User " + UserID + " is validated. Roles " + usrAuthType);

           string FullName = ldapAuth.UserFullName;
            if (FullName == "") FullName = UserID.ToUpper();

            bool isCookiePersistent = false;

            System.Web.Security.FormsAuthenticationTicket authTicket =
                new System.Web.Security.FormsAuthenticationTicket(1, ldapAuth.UserFullName, DateTime.Now, DateTime.Now.AddDays(1), isCookiePersistent, usrAuthType, FormsAuthentication.FormsCookiePath);

            //Encrypt the ticket.
            String encryptedTicket = System.Web.Security.FormsAuthentication.Encrypt(authTicket);

            //Create a cookie, and then add the encrypted ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, encryptedTicket);

           if (true == isCookiePersistent)
                authCookie.Expires = authTicket.Expiration;

            //Add the cookie to the outgoing cookies collection.
            Response.Cookies.Add(authCookie);           
                             
        }

        [HttpPost]
        public ActionResult Authenticate(string returnUrl)
        {
            var UserID = Request["email"] ?? "";
            var Password = Request["password"] ?? "";

            if (string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Message = "User Name or Password not entered";
                return View("Index");
            }

            bool isAuthenticated = false;

            if (ldapAuth.IsAuthValid(UserID, Password))
            {
                isAuthenticated = true;
            }
               
            if (isAuthenticated || ldapAuth.AuthenticatedOnly)
            {
                               
                StoreUserCookie(UserID);
                loginUserHistory(UserID,"Success");
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("NewAccount", "Home"); ;
                }
            }
            else
            {
                loginUserHistory(UserID, "Failure");
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            ViewBag.Message = "Invalid User Name or Password";
            return View("Index");
        }

        public ActionResult Login()
        {
            //return View();
            return View("Index");
        }


        private void loginUserHistory(string UserID, string status)
        {
            string userName = getUserName(UserID);
            if (userName != null)
            {
                loginHistory.UserName = userName;
            }
            else
            {
                loginHistory.UserName = "<Username not available>";
            }


            loginHistory.LoginStatus = status;
            loginHistory.ActiveDirectoryGroupName = getUserGrpName();
               // Change this to constituent controller 

            //Web Service insertloginhistory is decorated with [AllowAnonymous].  So, you don't need to pass Token and CliedID
            string url = BaseURL + "api/Login/insertloginhistory/";
            System.Threading.Tasks.Task.Run(async () => await Models.Resource.PostResourceAsync(url, "", loginHistory, "")); 

            //if (!string.IsNullOrEmpty(Token))
            //{
            //   string url = BaseURL + "api/Login/insertloginhistory/";
            //   System.Threading.Tasks.Task.Run(async ()=> await Models.Resource.PostResourceAsync(url, Token, loginHistory, ClientID));              
            //}
        }


        private string getUserName(string UserID)
        {
            if (UserID.Contains("\\"))
            {
                int index = UserID.IndexOf("\\");
                return UserID.Substring(index + 1);
            }
            return UserID;
        }


        private string getUserGrpName()
        {
            string strGrpName = string.Empty;
            if (ldapAuth.IsUser)
            {
                strGrpName = ldapAuth.UserGroup;
            }
            if (ldapAuth.IsAdmin)
            {
                strGrpName = ldapAuth.AdminGroup;
            }
            if (ldapAuth.IsWriter)
            {
                strGrpName = ldapAuth.WriterGroup;
            }

            return strGrpName;
        }

    }
}