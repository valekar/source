using Newtonsoft.Json;
using Stuart_V2.Exceptions;
using Stuart_V2.Models;
using Stuart_V2.Models.Entities.Cem;
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
    public abstract class BaseController: Controller
    {
        protected string Token = "";
        protected string BaseURL = "";
        protected string MenuFilePath = "";
        protected string ClientID = "";
        // GET: Constituent      

        protected string UserName = "";
        protected string Password = "";
        protected int TokenExpire = 30;                
       
       protected override void Initialize(System.Web.Routing.RequestContext requestContext)
       {
           UserName = WebConfigurationManager.AppSettings["UserName"];
           Password = WebConfigurationManager.AppSettings["Password"];
           TokenExpire = WebConfigurationManager.AppSettings["TokenExpire"] == null ? 30 : int.Parse(WebConfigurationManager.AppSettings["TokenExpire"].ToString());

           var Request = System.Web.HttpContext.Current.Request;
           BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
           MenuFilePath = WebConfigurationManager.AppSettings["MenuJSONFilePath"];
           ClientID = WebConfigurationManager.AppSettings["ClientID"];

           if (Request != null && Request.Cookies["StuartV2WebToken"] != null)
               this.Token = System.Web.HttpContext.Current.Request.Cookies["StuartV2WebToken"].Value.ToString();
           //check request context for cookie and do your thang.
           if (string.IsNullOrEmpty(this.Token))
           {
               Models.Token token = new Models.Token();
               var acctoken = Task.Run(async () => await token.getToken(BaseURL, ClientID, UserName, Password));
               this.Token = acctoken.Result;

               if (!string.IsNullOrEmpty(this.Token))
               {
                   if (requestContext.HttpContext.Response.Cookies["StuartV2WebToken"] != null)
                       requestContext.HttpContext.Response.Cookies.Remove("StuartV2WebToken");

                   requestContext.HttpContext.Response.SetCookie(new System.Web.HttpCookie("StuartV2WebToken")
                   {
                       Value = acctoken.Result.ToString(),
                       Expires = DateTime.Now.AddMinutes(TokenExpire),
                       HttpOnly = true
                   });
               }
           }

           base.Initialize(requestContext);
       }



       //public JsonResult handleTrivialHttpRequests(string res)
       //{

       //    if (!res.Equals("") && res != null)
       //    {
       //        var result = JsonConvert.DeserializeObject<WebServiceStatus>(res);
       //        if (result != null && result.Status != null)
       //        {
       //            var status = result.Status;

       //            if (status.ToLower().Contains("timedout"))
       //            {
       //                throw new CustomExceptionHandler(Json("TimedOut"));
       //            }
       //            else if (status.ToLower().Contains("error"))
       //            {
       //                throw new CustomExceptionHandler(Json("DatabaseError"));
       //            }
       //            else if (status.ToLower().Contains("unauthorized"))
       //            {
       //                throw new CustomExceptionHandler(Json("Unauthorized"));
       //            }
       //        }
       //        var result1 = (new JavaScriptSerializer()).DeserializeObject(result.Result);
       //        return Json(result1, JsonRequestBehavior.AllowGet);
       //    }
       //    else
       //    {
       //        return Json("", JsonRequestBehavior.AllowGet);
       //    }
       //}


       //public void checkExceptions(string res)
       //{
       //    var result = JsonConvert.DeserializeObject<WebServiceStatus>(res);
       //    if (result != null && result.Status != null)
       //    {
       //        var status = result.Status;
       //        if (status.ToLower().Contains("timedout"))
       //        {
       //            throw new CustomExceptionHandler(Json("TimedOut"));
       //        }
       //        if (status.ToLower().Contains("error"))
       //        {
       //            throw new CustomExceptionHandler(Json("DatabaseError"));
       //        }
       //        if (status.ToLower().Contains("unauthorized"))
       //        {
       //            throw new CustomExceptionHandler(Json("Unauthorized"));
       //        }
       //    }

       //}



       //public JsonResult handleTrivialHttpRequests(string res)
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


       //public void checkExceptions(string res)
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

       public JsonResult handleTrivialHttpRequests(string res)
       {
           if (!res.Equals("") && res != null)
           {
               var JObj = (new JavaScriptSerializer()).DeserializeObject(res);
               if (JObj is Array)
               {
                   return Json(JObj, JsonRequestBehavior.AllowGet);
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

       public void checkExceptions(string res)
       {
           var JObj = (new JavaScriptSerializer()).DeserializeObject(res);
           if (!(JObj is Array))
           {
               var result = JsonConvert.DeserializeObject<WebServiceStatus>(res);
               if (result != null && result.Status != null)
               {
                   var status = result.Status;
                   if (status.ToLower().Contains("timedout"))
                   {
                       throw new CustomExceptionHandler(Json("TimedOut"));
                   }
                   if (status.ToLower().Contains("error"))
                   {
                       throw new CustomExceptionHandler(Json("DatabaseError"));
                   }
                   if (status.ToLower().Contains("unauthorized"))
                   {
                       throw new CustomExceptionHandler(Json("Unauthorized"));
                   }
               }
           }
          
       }

       public JsonResult processResultForUploadInsertion(string res, string emailSubject)
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

               UploadSubmitOutput dncOP = JsonConvert.DeserializeObject<UploadSubmitOutput>(res);
               Utility.writeSerializedUploadJsonData(dncOP, User.GetUserEmail(), emailSubject);

               return Json(result, JsonRequestBehavior.AllowGet);
           }
           else
           {
               return Json("", JsonRequestBehavior.AllowGet);
           }
       }

       
      
   }
}