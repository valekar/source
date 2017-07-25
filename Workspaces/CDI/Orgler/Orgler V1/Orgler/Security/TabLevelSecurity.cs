using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Routing;
using System.Threading.Tasks;
using NLog;
using Orgler.Controllers;
using Orgler.Exceptions;
using System.Net.Http;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace Orgler.Security
{
    [HandleException]
    public class TabLevelSecurity : AuthorizeAttribute
    {

        private Logger log = LogManager.GetCurrentClassLogger();
        LDAPAuthentication ldapAuth =null;

        //Varible to store the Tab name same as the column name in DB
        string ActionTab;
        //Varible to store the permissions pertaining to the Action method
        string[] ActionPermission;

        public TabLevelSecurity(string actionTab, params string[] actionPermission)
        {
            this.ActionTab = actionTab;
            this.ActionPermission = actionPermission;
            ldapAuth = new LDAPAuthentication("LDAP://archq.ri.redcross.net");
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
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                bool tabPermissionExists = false;
                IPrincipal p = HttpContext.Current.User;

                string UsrName = string.Empty;
                UsrName = p.GetUserName();

                //UsrName = p.Identity.Name;
                //if (UsrName.Contains("\\"))
                //{
                //    int index = UsrName.IndexOf("\\");
                //    UsrName = UsrName.Substring(index + 1);
                //}
                string tabLevelSecureFilePath = ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"];
                List<TabLevelSecurityParams> listTabLevelSecurity = Utility.getTabLevelSecurityList();
                //Check if the use is authenticated
                if (p.Identity.IsAuthenticated)
                {
                    //Filter only the tab level access entries for the logged in user belonging to the right AD group
                    string UsrGrp = "";
                    string actDirGroupName = "";
                    string actDirUserEmailAddress = "";
                    Security.TabLevelSecurityParams TabLevelSecurityCurrentUser = new Security.TabLevelSecurityParams();
                    if (listTabLevelSecurity.Exists(x => x.usr_nm.ToString().ToLower() == UsrName.ToLower()))
                    {
                        TabLevelSecurityCurrentUser = listTabLevelSecurity.AsEnumerable().Where(x => (x.usr_nm.ToString().ToLower() == UsrName.ToLower())).Select(x => x).FirstOrDefault();
                    }
                    //else
                    //{
                    //    //Check if Group Name returns null or not
                    //    string UsrData = "";

                    //    if (HttpContext.Current.User.Identity is FormsIdentity)
                    //    {
                    //        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    //        UsrData = id.Ticket.UserData;
                    //    }
                    //    else if (HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName) != null)
                    //    {
                    //        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                    //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    //        UsrData = ticket.UserData;

                    //    }

                    //    if (!string.IsNullOrEmpty(UsrData))
                    //    {
                    //        //UsrData = HttpContext.Current.Request.Cookies["UserData"].Value.ToString();

                    //        if (UsrData.ToLower().Contains("user"))
                    //        {
                    //            UsrGrp = "Orgler";
                    //        }
                    //        if (UsrData.ToLower().Contains("writer"))
                    //        {
                    //            UsrGrp = "Orgler Writer";
                    //        }
                    //        if (UsrData.ToLower().Contains("admin"))
                    //        {
                    //            UsrGrp = "Orgler Admin";
                    //        }


                    //    }
                    //    if (UsrGrp != "")
                    //    {
                    //        actDirGroupName = UsrGrp;
                    //        actDirUserEmailAddress = getUserEmailAddress();
                    //        Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();

                    //        //Since the user has been authenticated but not present in the Tab Level Security file, we will create an entry for the user in the file and also in the database.
                    //        dictTabInsertionReturns = insertTabLevelSecturityPrevileges(listTabLevelSecurity, actDirGroupName, UsrName, tabLevelSecureFilePath).Result;

                    //        if (dictTabInsertionReturns["UpdatedTabLevelSecurityList"] != null)
                    //        {
                    //            listTabLevelSecurity = (List<Security.TabLevelSecurityParams>)dictTabInsertionReturns["UpdatedTabLevelSecurityList"];
                    //            TabLevelSecurityCurrentUser = listTabLevelSecurity.AsEnumerable().Where(x => (x.usr_nm.ToString() == UsrName)).Select(x => x).FirstOrDefault();
                    //        }
                    //    }
                    //}


                    string strPermission = string.Empty;
                    string strAddPermission = string.Empty;

                    //Loop through the tab level entries
                    //Only one record per user will be present and hence the list will have only one row
                    if (TabLevelSecurityCurrentUser != null)
                    {
                        switch (ActionTab)
                        {
                            case "newaccount_tb_access": strPermission = TabLevelSecurityCurrentUser.newaccount_tb_access ?? "N"; break;
                            case "topaccount_tb_access": strPermission = TabLevelSecurityCurrentUser.topaccount_tb_access ?? "N"; break;
                            case "enterprise_orgs_tb_access": strPermission = TabLevelSecurityCurrentUser.enterprise_orgs_tb_access ?? "N"; break;
                            case "constituent_tb_access": strPermission = TabLevelSecurityCurrentUser.constituent_tb_access ?? "N"; break;
                            case "transaction_tb_access": strPermission = TabLevelSecurityCurrentUser.transaction_tb_access ?? "N"; break;
                            case "admin_tb_access": strPermission = TabLevelSecurityCurrentUser.admin_tb_access ?? "N"; break;
                            case "help_tb_access": strPermission = TabLevelSecurityCurrentUser.help_tb_access ?? "N"; break;
                            case "upload_eosi_tb_access": strPermission = TabLevelSecurityCurrentUser.upload_eosi_tb_access ?? "N"; break;
                            case "upload_affil_tb_access": strPermission = TabLevelSecurityCurrentUser.upload_affil_tb_access ?? "N"; break;
                            case "upload_eo_tb_access": strPermission = TabLevelSecurityCurrentUser.upload_eo_tb_access ?? "N"; break;  
                            case "has_merge_unmerge_access":
                                strPermission = TabLevelSecurityCurrentUser.has_merge_unmerge_access ?? "N";
                                strAddPermission = TabLevelSecurityCurrentUser.constituent_tb_access ?? "N";
                                break;
                            case "is_approver":
                                strPermission = TabLevelSecurityCurrentUser.is_approver ?? "N";
                                strAddPermission = TabLevelSecurityCurrentUser.transaction_tb_access ?? "N";
                                break;
                        }
                    }


                    //Loop through all the permission attribute parameters
                    foreach (string str in ActionPermission)
                    {
                        //Check if any of the permission attribute parameters is matching with the entry in the table 
                        if (strPermission.ToString().ToUpper().Contains(str.ToUpper()))
                        {
                            //Check if merge/unmerge/approver check is performed which inturn includes additional check for the tab level permission
                            if (ActionTab == "has_merge_unmerge_access" || ActionTab == "is_approver")
                            {
                                if (strAddPermission == "RW")
                                {
                                    //Set the flag
                                    tabPermissionExists = true;
                                    break;
                                }
                            }
                            else
                            {
                                //Set the flag
                                tabPermissionExists = true;
                                break;
                            }
                        }
                    }


                    //If the flag is not set meaning the logged in user does not meet the required permissions to access the API method
                    if (tabPermissionExists == false)
                    {

                        filterContext.Result = new RedirectToRouteResult(new
                                                RouteValueDictionary(new { controller = "General", action = "Unauthorized" }));

                    }

                }

            }
            catch (CustomExceptionHandler ex)
            {
            }
            finally
            {

            }

        }

    }
    public class UpdateUserProfile
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        private string _token = "";
        private string _clientid = "";
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
        public string CleintId
        {
            get { return _clientid; }
            set { _clientid = value; }
        }
        public async Task updateUserProfile()
        {
            IPrincipal p = HttpContext.Current.User;
            if (p!= null && p.Identity.IsAuthenticated)
            {
                string UserId = p.Identity.Name;
                if (HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName) != null)
                {
                    var authCookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                    await updateUserProfile(UserId, authCookie);
                }
                else
                {
                    log.Info("FormsCookieName for " + UserId + " - not found.");
                }
            }
            else
            {
                log.Info("UpdateUserProfile - User is not authenticated.");
            }
        }

        public async Task updateUserProfile(string UserID)
        {
            try
            {
                var tabLevelSecureFilePath = System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"];
                List<TabLevelSecurityParams> tabLevelSecurityListData = new List<TabLevelSecurityParams>();
                tabLevelSecurityListData = Utility.readJSONTFromFile<List<TabLevelSecurityParams>>(tabLevelSecureFilePath);
                HttpCookie authCookie = null;
                IPrincipal p = HttpContext.Current.User;
                if (p!= null && p.Identity.IsAuthenticated)
                {

                    if (HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName) != null)
                    {
                        authCookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                        await updateUserProfile(UserID, authCookie);
                    }
                    else
                    {
                        log.Info("FormsCookieName - not found.");
                    }
                }
                else
                {
                    log.Info("UpdateUserProfile for " + UserID + " is not authenticated.");
                }
            }
            catch (Exception ex)
            {
                log.Info("Error UpdateProfile: " + ex.Message);
            }
        }
        public async Task updateUserProfile(string UserID, HttpCookie authCookie)
        {
            try
            {
                UserID = UserID.GetUserName();
                var tabLevelSecureFilePath = System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"];
                List<TabLevelSecurityParams> tabLevelSecurityListData = new List<TabLevelSecurityParams>();
                tabLevelSecurityListData = Utility.readJSONTFromFile<List<TabLevelSecurityParams>>(tabLevelSecureFilePath);

                string strGrpName = string.Empty;
                string userOrginalGrpName = strGrpName.GetUserGrpName(authCookie);
                string userName = UserID.GetUserName();

                string[] UserName = null;
                string userID = string.Empty;              
                if (UserID.Contains("@"))
                {
                    UserName = UserID.Split('@');
                    userID = UserName[0];
                }
                else { userID = UserID; }

                TabLevelSecurityParams tabSecurityParam = new TabLevelSecurityParams();
                tabSecurityParam = tabLevelSecurityListData.Find(x => x.usr_nm == getUserName(userID));


                if (tabSecurityParam == null)
                {

                    string userEmail = string.Empty;
                    userEmail = userEmail.GetUserEmail(authCookie);
                    //string userEmailAddress = getUserEmailAddress();


                    //Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();

                    //Since the user has been authenticated but not present in the Tab Level Security file, we will create an entry for the user in the file and also in the database.
                    await insertTabLevelSecurityPrivileges(tabLevelSecurityListData, userOrginalGrpName, getUserName(userID), userEmail, tabLevelSecureFilePath);
                }
                else
                { 
                    string profileGrpName = string.Empty;
                    if (!string.IsNullOrEmpty(tabSecurityParam.grp_nm))
                    {
                        profileGrpName = tabSecurityParam.grp_nm;
                    }

                    if (!(userOrginalGrpName ?? "").ToLower().Equals((profileGrpName ?? "").ToLower()))
                    {
                        tabLevelSecurityListData.Remove(tabSecurityParam);
                        tabSecurityParam.grp_nm = userOrginalGrpName;
                        var adminPostInput = new Orgler.Models.Admin.AdminPostInput();
                        Orgler.Models.Admin.Admin adminInput = new Orgler.Models.Admin.Admin();
                       
                        if (userOrginalGrpName == "Orgler Admin" || userOrginalGrpName == "ORGLER ADMIN")
                        {
                            adminInput.usr_nm = tabSecurityParam.usr_nm;
                            adminInput.grp_nm = userOrginalGrpName;
                            adminInput.telephone_number = tabSecurityParam.telephone_number;
                            adminInput.email_address = tabSecurityParam.email_address;
                            adminInput.newaccount_tb_access = "RW";
                            adminInput.topaccount_tb_access = "RW";
                            adminInput.enterprise_orgs_tb_access = "RW";
                            adminInput.constituent_tb_access = "RW";
                            adminInput.transaction_tb_access = "RW";
                            adminInput.admin_tb_access = "RW";
                            adminInput.has_merge_unmerge_access = "1";
                            adminInput.help_tb_access = "RW";
                            adminInput.upload_eosi_tb_access = "N";
                            adminInput.upload_affil_tb_access = "N";
                            adminInput.upload_eo_tb_access = "N";
                            adminInput.is_approver = "1";
                            adminPostInput.adminInput = adminInput;
                        }
                        else if (userOrginalGrpName == "Orgler Writer")
                        {
                            adminInput.usr_nm = tabSecurityParam.usr_nm;
                            adminInput.grp_nm = userOrginalGrpName;
                            adminInput.telephone_number = tabSecurityParam.telephone_number;
                            adminInput.email_address = tabSecurityParam.email_address;
                            adminInput.newaccount_tb_access = "RW";
                            adminInput.topaccount_tb_access = "RW";
                            adminInput.enterprise_orgs_tb_access = "RW";
                            adminInput.constituent_tb_access = "RW";
                            adminInput.transaction_tb_access = "RW";
                            adminInput.admin_tb_access = "N";
                            adminInput.has_merge_unmerge_access = "1";
                            adminInput.help_tb_access = "RW";
                            adminInput.upload_eosi_tb_access = "N";
                            adminInput.upload_affil_tb_access = "N";
                            adminInput.upload_eo_tb_access = "N";
                            adminInput.is_approver = "0";
                            adminPostInput.adminInput = adminInput;
                        }

                        else if (userOrginalGrpName == "Orgler")
                        {
                            adminInput.usr_nm = tabSecurityParam.usr_nm;
                            adminInput.grp_nm = userOrginalGrpName;
                            adminInput.telephone_number = tabSecurityParam.telephone_number;
                            adminInput.email_address = tabSecurityParam.email_address;
                            adminInput.newaccount_tb_access = "R";
                            adminInput.topaccount_tb_access = "R";
                            adminInput.enterprise_orgs_tb_access = "R";
                            adminInput.constituent_tb_access = "R";
                            adminInput.transaction_tb_access = "R";
                            adminInput.admin_tb_access = "N";
                            adminInput.has_merge_unmerge_access = "0";
                            adminInput.help_tb_access = "R";
                            adminInput.upload_eosi_tb_access = "N";
                            adminInput.upload_affil_tb_access = "N";
                            adminInput.upload_eo_tb_access = "N";
                            adminInput.is_approver = "0";
                            adminPostInput.adminInput = adminInput;
                        }
                        tabSecurityParam.admin_tb_access = adminInput.admin_tb_access;
                        tabSecurityParam.constituent_tb_access = adminInput.constituent_tb_access;
                        tabSecurityParam.enterprise_orgs_tb_access = adminInput.enterprise_orgs_tb_access;
                        tabSecurityParam.newaccount_tb_access = adminInput.newaccount_tb_access;
                        tabSecurityParam.topaccount_tb_access = adminInput.topaccount_tb_access;
                        tabSecurityParam.transaction_tb_access = adminInput.transaction_tb_access;
                        tabSecurityParam.help_tb_access = adminInput.help_tb_access;
                        tabSecurityParam.upload_eosi_tb_access = adminInput.upload_eosi_tb_access;
                        tabSecurityParam.upload_affil_tb_access = adminInput.upload_affil_tb_access;
                        tabSecurityParam.upload_eo_tb_access = adminInput.upload_eo_tb_access;
                        tabSecurityParam.has_merge_unmerge_access = adminInput.has_merge_unmerge_access;
                        tabSecurityParam.is_approver = adminInput.is_approver;                      
                        tabLevelSecurityListData.Add(tabSecurityParam);
                        adminPostInput.loggedInUser = UserID;
                        //string BaseURL = BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
                        //string url = BaseURL + "api/adminservice/edituserprofile/";
                        //var tabLevelSecResult = Task.Run(async () => await InvokeWebService.PostResourceAsync(url, "", adminPostInput, "")).Result;

                        AdminServiceController adminController = new AdminServiceController();//Instantiating
                        adminController.Token = _token;
                        adminController.ClientID = _clientid;
                        var tabLevelSecResult = await adminController.updateTabLevelSecurity(adminPostInput);

                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        var result = serializer.Deserialize<IList<UserProfileOutput>>(serializer.Serialize(tabLevelSecResult.Data));
                        
                        if (result != null && result.Count > 0 && result[0].o_transOutput.Contains("Success"))
                        {
                            try
                            {
                                var r = Utility.writeJSONToFile<List<Security.TabLevelSecurityParams>>(tabLevelSecurityListData, tabLevelSecureFilePath);
                            }
                            catch (Exception ex)
                            {
                                log.Info("Error updateTabLevelSecurity writeJSONToFile " + ex.Message);
                            }
                        }
                        else if (tabLevelSecResult.Data.ToString().Contains("Success"))
                        {
                            try
                            {
                                var r = Utility.writeJSONToFile<List<Security.TabLevelSecurityParams>>(tabLevelSecurityListData, tabLevelSecureFilePath);
                            }
                            catch(Exception ex)
                            {
                                log.Info("Error updateTabLevelSecurity writeJSONToFile " + ex.Message);
                            }
                            // System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(listTabLevelSecurity));

                        }
                        else
                        {
                            log.Info("Error updateTabLevelSecurity: " + tabLevelSecResult.Data.ToString());
                        }
                    }
                }
                //  });		HttpContext	null	System.Web.HttpContextBase

            }
            catch (Exception ex)
            {
                log.Info("Error UpdateProfile: " + ex.Message);
            }
        }

        public async Task insertTabLevelSecurityPrivileges(List<TabLevelSecurityParams> listTabLevelSecurity, string actDirGroupName, string UsrName, string userEmail, string tabLevelSecureFilePath)
        {
            Security.TabLevelSecurityParams tabLevelParams = new Security.TabLevelSecurityParams();
            //string userEmail = HttpContext.Current.User.GetUserEmail();
            //Providing previleges to the user based on his Group Name 
            if (actDirGroupName == "Orgler Admin" || actDirGroupName == "ORGLER ADMIN")
            {
                tabLevelParams.usr_nm = UsrName;
                tabLevelParams.grp_nm = actDirGroupName;
                tabLevelParams.email_address = userEmail;
                tabLevelParams.telephone_number = "";
                tabLevelParams.newaccount_tb_access = "RW";
                tabLevelParams.topaccount_tb_access = "RW";
                tabLevelParams.enterprise_orgs_tb_access = "RW";
                tabLevelParams.constituent_tb_access = "RW";
                tabLevelParams.transaction_tb_access = "RW";
                tabLevelParams.admin_tb_access = "RW";
                tabLevelParams.help_tb_access = "RW";
                tabLevelParams.upload_eosi_tb_access = "N";
                tabLevelParams.upload_affil_tb_access = "N";
                tabLevelParams.upload_eo_tb_access = "N";
                tabLevelParams.has_merge_unmerge_access = "1";
                tabLevelParams.is_approver = "1";
            }

            else if (actDirGroupName == "Orgler Writer")
            {
                tabLevelParams.usr_nm = UsrName;
                tabLevelParams.grp_nm = actDirGroupName;
                tabLevelParams.email_address = userEmail;
                tabLevelParams.telephone_number = "";
                tabLevelParams.newaccount_tb_access = "RW";
                tabLevelParams.topaccount_tb_access = "RW";
                tabLevelParams.enterprise_orgs_tb_access = "RW";
                tabLevelParams.constituent_tb_access = "RW";
                tabLevelParams.transaction_tb_access = "RW";
                tabLevelParams.admin_tb_access = "N";
                tabLevelParams.help_tb_access = "RW";
                tabLevelParams.upload_eosi_tb_access = "N";
                tabLevelParams.upload_affil_tb_access = "N";
                tabLevelParams.upload_eo_tb_access = "N";
                tabLevelParams.has_merge_unmerge_access = "1";
                tabLevelParams.is_approver = "0";
            }
            else if (actDirGroupName == "Orgler")
            {
                tabLevelParams.usr_nm = UsrName;
                tabLevelParams.grp_nm = actDirGroupName;
                tabLevelParams.email_address = userEmail;
                tabLevelParams.telephone_number = "";
                tabLevelParams.newaccount_tb_access = "R";
                tabLevelParams.topaccount_tb_access = "R";
                tabLevelParams.enterprise_orgs_tb_access = "R";
                tabLevelParams.constituent_tb_access = "R";
                tabLevelParams.transaction_tb_access = "R";
                tabLevelParams.admin_tb_access = "N";
                tabLevelParams.help_tb_access = "R";
                tabLevelParams.upload_eosi_tb_access = "N";
                tabLevelParams.upload_affil_tb_access = "N";
                tabLevelParams.upload_eo_tb_access = "N";
                tabLevelParams.has_merge_unmerge_access = "0";
                tabLevelParams.is_approver = "0";
            }

            listTabLevelSecurity.Add(tabLevelParams);

            
            Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();//This dictionary will return a result string and the updated Tab Level Security List 

            try
            {

                //var tabLevelSecResult = Task.Run(async () =>  await testController.insertTabLevelSecurity(tabLevelParams)).Result;
                AdminServiceController adminController = new AdminServiceController();//Instantiating
                adminController.Token = _token;
                adminController.ClientID = _clientid;
                var tabLevelSecResult = await adminController.insertTabLevelSecurity(tabLevelParams);

                if (tabLevelSecResult.Data.ToString().Contains("Success"))
                {
                    try { 
                            var r = Utility.writeJSONToFile<List<Security.TabLevelSecurityParams>>(listTabLevelSecurity, tabLevelSecureFilePath);
                        }
                    catch (Exception ex)
                    {
                        log.Info("Error insertTabLevelSecurity writeJSONToFile " + ex.Message);
                    }
                    // System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(listTabLevelSecurity));

                }
                else
                {
                    log.Info("Error insertTabLevelSecurity: " + tabLevelSecResult.Data.ToString());
                }

            }
            catch (Exception ex)
            {
                log.Info("Error insertTabLevelSecurityPrivileges: " + ex.Message);
            }
        }

        //private async Task<Dictionary<string, Object>> insertTabLevelSecturityPrevileges(List<Security.TabLevelSecurityParams> listTabLevelSecurity, string actDirGroupName, string UsrName, string tabLevelSecureFilePath)
        //{
        //    Security.TabLevelSecurityParams tabLevelParams = new Security.TabLevelSecurityParams();

        //    //Providing previleges to the user based on his Group Name 
        //    if (actDirGroupName == "Orgler Admin")
        //    {
        //        tabLevelParams.usr_nm = UsrName;
        //        tabLevelParams.grp_nm = actDirGroupName;
        //        tabLevelParams.email_address = getUserEmailAddress();
        //        tabLevelParams.telephone_number = "";
        //        tabLevelParams.newaccount_tb_access = "RW";
        //        tabLevelParams.topaccount_tb_access = "RW";
        //        tabLevelParams.enterprise_orgs_tb_access = "N";
        //        tabLevelParams.constituent_tb_access = "RW";
        //        tabLevelParams.transaction_tb_access = "RW";
        //        tabLevelParams.admin_tb_access = "RW";
        //        tabLevelParams.help_tb_access = "RW";
        //        tabLevelParams.has_merge_unmerge_access = "true";
        //        tabLevelParams.is_approver = "1";
        //    }

        //    else if (actDirGroupName == "Orgler Writer")
        //    {
        //        tabLevelParams.usr_nm = UsrName;
        //        tabLevelParams.grp_nm = actDirGroupName;
        //        tabLevelParams.email_address = getUserEmailAddress();
        //        tabLevelParams.telephone_number = "";
        //        tabLevelParams.newaccount_tb_access = "RW";
        //        tabLevelParams.topaccount_tb_access = "RW";
        //        tabLevelParams.enterprise_orgs_tb_access = "N";
        //        tabLevelParams.constituent_tb_access = "RW";
        //        tabLevelParams.transaction_tb_access = "RW";
        //        tabLevelParams.admin_tb_access = "N";
        //        tabLevelParams.help_tb_access = "RW";
        //        tabLevelParams.has_merge_unmerge_access = "false";
        //        tabLevelParams.is_approver = "0";
        //    }
        //    else if (actDirGroupName == "Orgler")
        //    {
        //        tabLevelParams.usr_nm = UsrName;
        //        tabLevelParams.grp_nm = actDirGroupName;
        //        tabLevelParams.email_address = getUserEmailAddress();
        //        tabLevelParams.telephone_number = "";
        //        tabLevelParams.newaccount_tb_access = "R";
        //        tabLevelParams.topaccount_tb_access = "R";
        //        tabLevelParams.enterprise_orgs_tb_access = "N";
        //        tabLevelParams.constituent_tb_access = "R";
        //        tabLevelParams.transaction_tb_access = "R";
        //        tabLevelParams.admin_tb_access = "N";
        //        tabLevelParams.help_tb_access = "R";
        //        tabLevelParams.has_merge_unmerge_access = "false";
        //        tabLevelParams.is_approver = "0";
        //    }

        //    listTabLevelSecurity.Add(tabLevelParams);

        //    AdminServiceController adminController = new AdminServiceController();//Instantiating
        //    Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();//This dictionary will return a result string and the updated Tab Level Security List 

        //    try
        //    {
        //        //Writing the new entry to the database
        //        //  var tabLevelSecResult = await testController.insertTabLevelSecurity(tabLevelParams);

        //        var tabLevelSecResult = Task.Run(async () => await adminController.insertTabLevelSecurity(tabLevelParams)).Result;


        //        //If database returns success, then continue to write to the JSON file
        //        if (tabLevelSecResult.Data.ToString().Contains("Success"))
        //        {
        //            var result = Utility.writeJSONToFile<List<Security.TabLevelSecurityParams>>(listTabLevelSecurity, tabLevelSecureFilePath);
        //            if (result.ToString().ToLower().Contains("success"))
        //            {
        //                dictTabInsertionReturns.Add("Result", "Success");
        //                dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", listTabLevelSecurity);
        //                return dictTabInsertionReturns;
        //            }

        //            else
        //            {
        //                dictTabInsertionReturns.Add("Result", "Failure");
        //                dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
        //                return dictTabInsertionReturns;
        //            }

        //        }
        //        else
        //        {

        //            dictTabInsertionReturns.Add("Result", "Failure");
        //            dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
        //            return dictTabInsertionReturns;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        dictTabInsertionReturns.Add("Result", "Failure");
        //        dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
        //        return dictTabInsertionReturns;
        //    }

        //}

        private string getUserName(string UserID)
        {
            if (UserID.Contains("\\"))
            {
                int index = UserID.IndexOf("\\");
                return UserID.Substring(index + 1);
            }
            return UserID;
        }


        public class MyResult
        {
            public string Status { get; set; }
            public string StatusDescription { get; set; }
            public string Result { get; set; }
        }

        public class UserProfileOutput
        {
            public string o_transOutput { get; set; }
        }
    }
}