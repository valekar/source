using Orgler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Orgler.Models.Entities.Constituents;
using System.Web.Script.Serialization;
using Orgler.Exceptions;
using Newtonsoft.Json;

namespace Orgler.Controllers
{
    [Authorize]
    public class ConstituentController : BaseController
    {

        public ConstituentController()
        {

        }
        // GET: Constituent
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> ConstituentARCBest(string id)
        {
            // string user = User.Identity.Name;
            // (string.IsNullOrEmpty(Token)) return RedirectToAction("SessionOut");
            if (string.IsNullOrWhiteSpace(id)) id = Request["id"] ?? "";
            string url = BaseURL + "api/Constituent/GetConstituentARCBest/" + id;
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            handleTrivialHttpRequests(res);
            // string res = await  Models.Resource.GetResourceAsync(url, Token, ClientID);
            //var result = (new JavaScriptSerializer()).Deserialize<IList<Data.Entities.Constituents.ARCBest>>(res);           
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> search(ConstituentSearchModel postData)
        {
            string url = BaseURL + "api/Constituent/search/";
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            handleTrivialHttpRequests(res);

            //var result = (new JavaScriptSerializer()).Deserialize<IList<Data.Entities.Constituents.ConsSearchResultsCache>>(res);        

            return Json(res);
        }

        [HttpPost]
        //[Route("constituent/advsearch")]
        public async Task<JsonResult> advsearch(ConstituentSearchModel postData)
        {
            string url = BaseURL + "api/Constituent/advsearch/";
            string res = await InvokeWebService.GetResourceAsync(url, Token, ClientID);
            handleTrivialHttpRequests(res);
            return Json(res);
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
    }
}