using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities;
using Stuart_V2.Models.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("AdminNative")]
    [RoutePrefix("")]
    [Authorize]
    public class AdminNativeController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        [Route("GetTabLevelSecurity/{id}")]
        public async Task<JsonResult> GetTabLevelSecurity(string id)
        {
            AdminTabSecurityInput adTabInput = new AdminTabSecurityInput();
            adTabInput.loggedInUser = User.GetUserName();

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Admin/GetTabLevelSecurity";
            string res = await Models.Resource.PostResourceAsync(url, Token, adTabInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        [Route("EditTabLevelSecurity")]
        public async Task<JsonResult> editTabLevelSecurity(Admin adminInput)
        {
            AdminPostInput adminPostInput = new AdminPostInput();
            adminPostInput.adminInput = adminInput;
            adminPostInput.loggedInUser = User.GetUserName();

            string url = BaseURL + "api/Admin/EditTabLevelSecurity";
            string res = await Models.Resource.PostResourceAsync(url, Token, adminPostInput, ClientID);
            return handleTrivialHttpRequests(res);
        }


        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        [Route("InsertTabLevelSecurity")]
        public async Task<JsonResult> insertTabLevelSecurity(Admin adminInput)
        {
            AdminPostInput adminPostInput = new AdminPostInput();
            adminPostInput.adminInput = adminInput;
            adminPostInput.loggedInUser = User.GetUserName();

            string url = BaseURL + "api/Admin/InsertTabLevelSecurity";
            string res = await Models.Resource.PostResourceAsync(url, Token, adminPostInput, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        [Route("DeleteTabLevelSecurity")]
        public async Task<JsonResult> deleteTabLevelSecurity(Admin adminInput)
        {
            AdminPostInput adminPostInput = new AdminPostInput();
            adminPostInput.adminInput = adminInput;
            adminPostInput.loggedInUser = User.GetUserName();

            string url = BaseURL + "api/Admin/DeleteTabLevelSecurity";
            string res = await Models.Resource.PostResourceAsync(url, Token, adminPostInput, ClientID);
            return handleTrivialHttpRequests(res);
        }



        [HandleException]
        [TabLevelSecurity("admin_tb_access", "RW", "R")]
        [Route("GetUpdatedTabLevelSecurity/{id}")]
        public async Task<JsonResult> GetUpdatedTabLevelSecurity(string id)
        {
            AdminTabSecurityInput adTabInput = new AdminTabSecurityInput();
            adTabInput.loggedInUser = User.GetUserName();

            if (string.IsNullOrWhiteSpace(id)) return Json("NoID");
            string url = BaseURL + "api/Admin/GetTabLevelSecurity";
            string res = await Models.Resource.PostResourceAsync(url, Token, adTabInput, ClientID);
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

        //private JsonResult handleTrivialHttpRequests(string res)
        //{

        //    if (!res.Equals("") && res != null)
        //    {
        //        if (res.ToLower().Contains("timedout"))
        //        {
        //            throw new CustomExceptionHandler(Json("TimedOut"));
        //        }
        //        if (res.ToLower().Contains("error"))
        //        {
        //            throw new CustomExceptionHandler(Json("DatabaseError"));
        //        }
        //        if (res.ToLower().Contains("unauthorized"))
        //        {
        //            throw new CustomExceptionHandler(Json("Unauthorized"));
        //        }
        //        var result = (new JavaScriptSerializer()).DeserializeObject(res);



        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json("", JsonRequestBehavior.AllowGet);
        //    }


        //}

    }
}