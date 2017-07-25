using Orgler.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Orgler.Models.Admin;
using Orgler.Services;
using System.Security.Principal;
using System.Web.Configuration;
using Orgler.Security;
using Newtonsoft.Json;

namespace Orgler.Controllers
{
    [Authorize]
    public class AdminServiceController : BaseController
    {
        public AdminServiceController()
        {
            BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]        
        public async Task<JsonResult> GetTabLevelSecurity(string id)
        {
            AdminTabSecurityInput adTabInput = new AdminTabSecurityInput();
            IPrincipal p = System.Web.HttpContext.Current.User;
            adTabInput.loggedInUser = Orgler.Security.Extentions.GetUserName(p);
            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/adminservice/getuserprofiledetails";
            string res = await InvokeWebService.PostResourceAsync(url, Token, adTabInput, ClientID);

            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);
        }

        [HandleException]       
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        public async Task<JsonResult> editTabLevelSecurity(Admin adminInput)
        {
            AdminPostInput adminPostInput = new AdminPostInput();
            adminPostInput.adminInput = adminInput;
            IPrincipal p = System.Web.HttpContext.Current.User;
            adminPostInput.loggedInUser = Orgler.Security.Extentions.GetUserName(p);

            string url = BaseURL + "api/adminservice/edituserprofile";
            string res = await InvokeWebService.PostResourceAsync(url, Token, adminPostInput, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        //[Route("DeleteTabLevelSecurity")]
        public async Task<JsonResult> deleteTabLevelSecurity(Admin adminInput)
        {
            AdminPostInput adminPostInput = new AdminPostInput();
            adminPostInput.adminInput = adminInput;
            IPrincipal p = System.Web.HttpContext.Current.User;
            adminPostInput.loggedInUser = Orgler.Security.Extentions.GetUserName(p);

            string url = BaseURL + "api/adminservice/deleteuserprofile";
            string res = await InvokeWebService.PostResourceAsync(url, Token, adminPostInput, ClientID);
            return handleTrivialHttpRequests(res);
        }
        //This method implements the logging mechanism when the user attempts to login
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        public async Task<JsonResult> insertTabLevelSecurity(Security.TabLevelSecurityParams TablevelSecurity)
        {
            string url = BaseURL + "api/adminservice/addUserTabLevelSecurity/";
            string res = await InvokeWebService.PostResourceAsync(url, Token, TablevelSecurity, ClientID);
            checkExceptions(res);
            var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);
        }
        //This method implements the logging mechanism when the user attempts to login
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        public async Task<JsonResult> updateTabLevelSecurity(Models.Admin.AdminPostInput TablevelSecurity)
        {
            string url = BaseURL + "api/adminservice/edituserprofile";            
            string res = await InvokeWebService.PostResourceAsync(url, Token, TablevelSecurity, ClientID);
            checkExceptions(res);
            var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);
        }
        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        //[Route("GetUpdatedTabLevelSecurity/{id}")]
        public async Task<JsonResult> GetUpdatedTabLevelSecurity(string id)
        {
            AdminTabSecurityInput adTabInput = new AdminTabSecurityInput();
            adTabInput.loggedInUser = User.GetUserName();

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/adminservice/getuserprofiledetails";
            string res = await InvokeWebService.PostResourceAsync(url, Token, adTabInput, ClientID);
            handleTrivialHttpRequests(res);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/

            List<TabLevelSecurityParams> listTabLevelSecurity = (new JavaScriptSerializer()).Deserialize<List<TabLevelSecurityParams>>(res);
            var tabLevelSecureFilePath = System.Configuration.ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"];

            var result = Utility.writeJSONToFile<List<TabLevelSecurityParams>>(listTabLevelSecurity, tabLevelSecureFilePath);


            if (result.ToString().ToLower().Contains("success"))
            {
                return handleTrivialHttpRequests(res);
            }

            return handleTrivialHttpRequests("error");

        }

        private JsonResult handleTrivialHttpRequests1(string res)
        {

            if (!res.Equals("") && res != null)
            {
                if (res.ToLower().Contains("timedout"))
                {
                    throw new CustomExceptionHandler(Json("TimedOut"));
                }
                if (res.ToLower().Contains("error"))
                {
                    throw new CustomExceptionHandler(Json("DatabaseError"));
                }
                if (res.ToLower().Contains("unauthorized"))
                {
                    throw new CustomExceptionHandler(Json("Unauthorized"));
                }
                var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }


        }
        /* Code by naga */
        private JsonResult handleTrivialHttpRequests(string res)
        {
            if (!res.Equals("") && res != null)
            {
                // var result = JsonConvert.DeserializeObject<WebServiceStatus>(res);   
                // var JObj =(new JavaScriptSerializer()).DeserializeObject(res) as Dictionary<string,object>;
                //object x = null;
                //var result = new WebServiceStatus();
                //foreach (var k in JObj)
                //{
                //    if (JObj.TryGetValue(k.Key, out x))
                //    {
                //        if (x.GetType() == typeof(WebServiceStatus))
                //        {
                //            result = (WebServiceStatus)x;
                //            //t.HasValue = true;
                //        }
                //    }
                //}
                var JObj = (new JavaScriptSerializer()).DeserializeObject(res);
                if (JObj is Array)
                {
                    var results = (new JavaScriptSerializer()).DeserializeObject(res);
                    return Json(results, JsonRequestBehavior.AllowGet);
                }

                var result = JsonConvert.DeserializeObject<WebServiceStatus>(res);
                if (result != null && result.Status != null)
                {
                    var status = result.Status;

                    if (status.ToLower().Contains("timedout"))
                    {
                        throw new CustomExceptionHandler(Json("TimedOut"));
                    }
                    else if (status.ToLower().Contains("error"))
                    {
                        throw new CustomExceptionHandler(Json("DatabaseError"));
                    }
                    else if (status.ToLower().Contains("unauthorized"))
                    {
                        throw new CustomExceptionHandler(Json("Unauthorized"));
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = (new JavaScriptSerializer()).DeserializeObject(res);
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        private void checkExceptions(string res)
        {
            var JObj = (new JavaScriptSerializer()).DeserializeObject(res);
            if (JObj is Array)
            {
                var results = (new JavaScriptSerializer()).DeserializeObject(res);
                //return Json(results, JsonRequestBehavior.AllowGet);
            }
            if (res.ToLower().Contains("timedout"))
            {
                throw new CustomExceptionHandler(Json("TimedOut"));
            }
            if (res.ToLower().Contains("error"))
            {
                throw new CustomExceptionHandler(Json("DatabaseError"));
            }
            if (res.ToLower().Contains("unauthorized"))
            {
                throw new CustomExceptionHandler(Json("Unauthorized"));
            }
        }
    }
}