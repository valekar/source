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
    [RouteArea("message")]
    [RoutePrefix("")]
    [Authorize]
    public class MessagePreferenceController : BaseController
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandleException]
        [Route("preference/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getMessagePreference(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/Message/GetMessagePreference/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            JavaScriptSerializer serializer = serializer = new JavaScriptSerializer();
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<MessagePreference> prefLocList = serializer.Deserialize<List<MessagePreference>>(res);
                ProcessCEM<MessagePreference> process = new ProcessCEM<MessagePreference>();
                List<MessagePreference> convertedPrefLocList = process.convertRecords(prefLocList, "MESSAGE_PREFERENCE");
                return Json(convertedPrefLocList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        [HandleException]
        [Route("preference/all/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getAllMessagePreference(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/Message/GetAllMessagePreference/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HttpGet]
        [HandleException]
        [Route("preference/options/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getMessagePreferenceOptions(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/Message/GetMessagePreferenceOptions/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            JavaScriptSerializer serializer = serializer = new JavaScriptSerializer();
            return handleTrivialHttpRequests(res);
        }


        [HttpPost]
        [HandleException]
        [Route("preference/add")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addMessagePreference(MessagePreferenceInput msgPrefInput)
        {
            string url = BaseURL + "api/Message/AddMessagePreference";
            msgPrefInput.i_user_id = User.GetUserName();
            msgPrefInput.i_created_by = User.GetUserName();
            string res = await Models.Resource.PostResourceAsync(url, Token, msgPrefInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);
        }


        [HttpPost]
        [HandleException]
        [Route("preference/edit")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> editMessagePreference(MessagePreferenceInput msgPrefInput)
        {
            string url = BaseURL + "api/Message/EditMessagePreference";
            msgPrefInput.i_user_id = User.GetUserName();
            msgPrefInput.i_created_by = User.GetUserName();
            string res = await Models.Resource.PostResourceAsync(url, Token, msgPrefInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("preference/delete")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteMessagePreference(MessagePreferenceInput msgPrefInput)
        {
            string url = BaseURL + "api/Message/DeleteMessagePreference";
            msgPrefInput.i_user_id = User.GetUserName();
            msgPrefInput.i_created_by = User.GetUserName();
            string res = await Models.Resource.PostResourceAsync(url, Token, msgPrefInput, ClientID);
            /*var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return Json(result);*/
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