using Newtonsoft.Json.Linq;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Case;
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
    [RouteArea("CaseNative")]
    [RoutePrefix("")]
    [Authorize]
    public class CaseNativeController : BaseController
    {
        // GET: Case
        public ActionResult Index()
        {
            return View();
        }
        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW", "R")]
        public async Task<JsonResult> AdvSearch(ListCaseInputSearchModel postData)
        {
            
                string url = BaseURL + "api/Case/advcasesearch";
                string res = await Models.Resource.PostResourceAsync(url, Token, postData, ClientID);
                //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                return handleTrivialHttpRequests(res);   
            
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> CreateCase(CompositeCreateCaseInput CompositeCreateCaseInput)
        {
            
                    string url = BaseURL + "api/Case/createcase";
                    string res = await Models.Resource.PostResourceAsync(url, Token, CompositeCreateCaseInput, ClientID);
                    //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                    return handleTrivialHttpRequests(res);   
                
            
        }

        [HandleException]
        [HttpGet]
        [TabLevelSecurity("case_tb_access", "RW", "R")]
        public async Task<JsonResult> getCaseTransDetails(string id)
        {
            
                    string url = BaseURL + "api/Case/transactiondetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                    //return Json(result, JsonRequestBehavior.AllowGet);
                    return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpGet]
        [TabLevelSecurity("case_tb_access", "RW", "R")]
        public async Task<JsonResult> getCaseLocatorDetails(string id)
        {
            
                    string url = BaseURL + "api/Case/locatordetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                    //return Json(result, JsonRequestBehavior.AllowGet);
                    return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpGet]
        [TabLevelSecurity("case_tb_access", "RW", "R")]
        public async Task<JsonResult> GetCaseNotesDetails(string id)
        {
           
                    string url = BaseURL + "api/Case/notesdetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                    //return Json(result, JsonRequestBehavior.AllowGet);
                    return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpGet]
        [TabLevelSecurity("case_tb_access", "RW", "R")]
        public async Task<JsonResult> GetCasePreferenceDetails(string id)
        {
            
                    string url = BaseURL + "api/Case/preferencedetails/" + id;
                    string res = await Models.Resource.GetResourceAsync(url, Token, ClientID);
                    //var result = (new JavaScriptSerializer()).DeserializeObject(res);
                    return handleTrivialHttpRequests(res);   
                
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> updatecaseinfo(CreateCaseInput UpdateCaseInput)
        {
                string url = BaseURL + "api/Case/editcase/";
                string res = await Models.Resource.PostResourceAsync(url, Token, UpdateCaseInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }


        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> deletecaseinfo(DeleteCaseInput DeleteCaseInput)
        {
            
                string url = BaseURL + "api/Case/deletecase/";
                string res = await Models.Resource.PostResourceAsync(url, Token, DeleteCaseInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> addcaselocinfo(CaseLocatorInput CaseLocInput)
        {
            
                string url = BaseURL + "api/Case/addcaselocator/";
                string res = await Models.Resource.PostResourceAsync(url, Token, CaseLocInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> updatecaselocinfo(CaseLocatorInput CaseLocInput)
        {
            
                string url = BaseURL + "api/Case/editcaselocator/";
                string res = await Models.Resource.PostResourceAsync(url, Token, CaseLocInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> deletecaselocinfo(CaseLocatorInput CaseLocInput)
        {
            
                string url = BaseURL + "api/Case/deletecaselocator/";
                string res = await Models.Resource.PostResourceAsync(url, Token, CaseLocInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> addcasenotesinfo(CaseNotesInput CaseNotesInput)
        {
            
                string url = BaseURL + "api/Case/addcasenotes/";
                string res = await Models.Resource.PostResourceAsync(url, Token, CaseNotesInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> updatecasenotesinfo(CaseNotesInput CaseNotesInput)
        {
            
                string url = BaseURL + "api/Case/editcasenotes/";
                string res = await Models.Resource.PostResourceAsync(url, Token, CaseNotesInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpPost]
        [TabLevelSecurity("case_tb_access", "RW")]
        public async Task<JsonResult> deletecasenotesinfo(CaseNotesInput CaseNotesInput)
        {
            
                string url = BaseURL + "api/Case/deletecasenotes/";
                string res = await Models.Resource.PostResourceAsync(url, Token, CaseNotesInput, ClientID);
                return handleTrivialHttpRequests(res);   
        }

        [HandleException]
        [HttpPost()]
        [TabLevelSecurity("case_tb_access", "RW")]
        public string UploadFiles()
        // public async Task<JsonResult> UploadFiles()
        {
            //System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            //string url = BaseURL + "api/Case/uploadgeneralfile/";
            //string res = await Models.Resource.PostFileAsync(url, Token, hfc, ClientID);
            //var result = (new JavaScriptSerializer()).DeserializeObject(res);
            //return Json(result);
          
          
                int iUploadedCnt = 0;
                string fileName = "";
                string savedFilePath = "";
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                try
                {
                    sPath = WebConfigurationManager.AppSettings["UploadFilePath"];  //System.Web.Hosting.HostingEnvironment.MapPath("~/Files/");
                    System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                    // CHECK THE FILE COUNT.
                    for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                    {
                        System.Web.HttpPostedFile hpf = hfc[iCnt];
                        if (hpf.ContentLength > 0)
                        {
                            // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                            if (!System.IO.File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                            {
                                // SAVE THE FILES IN THE FOLDER.
                                fileName = Path.GetFileName(hpf.FileName);
                                hpf.SaveAs(sPath + fileName);
                                iUploadedCnt = iUploadedCnt + 1;
                            }
                        }
                    }
                    // RETURN A MESSAGE (OPTIONAL).
                    if (iUploadedCnt > 0)
                    {
                        //If you update the path here, update in web config as well

                        
                       // savedFilePath = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/Files/" + fileName;

                        

                        string callbackurl = Request.Url.Host != "localhost" ? Request.Url.Host : Request.Url.Authority;

                        savedFilePath = "http://" + callbackurl + WebConfigurationManager.AppSettings["UploadToPathForCase"] + fileName;
                        return savedFilePath;
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                catch
                {
                    return "Failed";
                }
            
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