using Orgler.Models.Entities.AccountMonitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Orgler.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Web.UI;
using System.Reflection;
using Orgler.Models.Entities;
using Orgler.Exceptions;
using System.Security.Principal;
namespace Orgler.Controllers.MVC
{

   // [Authorize]
    public class TestController : BaseController
    {

        //  public static sealed TestController testInstance = new TestController();

        JavaScriptSerializer serializer;

        public TestController()
        {

            Constant consts = Constant.getInstance();
            serializer = new JavaScriptSerializer();

        }


        // GET: Test
        public ActionResult Index()
        {

            return View();
        }


        //This method implements the logging mechanism when the user attempts to login
        public async Task<JsonResult> insertLoginHistory(LoginHistoryInput LoginHistoryInput)
        {
            string url = BaseURL + "api/Login/insertloginhistory/";
            string res = await Models.Resource.PostResourceAsync(url, Token, LoginHistoryInput, ClientID);
            var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);
        }

        //This method implements the logging mechanism when the user attempts to login
        public async Task<JsonResult> insertTabLevelSecurity(TabLevelSecurityParams TablevelSecurity)
        {
            string url = BaseURL + "api/Login/addUserTabLevelSecurity/";
            string res = await Models.Resource.PostResourceAsync(url, Token, TablevelSecurity, ClientID);
            var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);
        }

        [HandleException]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> ConstituentARCBest(string id)
        {
                if (string.IsNullOrWhiteSpace(id)) id = Request["id"] ?? "";
                string url = BaseURL + "api/Constituent/GetARCBest/" + id;
                //var res = await Models.Resource.GetResourceAsync<object>(url, Token, ClientID);
                //return Json(res);
                string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                return handleTrivialHttpRequests(res);
        }

        //[HandleException]
        //[HttpPost]
        //[TabLevelSecurity("constituent_tb_access", "RW", "R")]
        //public async Task<JsonResult> search(ConstituentSearchModel postData)
        //{

        //        string url = BaseURL + "api/Constituent/search/";
        //        string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
        //        return handleTrivialHttpRequests(res);
            
        //}
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> search(AccountSearchInputModel postData)
        {
           //postData.naicsSuggestionPresentInd = true;
            string url = BaseURL + "api/accountmonitoring/search/";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            return handleTrivialHttpRequests(res);

        }
        //[HandleException]
        //[HttpPost]
        //[TabLevelSecurity("constituent_tb_access", "RW", "R")]
        //public async Task<JsonResult> advsearch(ListConstituentInputSearchModel postData)
        //{           
        //        postData.AnswerSetLimit = "50";
        //        string url = BaseURL + "api/Constituent/advsearch/";
        //        string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
        //        return handleTrivialHttpRequests(res);   
        //}
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> advsearch(AccountSearchListInputModel postData)
        {
            postData.listAccountSearchInputModel[0].masteringType = null;
            postData.listAccountSearchInputModel[0].naicsSuggestionPresentInd= true;
            string url = BaseURL + "api/accountmonitoring/search/";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            return handleTrivialHttpRequests(res);
        }
       

        private JsonResult handleTrivialHttpRequests(string res)
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
                return Json("",JsonRequestBehavior.AllowGet);
            }

           
        }


        private void checkExceptions(string res)
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
    }   
}


