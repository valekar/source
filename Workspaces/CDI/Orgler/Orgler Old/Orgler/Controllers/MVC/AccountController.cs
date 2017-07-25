
using Orgler.Models;
using Orgler.Models.Entities.AccountMonitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Orgler.Controllers.MVC
{
    public class AccountController : BaseController
    {
        //private string Token = "";
        //private string BaseURL = "";
        //private string MenuFilePath = "";
        //// GET: Account
        //public AccountController()
        //{
        //    var Request = System.Web.HttpContext.Current.Request;
        //    BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
        //    MenuFilePath = WebConfigurationManager.AppSettings["MenuJSONFilePath"];

        //    if (Request != null && Request.Cookies["StuartWebToken"] != null)
        //        Token = System.Web.HttpContext.Current.Request.Cookies["StuartWebToken"].Value.ToString();    
        //}
       
        public AccountController()
        {
          
        }
        public async Task<ActionResult> Index()
        {
           
            return View();
        }

        public async Task<JsonResult> AccountARCBest(string id)
        {
            // string user = User.Identity.Name;
            // (string.IsNullOrEmpty(Token)) return RedirectToAction("SessionOut");
            if (string.IsNullOrWhiteSpace(id)) id = Request["id"] ?? "";
            string url = BaseURL + "api/Account/GetAccountARCBest/" + id;
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
           //var result = (new JavaScriptSerializer()).Deserialize<IList<Data.Entities.Accounts.ARCBest>>(res);           
            return Json(res);
        }

        //[HttpPost]
        //public async Task<JsonResult> search(AccountSearchModel postData)
        //{
        //    string url = BaseURL + "api/Account/search/";
        //    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);          

        //    //var result = (new JavaScriptSerializer()).Deserialize<IList<Data.Entities.Accounts.ConsSearchResultsCache>>(res);        

        //    return Json(res);
        //}

       
        [HttpPost]
        [Route("Account/search")]
        public async Task<JsonResult> search(AccountSearchInputModel postData)
        {
            string url = BaseURL + "api/Account/search/";
            string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
            //AccountSearchOutputModel accountData = new AccountSearchOutputModel();
            //var result = accountData.AccountSearchOutputData(postData);
            ////var result = (new JavaScriptSerializer()).Deserialize<IList<Data.Entities.Accounts.ConsSearchResultsCache>>(res);        

            return Json(res);
        } 
    }
}