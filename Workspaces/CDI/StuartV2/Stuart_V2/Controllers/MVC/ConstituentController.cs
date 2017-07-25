
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Constituents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Stuart_V2.Controllers.MVC
{

    public class ConstituentController : BaseController
    {
        //private string Token = "";
        //private string BaseURL = "";
        //private string MenuFilePath = "";
        //// GET: Constituent
        //public ConstituentController()
        //{
        //    var Request = System.Web.HttpContext.Current.Request;
        //    BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
        //    MenuFilePath = WebConfigurationManager.AppSettings["MenuJSONFilePath"];

        //    if (Request != null && Request.Cookies["StuartWebToken"] != null)
        //        Token = System.Web.HttpContext.Current.Request.Cookies["StuartWebToken"].Value.ToString();    
        //}
       
        public ConstituentController()
        {
          
        }
        public async Task<ActionResult> Index()
        {
           
            return View();
        }

        public async Task<JsonResult> ConstituentARCBest(string id)
        {
            // string user = User.Identity.Name;
            // (string.IsNullOrEmpty(Token)) return RedirectToAction("SessionOut");
            if (string.IsNullOrWhiteSpace(id)) id = Request["id"] ?? "";
            string url = BaseURL + "api/Constituent/GetConstituentARCBest/" + id;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
           //var result = (new JavaScriptSerializer()).Deserialize<IList<Data.Entities.Constituents.ARCBest>>(res);           
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> search(ConstituentSearchModel postData)
        {
            string url = BaseURL + "api/Constituent/search/";
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);          

            //var result = (new JavaScriptSerializer()).Deserialize<IList<Data.Entities.Constituents.ConsSearchResultsCache>>(res);        

            return Json(res);
        }
        
        [HttpPost]
        [Route("constituent/advsearch")]
        public async Task<JsonResult> advsearch(ConstituentSearchModel postData)
        {
            string url = BaseURL + "api/Constituent/advsearch/";
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            return Json(res);
        }





       
    }
}