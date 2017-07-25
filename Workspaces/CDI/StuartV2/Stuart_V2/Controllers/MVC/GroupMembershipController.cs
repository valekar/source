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
    [RouteArea("group")]
    [RoutePrefix("")]
    [Authorize]
    public class GroupMembershipController : BaseController
    {
        // GET: GroupMembership
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [HandleException]
        [Route("membership/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getGroupMembership(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/group/GetGroupMembership/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            JavaScriptSerializer serializer = serializer = new JavaScriptSerializer();
            if (!res.Equals(""))
            {
                checkExceptions(res);
                List<GroupMembership> grpMembrshpList = serializer.Deserialize<List<GroupMembership>>(res);
                ProcessCEM<GroupMembership> process = new ProcessCEM<GroupMembership>();
                List<GroupMembership> convertedgrpMembrshpList = process.convertRecords(grpMembrshpList, "GROUP_MEMBERSHIP");
                return Json(convertedgrpMembrshpList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        [HandleException]
        [Route("membership/all/{masterId}")]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> getAllGroupMembership(string masterId)
        {
            if (string.IsNullOrWhiteSpace(masterId)) return Json("NoID");
            string url = BaseURL + "api/group/GetAllGroupMembership/" + masterId;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("membership/add")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> addGroupMembership(GroupMembershipInput grpMembershipInput)
        {
            string url = BaseURL + "api/group/AddGroupMembership";
            string res = await Models.Resource.PostResourceAsync(url, Token, grpMembershipInput, ClientID);
            return handleTrivialHttpRequests(res);
        }


        [HttpPost]
        [HandleException]
        [Route("membership/edit")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> editGroupMembership(GroupMembershipInput grpMembershipInput)
        {
            string url = BaseURL + "api/group/EditGroupMembership";
            string res = await Models.Resource.PostResourceAsync(url, Token, grpMembershipInput, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HttpPost]
        [HandleException]
        [Route("membership/delete")]
        [TabLevelSecurity("constituent_tb_access", "RW")]
        public async Task<JsonResult> deleteGroupMembership(GroupMembershipInput grpMembershipInput)
        {
            string url = BaseURL + "api/group/DeleteGroupMembership";
            string res = await Models.Resource.PostResourceAsync(url, Token, grpMembershipInput, ClientID);
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