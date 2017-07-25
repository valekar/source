using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Reflection;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using Orgler.Models.Entities;
using Orgler.Exceptions;
using System.Web.Routing;
using System.Threading.Tasks;
using Orgler.Controllers.MVC;

namespace Orgler.Models
{
    // Keerthana - 28-Apr-2016 - Filter Attribute to check for tab level security
    // AKshay - 30-May-201 - Adding user to the TabLevelSecurity file if logging in to the application for the first time

    [HandleException]
    public class TabLevelSecurity : AuthorizeAttribute
    {

        Logger log = LogManager.GetCurrentClassLogger();

        //Varible to store the Tab name same as the column name in DB
        string ActionTab;
        //Varible to store the permissions pertaining to the Action method
        string[] ActionPermission;

        public TabLevelSecurity(string actionTab, params string[] actionPermission)
        {
            this.ActionTab = actionTab;
            this.ActionPermission = actionPermission;
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
                    Entities.TabLevelSecurityParams TabLevelSecurityCurrentUser = new Entities.TabLevelSecurityParams();                 
                    if (listTabLevelSecurity.Exists(x => x.usr_nm.ToString().ToLower() == UsrName.ToLower()))
                    {
                        TabLevelSecurityCurrentUser = listTabLevelSecurity.AsEnumerable().Where(x => (x.usr_nm.ToString().ToLower() == UsrName.ToLower())).Select(x => x).FirstOrDefault();
                    }
                    else
                    {
                        //Check if Group Name returns null or not
                        string UsrData = "";

                        if (HttpContext.Current.User.Identity is FormsIdentity)
                        {
                            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
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
                                UsrGrp = "Stuart Admin";
                            }
                            if (UsrData.ToLower().Contains("writer"))
                            {
                                UsrGrp = "Stuart Writer";
                            }
                            if (UsrData.ToLower().Contains("user"))
                            {
                                UsrGrp = "Stuart";
                            }
                        }
                        if (UsrGrp != "")
                        {
                            actDirGroupName = UsrGrp;
                            Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();

                            //Since the user has been authenticated but not present in the Tab Level Security file, we will create an entry for the user in the file and also in the database.
                           // dictTabInsertionReturns = insertTabLevelSecturityPrevileges(listTabLevelSecurity, actDirGroupName, UsrName, tabLevelSecureFilePath).Result;

                            if (dictTabInsertionReturns["UpdatedTabLevelSecurityList"] != null)
                            {
                                listTabLevelSecurity = (List<Entities.TabLevelSecurityParams>)dictTabInsertionReturns["UpdatedTabLevelSecurityList"];
                                TabLevelSecurityCurrentUser = listTabLevelSecurity.AsEnumerable().Where(x => (x.usr_nm.ToString() == UsrName)).Select(x => x).FirstOrDefault();
                            }
                        }
                    }


                    string strPermission = string.Empty;
                    string strAddPermission = string.Empty;

