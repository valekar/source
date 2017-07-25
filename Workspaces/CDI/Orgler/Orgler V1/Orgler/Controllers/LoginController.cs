using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using System.Security;
using System.Configuration;
using System.Web.Script.Serialization;
using Orgler.Models.Login;
using Orgler.Security;
using System.Web.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.Threading.Tasks;
using Orgler.Services;
using Newtonsoft.Json;

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
            // AdmGroup = System.Configuration.ConfigurationManager.AppSettings["StuartAdmin"]; //DEPLOYMENT STEP
            AdmGroup = System.Configuration.ConfigurationManager.AppSettings["OrglerAdmin"];
            UsrGroup = System.Configuration.ConfigurationManager.AppSettings["OrglerReader"];
            WriterGroup = System.Configuration.ConfigurationManager.AppSettings["OrglerWriter"];
            ldapAuth = new LDAPAuthentication("LDAP://archq.ri.redcross.net");
            ldapAuth.AdminGroup = AdmGroup;
            ldapAuth.UserGroup = UsrGroup;
            ldapAuth.WriterGroup = WriterGroup;
            loginHistory = new LoginHistoryInput();
        }

        public ActionResult Index(string returnUrl)
        {
            string UserID = Request.LogonUserIdentity.Name;
            //UserID = "rc\\zack.melvin@redcross.org";
            // commenting this out as this is a hardcoded value - 7/19/2016
            //UserID = "ARCHQ\\aj.jones";
            string userName = getUserName(UserID);

            bool isAuthenticated = false;
            if (ldapAuth.IsGroupsAuthenticated(userName))
            {
                isAuthenticated = true;
            }
            if (isAuthenticated || ldapAuth.AuthenticatedOnly)
            {
                StoreUserCookie(UserID);

                //loginUserHistory(UserID, "Success"); 
                TempData["FromLogin"] = "Y";              
                TempData["returnUrl"] = returnUrl;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                loginUserHistory(UserID, "Failure");
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
            if (ldapAuth.IsWriter)
            {
                usrAuthType += "Writer,";
            }
            if (ldapAuth.IsAdmin)
            {
                usrAuthType += "Admin,";
            }        

            string userName = getUserName(UserID);
            log.Info("User " + UserID + " is validated. Roles " + usrAuthType);

            string FullName = ldapAuth.UserFullName;
            if (FullName == "") FullName = UserID.ToUpper();
            Dictionary<string, string> userDict = new Dictionary<string, string>();
            userDict.Add("usrAuthType", usrAuthType);
            userDict.Add("userEmail", ldapAuth.EmailAddress); //Keerthana - 18-Apr-2017 - userDict.Add("userEmail", ldapAuth.UserName);
            userDict.Add("userName", ldapAuth.UserFullName);
            string userData = JsonConvert.SerializeObject(userDict);

            bool isCookiePersistent = false;

            System.Web.Security.FormsAuthenticationTicket authTicket =
                new System.Web.Security.FormsAuthenticationTicket(1, ldapAuth.UserFullName, DateTime.Now, DateTime.Now.AddDays(1), isCookiePersistent, userData, FormsAuthentication.FormsCookiePath);

            //Encrypt the ticket.
            String encryptedTicket = System.Web.Security.FormsAuthentication.Encrypt(authTicket);

            //Create a cookie, and then add the encrypted ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, encryptedTicket);

            if (true == isCookiePersistent)
                authCookie.Expires = authTicket.Expiration;

            //Add the cookie to the outgoing cookies collection.
            Response.Cookies.Add(authCookie);
            //UpdateUserProfile usrProfile = new UpdateUserProfile();
            //usrProfile.updateUserProfile(UserID,authCookie);
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
                string s1 = User.Identity.Name.ToUpper();
                string s2 = Request.LogonUserIdentity.Name;
                StoreUserCookie(UserID);
                loginUserHistory(UserID, "Success");
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    //return RedirectToAction("Index", "Home");
                    TempData["returnUrl"] = returnUrl;
                    return RedirectToAction("Constituent", "Home");
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
            string url = BaseURL + "api/adminservice/insertloginhistory/";
            System.Threading.Tasks.Task.Run(async () => await Services.InvokeWebService.PostResourceAsync(url, "", loginHistory, ""));

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
            if (ldapAuth.IsWriter)
            {
                strGrpName = ldapAuth.WriterGroup;
            }
            if (ldapAuth.IsAdmin)
            {
                strGrpName = ldapAuth.AdminGroup;
            }

            return strGrpName;
        }
        private string getUserEmailAddress()
        {
            string strUserEmailAddress = string.Empty;
            if (ldapAuth.IsUser)
            {
                strUserEmailAddress = ldapAuth.EmailAddress;
            }
            if (ldapAuth.IsWriter)
            {
                strUserEmailAddress = ldapAuth.EmailAddress;
            }
            if (ldapAuth.IsAdmin)
            {
                strUserEmailAddress = ldapAuth.EmailAddress;
            }

            return strUserEmailAddress;
        }
        //update usr_prfl table
        //private async Task updateUserProfile(string UserID)
        //{
        //    try
        //    {


        //        await Task.Run(() =>
        //        {
        //            List<TabLevelSecurityParams> tabLevelSecurityListData =
        //          Utility.readJSONTFromFile<List<TabLevelSecurityParams>>(System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"]);

        //            string userOrginalGrpName = getUserGrpName();
        //            string userEmailAddress = getUserEmailAddress();
        //            string[] userName = null;
        //            string userID = string.Empty;
        //            string profileGrpName = string.Empty;
        //            if (UserID.Contains("@"))
        //            {
        //                userName = UserID.Split('@');
        //                userID = userName[0];
        //            }
        //            else { userID = UserID; }
        //           if(tabLevelSecurityListData!=null)
        //            { 
        //            TabLevelSecurityParams tabSecurityParam = tabLevelSecurityListData.Find(x => x.usr_nm == getUserName(userID));
        //                if (tabSecurityParam != null)
        //                {
        //                    if (!string.IsNullOrEmpty(tabSecurityParam.grp_nm))
        //                    {
        //                        profileGrpName = tabSecurityParam.grp_nm;
        //                    }

        //                    if (!(userOrginalGrpName??"").ToLower().Equals((profileGrpName??"").ToLower()))
        //                    {
        //                        tabSecurityParam.grp_nm = userOrginalGrpName;


        //                        Orgler.Models.Admin.AdminPostInput adminPostInput = new Models.Admin.AdminPostInput();
        //                        //TabLevelSecurityParams adminInput = new TabLevelSecurityParams();
        //                        Orgler.Models.Admin.Admin adminInput = new Models.Admin.Admin();
        //                        if (userOrginalGrpName == "Orgler Admin")
        //                        {
        //                            adminInput.usr_nm = tabSecurityParam.usr_nm;
        //                            adminInput.grp_nm = userOrginalGrpName;
        //                            adminInput.telephone_number = tabSecurityParam.telephone_number;
        //                            adminInput.email_address = userEmailAddress;
        //                            adminInput.newaccount_tb_access = "RW";
        //                            adminInput.topaccount_tb_access = "RW";
        //                            adminInput.enterprise_orgs_tb_access = "RW";
        //                            adminInput.constituent_tb_access = "RW";
        //                            adminInput.transaction_tb_access = "RW";
        //                            adminInput.admin_tb_access = "RW";
        //                            adminInput.has_merge_unmerge_access = "true";
        //                            adminInput.help_tb_access = "RW";
        //                            adminInput.is_approver = "1";
        //                            adminPostInput.adminInput = adminInput;
        //                        }
        //                        else if (userOrginalGrpName == "Orgler Writer")
        //                        {
        //                            adminInput.usr_nm = tabSecurityParam.usr_nm;
        //                            adminInput.grp_nm = userOrginalGrpName;
        //                            adminInput.telephone_number = tabSecurityParam.telephone_number;
        //                            adminInput.email_address = userEmailAddress;
        //                            adminInput.newaccount_tb_access = "RW";
        //                            adminInput.topaccount_tb_access = "RW";
        //                            adminInput.enterprise_orgs_tb_access = "N";
        //                            adminInput.constituent_tb_access = "RW";
        //                            adminInput.transaction_tb_access = "RW";
        //                            adminInput.admin_tb_access = "N";
        //                            adminInput.has_merge_unmerge_access = "true";
        //                            adminInput.help_tb_access = "RW";
        //                            adminInput.is_approver = "1";
        //                            adminPostInput.adminInput = adminInput;
        //                        }
                               
        //                        else if (userOrginalGrpName == "Orgler")
        //                        {
        //                            adminInput.usr_nm = tabSecurityParam.usr_nm;
        //                            adminInput.grp_nm = userOrginalGrpName;
        //                            adminInput.telephone_number = tabSecurityParam.telephone_number;
        //                            adminInput.email_address = userEmailAddress;
        //                            adminInput.newaccount_tb_access = "R";
        //                            adminInput.topaccount_tb_access = "R";
        //                            adminInput.enterprise_orgs_tb_access = "N";
        //                            adminInput.constituent_tb_access = "R";
        //                            adminInput.transaction_tb_access = "R";
        //                            adminInput.admin_tb_access = "N";
        //                            adminInput.has_merge_unmerge_access = "false";
        //                            adminInput.help_tb_access = "R";
        //                            adminInput.is_approver = "0";
        //                            adminPostInput.adminInput = adminInput;
        //                        }


        //                        string url = BaseURL + "api/adminservice/edituserprofile/";
        //                        System.Threading.Tasks.Task.Run(async () => await InvokeWebService.PostResourceAsync(url, "", adminPostInput, ""));
        //                        //System.Threading.Tasks.Task.Run(async () => await Models.Resource.PostResourceAsync(url, "", adminPostInput, ""));

        //                    }
        //                }
        //            }

        //        });
        //    }
        //    catch (Exception ex)
        //    {

        //    }


        //}

    }
}