using Orgler.Exceptions;
using Orgler.Models;
using Orgler.Models.Entities.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Orgler.Controllers.MVC
{
    [Authorize]
    public class GroupMembershipReferenceNativeController:BaseController
    {
        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> getGroupMembershipReferenceData()
        {
            string url = BaseURL + "api/Upload/getGroupMembershipReferenceData/";
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postAddNewGrpMbrshpRefRecordParams(GroupMembershipReferenceInsertData addRefDataRec)
        {
            string url = BaseURL + "api/Upload/postNewGroupMembershipReferenceRecord/";
            string res = await Models.Resource.PostResourceAsync(url, Token, addRefDataRec, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postDeleteGrpMbrshpRefRecordParam (GroupMembershipDeleteReferenceParam gm)
        {
            string url = BaseURL + "api/Upload/postDeleteMembershipReferenceRecord/";
            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [TabLevelSecurity("upload_tb_access", "RW")]
        public async Task<JsonResult> postEditGrpMbrshpRefRecordParam(GroupMembershipEditReferenceParam gm)
        {
            string url = BaseURL + "api/Upload/postEditMembershipReferenceRecord/";
            string res = await Models.Resource.PostResourceAsync(url, Token, gm, ClientID);
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
                 return Json("", JsonRequestBehavior.AllowGet);
             }


         }
    }
}