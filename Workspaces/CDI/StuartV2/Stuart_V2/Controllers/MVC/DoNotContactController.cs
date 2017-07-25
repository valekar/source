using Newtonsoft.Json;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Cem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{
    [RouteArea("dnc")]
    [RoutePrefix("")]
    [Authorize]
    public class DoNotContactController : BaseController
    {
        // GET: DoNotContact
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandleException]
        [Route("{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getDnc(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/dnc/GetDnc/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            JavaScriptSerializer serializer = serializer = new JavaScriptSerializer();
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<DoNotContact> dncList = serializer.Deserialize<List<DoNotContact>>(res);
                ProcessCEM<DoNotContact> process = new ProcessCEM<DoNotContact>();
                List<DoNotContact> convertedPrefLocList = process.convertRecords(dncList, "DO_NOT_CONTACT");
                return Json(convertedPrefLocList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [HandleException]
        [Route("all/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getAllDncs(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/dnc/GetAllDnc/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("add")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addDoNotContact(DoNotContactInput dncInput)
        {
            string url = BaseURL + "api/dnc/AddDnc";
            dncInput.i_user_id = User.GetUserName();
            string res = await Models.Resource.PostResourceAsync(url, Token, dncInput, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("edit")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> editDoNotContact(DoNotContactInput dncInput)
        {
            string url = BaseURL + "api/dnc/EditDnc";
            dncInput.i_user_id = User.GetUserName();
            string res = await Models.Resource.PostResourceAsync(url, Token, dncInput, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("delete")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteDoNotContact(DoNotContactInput dncInput)
        {
            string url = BaseURL + "api/dnc/DeleteDnc";
            dncInput.i_user_id = User.GetUserName();
            string res = await Models.Resource.PostResourceAsync(url, Token, dncInput, ClientID);
            return handleTrivialHttpRequests(res);
        }

        //private void checkExceptions(string res)
        //{
        //    if (res.ToLower().Contains("timedout"))
        //    {
        //        throw new CustomExceptionHandler(Json("TimedOut"));
        //    }
        //    if (res.ToLower().Contains("error"))
        //    {
        //        throw new CustomExceptionHandler(Json("DatabaseError"));
        //    }
        //    if (res.ToLower().Contains("unauthorized"))
        //    {
        //        throw new CustomExceptionHandler(Json("Unauthorized"));
        //    }
        //}


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