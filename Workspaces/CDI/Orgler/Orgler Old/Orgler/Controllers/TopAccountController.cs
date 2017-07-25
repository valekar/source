using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orgler.Models;
using System.Threading.Tasks;
using Orgler.Exceptions;
using Orgler.Models.TopAccount;
using System.Web.Script.Serialization;

namespace Orgler.Controllers
{
    [Authorize]
    public class TopAccountClientController : BaseController
    {
        //Search for Top Accounts
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("constituent_tb_access", "RW", "R")]
        public async Task<JsonResult> Search(SearchInput postData)
        {
            string url = BaseURL + "api/topaccount/search/";
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
                return Json("", JsonRequestBehavior.AllowGet);
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
    }
}