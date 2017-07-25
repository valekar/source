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
using Stuart_V2.Models.Entities;
using Stuart_V2.Exceptions;
using System.Web.Routing;
using System.Threading.Tasks;
using Stuart_V2.Controllers.MVC;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace Stuart_V2.Models
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
                                                RouteValueDictionary(new { controller = "General", action = "Unauthorized", area = "" }));

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
        //update usr_prfl table
        public async Task updateUserProfile(string UserID,HttpCookie authCookie)
        {
            try
            {
                //await Task.Run(() =>
                //{
                    var tabLevelSecureFilePath = System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"];
                    List<TabLevelSecurityParams> tabLevelSecurityListData = new List<TabLevelSecurityParams>();
                    tabLevelSecurityListData = Utility.readJSONTFromFile<List<TabLevelSecurityParams>>(tabLevelSecureFilePath);

                    string strGrpName = string.Empty;
                    string userOrginalGrpName = strGrpName.GetUserGrpName(authCookie);
                    string userName = UserID.GetUserName() ;

                    TabLevelSecurityParams tabSecurityParam = new TabLevelSecurityParams();
                    tabSecurityParam = tabLevelSecurityListData.Find(x => x.usr_nm.ToLower() == userName.ToLower());


                    if (tabSecurityParam == null)
                    {

                        string userEmail = string.Empty;
                        userEmail = userEmail.GetUserEmail(authCookie);

                         Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();

                            //Since the user has been authenticated but not present in the Tab Level Security file, we will create an entry for the user in the file and also in the database.
                       dictTabInsertionReturns
                       = insertTabLevelSecurityPrivileges(tabLevelSecurityListData, userOrginalGrpName, userName, userEmail, tabLevelSecureFilePath).Result;
                    }

                    string profileGrpName = tabSecurityParam.grp_nm;

                    if (!(userOrginalGrpName ?? "").ToLower().Equals((profileGrpName ?? "").ToLower()))
                    {
                        tabLevelSecurityListData.Remove(tabSecurityParam);
                        tabSecurityParam.grp_nm = userOrginalGrpName;
                        tabLevelSecurityListData.Add(tabSecurityParam);

                        var adminPostInput = new Stuart_V2.Models.Entities.Admin.AdminPostInput();

                        Stuart_V2.Models.Entities.Admin.Admin adminInput = new Stuart_V2.Models.Entities.Admin.Admin();

                        adminInput.account_tb_access = tabSecurityParam.account_tb_access;
                        adminInput.admin_tb_access = tabSecurityParam.admin_tb_access;
                        adminInput.case_tb_access = tabSecurityParam.case_tb_access;
                        adminInput.constituent_tb_access = tabSecurityParam.constituent_tb_access;
                        adminInput.domn_corctn_access = tabSecurityParam.domn_corctn_access;
                        //adminInput.dw_trans_ts = 
                        adminInput.email_address = tabSecurityParam.email_address;
                        adminInput.enterprise_orgs_tb_access = tabSecurityParam.enterprise_orgs_tb_access;
                        adminInput.grp_nm = userOrginalGrpName;
                        adminInput.has_merge_unmerge_access = tabSecurityParam.has_merge_unmerge_access;
                        adminInput.help_tb_access = tabSecurityParam.help_tb_access;
                        adminInput.is_approver = tabSecurityParam.is_approver;
                        adminInput.locator_tab_access = tabSecurityParam.locator_tab_access;
                        adminInput.reference_data_tb_access = tabSecurityParam.reference_data_tb_access;
                        adminInput.report_tb_access = tabSecurityParam.report_tb_access;
                        adminInput.telephone_number = tabSecurityParam.telephone_number;
                        adminInput.transaction_tb_access = tabSecurityParam.transaction_tb_access;
                        adminInput.upload_tb_access = tabSecurityParam.upload_tb_access;
                        adminInput.usr_nm = tabSecurityParam.usr_nm;
                        adminInput.utitlity_tb_access = tabSecurityParam.utitlity_tb_access;


                        adminPostInput.adminInput = adminInput;
                        //  adminPostInput.loggedInUser = User.GetUserName();
                        string BaseURL = BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
                        string url = BaseURL + "api/Login/EditTabLevelSecurity";
                        //System.Threading.Tasks.Task.Run(async () => await Models.Resource.PostResourceAsync(url, "", adminPostInput, ""));

                        var tabLevelSecResult = Task.Run(async () => await Models.Resource.PostResourceAsync(url, "", adminPostInput, ""));


                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        MyResult res = serializer.Deserialize<MyResult>(serializer.Serialize(tabLevelSecResult));
                       // MyResult result1 = serializer.Deserialize<MyResult>(serializer.Serialize(res));
                        if (res.Result.ToLower().Contains("success"))
                        {
                            var result = Utility.writeJSONToFile<List<Stuart_V2.Models.Entities.TabLevelSecurityParams>>(tabLevelSecurityListData, tabLevelSecureFilePath);
                            if (result.ToString().ToLower().Contains("success"))
                            {

                            }
                        }
                        else
                        {

                        }
                    }

              //  });
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<Dictionary<string, Object>> insertTabLevelSecurityPrivileges(List<TabLevelSecurityParams> listTabLevelSecurity, string actDirGroupName, string UsrName, string userEmail, string tabLevelSecureFilePath)
        {
            Entities.TabLevelSecurityParams tabLevelParams = new Entities.TabLevelSecurityParams();
            //string userEmail = HttpContext.Current.User.GetUserEmail();
            //Providing previleges to the user based on his Group Name 
            if (actDirGroupName == "Stuart Admin" || actDirGroupName == "STUART ADMIN")
            {
                tabLevelParams.usr_nm = UsrName;
                tabLevelParams.grp_nm = actDirGroupName;
                tabLevelParams.email_address = userEmail;
                tabLevelParams.telephone_number = "";
                tabLevelParams.constituent_tb_access = "RW";
                tabLevelParams.account_tb_access = "R";
                tabLevelParams.transaction_tb_access = "RW";
                tabLevelParams.case_tb_access = "RW";
                tabLevelParams.admin_tb_access = "RW";
                tabLevelParams.enterprise_orgs_tb_access = "RW";
                tabLevelParams.reference_data_tb_access = "RW";
                tabLevelParams.upload_tb_access = "RW";
                tabLevelParams.report_tb_access = "RW";
                tabLevelParams.utitlity_tb_access = "RW";
                tabLevelParams.locator_tab_access = "RW";
                tabLevelParams.help_tb_access = "R";
                tabLevelParams.domn_corctn_access = "RW";
                tabLevelParams.has_merge_unmerge_access = "1";
                tabLevelParams.is_approver = "1";
            }

            else if (actDirGroupName == "Stuart Writer")
            {
                tabLevelParams.usr_nm = UsrName;
                tabLevelParams.grp_nm = actDirGroupName;
                tabLevelParams.email_address = userEmail;
                tabLevelParams.telephone_number = "";
                tabLevelParams.constituent_tb_access = "RW";
                tabLevelParams.account_tb_access = "R";
                tabLevelParams.transaction_tb_access = "RW";
                tabLevelParams.case_tb_access = "RW";
                tabLevelParams.admin_tb_access = "N";
                tabLevelParams.enterprise_orgs_tb_access = "RW";
                tabLevelParams.reference_data_tb_access = "RW";
                tabLevelParams.upload_tb_access = "RW";
                tabLevelParams.report_tb_access = "N";
                tabLevelParams.utitlity_tb_access = "N";
                tabLevelParams.locator_tab_access = "RW";
                tabLevelParams.help_tb_access = "R";
                tabLevelParams.domn_corctn_access = "R";
                tabLevelParams.has_merge_unmerge_access = "1";
                tabLevelParams.is_approver = "0";
            }
            else if (actDirGroupName == "Stuart")
            {
                tabLevelParams.usr_nm = UsrName;
                tabLevelParams.grp_nm = actDirGroupName;
                tabLevelParams.email_address = userEmail;
                tabLevelParams.telephone_number = "";
                tabLevelParams.constituent_tb_access = "R";
                tabLevelParams.account_tb_access = "R";
                tabLevelParams.transaction_tb_access = "R";
                tabLevelParams.case_tb_access = "RW";
                tabLevelParams.admin_tb_access = "N";
                tabLevelParams.enterprise_orgs_tb_access = "R";
                tabLevelParams.reference_data_tb_access = "R";
                tabLevelParams.upload_tb_access = "R";
                tabLevelParams.report_tb_access = "N";
                tabLevelParams.utitlity_tb_access = "N";
                tabLevelParams.locator_tab_access = "R";
                tabLevelParams.help_tb_access = "R";
                tabLevelParams.domn_corctn_access = "R";
                tabLevelParams.has_merge_unmerge_access = "0";
                tabLevelParams.is_approver = "0";
            }

            listTabLevelSecurity.Add(tabLevelParams);

            TestController testController = new TestController();//Instantiating
            Dictionary<string, Object> dictTabInsertionReturns = new Dictionary<string, object>();//This dictionary will return a result string and the updated Tab Level Security List 

            try
            {

                var tabLevelSecResult = Task.Run(async () => await testController.insertTabLevelSecurity(tabLevelParams)).Result;

                //JavaScriptSerializer serializer = new JavaScriptSerializer();
               // var JObj = (new JavaScriptSerializer()).DeserializeObject(tabLevelSecResult);

                if (tabLevelSecResult.Data.ToString().ToLower().Contains("success"))
                {
                    var result = Utility.writeJSONToFile<List<Entities.TabLevelSecurityParams>>(listTabLevelSecurity, tabLevelSecureFilePath);

                   // System.IO.File.WriteAllText(jpath, JSON_PrettyPrinter.Process(listTabLevelSecurity));

                    if (result.ToString().ToLower().Contains("success"))
                    {
                        dictTabInsertionReturns.Add("Result", "Success");
                        dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", listTabLevelSecurity);
                        return dictTabInsertionReturns;
                    }

                    else
                    {
                        dictTabInsertionReturns.Add("Result", "Failure");
                        dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
                        return dictTabInsertionReturns;
                    }

                }
                else
                {

                    dictTabInsertionReturns.Add("Result", "Failure");
                    dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
                    return dictTabInsertionReturns;
                }

            }
            catch (Exception ex)
            {
                dictTabInsertionReturns.Add("Result", "Failure");
                dictTabInsertionReturns.Add("UpdatedTabLevelSecurityList", null);
                return dictTabInsertionReturns;
            }
        }


        public class MyResult 
        {
            public string Status { get; set; }
            public string StatusDescription { get; set; }
            public string Result { get; set; }
        }
    }

}