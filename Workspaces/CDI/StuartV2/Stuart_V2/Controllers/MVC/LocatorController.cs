using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Locator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{
    [Authorize]
    public class LocatorNativeController : BaseController
    {
        // GET: Locator
        public ActionResult Index()
        {
            return View();
        }

       
        [HandleException]
        [HttpPost]       
        public async Task<JsonResult> LocatorEmailsearch(ListLocatorEmailInputSearchModel postData)
        {
                string url = BaseURL + "api/Locator/locatoremailsearch";
                string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return handleTrivialHttpRequests(res);
        }


        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorEmailDetailsByID(LocatorEmailInputSearchModel postData)
        {
            string url = BaseURL + "api/Locator/locatoremailDetailsByID";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            //var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorEmailConstDetailsByID(LocatorEmailInputSearchModel postData)
        {
            string url = BaseURL + "api/Locator/locatoremailconstDetailsByID";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            //var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        //[TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> updateLocatorEmail(LocatorEmailInputSearchModel UpdateLocatorEmail)
        {
             string url = BaseURL + "api/Locator/editlocatorEmail/";
            string res = await Models.Resource.PostResourceAsync(url, Token, UpdateLocatorEmail, ClientID);            
            return handleTrivialHttpRequests(res);
        }



        [HandleException]
        [HttpPost]
        //[TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> updateLocatorAddress(LocatorAddressInputSearchModel UpdateLocatorAddress)
        {
            string url = BaseURL + "api/Locator/editlocatorAddress/";
            string res = await Models.Resource.PostResourceAsync(url, Token, UpdateLocatorAddress, ClientID);
            return handleTrivialHttpRequests(res);
        }


        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorAddresssearch(ListLocatorAddressInputSearchModel postData)
        {
            string url = BaseURL + "api/Locator/locatoraddresssearch";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            //var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorAddressDetailsByID(LocatorAddressInputSearchModel postData)
        {
            string url = BaseURL + "api/Locator/locatorAddressDetailsByID";           
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);            
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorAddressAssessmentDetailsByID(LocatorAddressInputSearchModel postData)
        {

            string url = BaseURL + "api/Locator/locatorAddressAssessmentDetailsByID";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorAddressConstituentsDetailsByID(LocatorAddressInputSearchModel postData)
        {           
            string url = BaseURL + "api/Locator/locatorAddressConstituentsDetailsByID";          
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);           
            return handleTrivialHttpRequests(res);
        }

        
        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorDomainsearch(ListLocatorDomainInputSearchModel postData)
        {
            string url = BaseURL + "api/Locator/locatoremailDomain_Details";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            //var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return handleTrivialHttpRequests(res);
        }

        [HandleException]
        [HttpPost]
        public async Task<JsonResult> LocatorDomainRollback(ListLocatorDomainInputSearchModel postData)
        {
            string url = BaseURL + "api/Locator/locatordomainUpdates";
            string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
            //var result = (new JavaScriptSerializer()).DeserializeObject(res);
            return handleTrivialHttpRequests(res);
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