                    //Loop through the tab level entries
                    //Only one record per user will be present and hence the list will have only one row
                    if (TabLevelSecurityCurrentUser != null)
                    {
                        switch (ActionTab)
                        {
                            case "constituent_tb_access": strPermission = TabLevelSecurityCurrentUser.constituent_tb_access ?? "N"; break;
                            case "account_tb_access": strPermission = TabLevelSecurityCurrentUser.account_tb_access ?? "N"; break;
                            case "transaction_tb_access": strPermission = TabLevelSecurityCurrentUser.transaction_tb_access ?? "N"; break;
                            case "case_tb_access": strPermission = TabLevelSecurityCurrentUser.case_tb_access ?? "N"; break;
                            case "admin_tb_access": strPermission = TabLevelSecurityCurrentUser.admin_tb_access ?? "N"; break;
                            case "enterprise_orgs_tb_access": strPermission = TabLevelSecurityCurrentUser.enterprise_orgs_tb_access ?? "N"; break;
                            case "reference_data_tb_access": strPermission = TabLevelSecurityCurrentUser.reference_data_tb_access ?? "N"; break;
                            case "upload_tb_access": strPermission = TabLevelSecurityCurrentUser.upload_tb_access ?? "N"; break;
                            case "report_tb_access": strPermission = TabLevelSecurityCurrentUser.report_tb_access ?? "N"; break;
                            case "utitlity_tb_access": strPermission = TabLevelSecurityCurrentUser.utitlity_tb_access ?? "N"; break;
                            case "locator_tab_access": strPermission = TabLevelSecurityCurrentUser.locator_tab_access ?? "N"; break;
                            case "help_tb_access": strPermission = TabLevelSecurityCurrentUser.help_tb_access ?? "N"; break;
                            case "domn_corctn_access": strPermission = TabLevelSecurityCurrentUser.domn_corctn_access ?? "N"; break;
                            case "has_merge_unmerge_access":
                                strPermission = TabLevelSecurityCurrentUser.has_merge_unmerge_access ?? "N";
                                strAddPermission = TabLevelSecurityCurrentUser.constituent_tb_access ?? "N";
                                break;
                            case "is_approver": strPermission = TabLevelSecurityCurrentUser.is_approver ?? "N";
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


        //private async Task<Dictionary<string, Object>> insertTabLevelSecturityPrevileges(List<Entities.TabLevelSecurityParams> listTabLevelSecurity, string actDirGroupName, string UsrName, string tabLevelSecureFilePath)
        //{
        //    Entities.TabLevelSecurityParams tabLevelParams = new Entities.TabLevelSecurityParams();

        //    //Providing previleges to the user based on his Group Name 
        //    if (actDirGroupName == "Stuart Admin")
        //    {
        //        tabLevelParams.usr_nm = UsrName;
        //        tabLevelParams.grp_nm = actDirGroupName;
        //        tabLevelParams.email_address = "";
        //        tabLevelParams.telephone_number = "";
        //        tabLevelParams.constituent_tb_access = "RW";
        //        tabLevelParams.account_tb_access = "R";
        //        tabLevelParams.transaction_tb_access = "RW";
        //        tabLevelParams.case_tb_access = "RW";
        //        tabLevelParams.admin_tb_access = "RW";
        //        tabLevelParams.enterprise_orgs_tb_access = "RW";
        //        tabLevelParams.reference_data_tb_access = "RW";
        //        tabLevelParams.upload_tb_access = "RW";
        //        tabLevelParams.report_tb_access = "RW";
        //        tabLevelParams.utitlity_tb_access = "RW";
        //        tabLevelParams.locator_tab_access = "RW";
        //        tabLevelParams.help_tb_access = "R";
        //        tabLevelParams.domn_corctn_access = "RW";
        //        tabLevelParams.has_merge_unmerge_access = "true";
        //        tabLevelParams.is_approver = "1";
        //    }

        //    else if (actDirGroupName == "Stuart Writer")
        //    {
        //        tabLevelParams.usr_nm = UsrName;
        //        tabLevelParams.grp_nm = actDirGroupName;
        //        tabLevelParams.email_address = "";
        //        tabLevelParams.telephone_number = "";
        //        tabLevelParams.constituent_tb_access = "RW";
        //        tabLevelParams.account_tb_access = "R";
        //        tabLevelParams.transaction_tb_access = "RW";
        //        tabLevelParams.case_tb_access = "RW";
        //        tabLevelParams.admin_tb_access = "N";
        //        tabLevelParams.enterprise_orgs_tb_access = "RW";
        //        tabLevelParams.reference_data_tb_access = "RW";
        //        tabLevelParams.upload_tb_access = "RW";
        //        tabLevelParams.report_tb_access = "N";
        //        tabLevelParams.utitlity_tb_access = "N";
        //        tabLevelParams.locator_tab_access = "RW";
        //        tabLevelParams.help_tb_access = "R";
        //        tabLevelParams.domn_corctn_access = "R";
        //        tabLevelParams.has_merge_unmerge_access = "true";
        //        tabLevelParams.is_approver = "0";
        //    }
        //    else if (actDirGroupName == "Stuart")
        //    {
        //        tabLevelParams.usr_nm = UsrName;
        //        tabLevelParams.grp_nm = actDirGroupName;
        //        tabLevelParams.email_address = "";
        //        tabLevelParams.telephone_number = "";
        //        tabLevelParams.constituent_tb_access = "R";
        //        tabLevelParams.account_tb_access = "R";
        //        tabLevelParams.transaction_tb_access = "R";
        //        tabLevelParams.case_tb_access = "RW";
        //        tabLevelParams.admin_tb_access = "N";
        //        tabLevelParams.enterprise_orgs_tb_access = "R";
        //        tabLevelParams.reference_data_tb_access = "R";
        //        tabLevelParams.upload_tb_access = "N";
        //        tabLevelParams.report_tb_access = "N";
        //        tabLevelParams.utitlity_tb_access = "N";
        //        tabLevelParams.locator_tab_access = "R";
        //        tabLevelParams.help_tb_access = "R";
        //        tabLevelParams.domn_corctn_access = "R";
        //        tabLevelParams.has_merge_unmerge_access = "false";
        //        tabLevelParams.is_approver = "0";
        //    }

        //    listTabLevelSecurity.Add(tabLevelParams);

        //    TestController testController = new TestController();//Instantiating
        //    Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();//This dictionary will return a result string and the updated Tab Level Security List 

        //    try
        //    {
        //        //Writing the new entry to the database
                //  var tabLevelSecResult = await testController.insertTabLevelSecurity(tabLevelParams);

               // var tabLevelSecResult = Task.Run(async () => await testController.insertTabLevelSecurity(tabLevelParams)).Result;


                //If database returns success, then continue to write to the JSON file
                //if (tabLevelSecResult.Data.ToString().Contains("Success"))
                //{
                //    var result = Utility.writeJSONToFile<List<Entities.TabLevelSecurityParams>>(listTabLevelSecurity, tabLevelSecureFilePath);
                //    if (result.ToString().ToLower().Contains("success"))
                //    {
                //        dictTabInsertionReturns.Add("Result", "Success");
                //        dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", listTabLevelSecurity);
                //        return dictTabInsertionReturns;
                //    }

                //    else
                //    {
                //        dictTabInsertionReturns.Add("Result", "Failure");
                //        dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
                //        return dictTabInsertionReturns;
                //    }

                //}
                //else
                //{

                //    dictTabInsertionReturns.Add("Result", "Failure");
                //    dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
                //    return dictTabInsertionReturns;
                //}

        //    }
        //    catch (Exception ex)
        //    {
        //        dictTabInsertionReturns.Add("Result", "Failure");
        //        dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
        //        return dictTabInsertionReturns;
        //    }

        //}
    }
}