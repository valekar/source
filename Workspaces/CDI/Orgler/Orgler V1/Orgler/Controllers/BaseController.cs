using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Configuration;
using NLog;
using Orgler.Models.Upload;
using Orgler.Exceptions;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Orgler.Security;

namespace Orgler.Controllers
{
    public abstract class BaseController : Controller
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        //Base class to store the Web API details which can be accessed from anyother controller using inheritance
        internal string Token = "";
        internal string ClientID = "";
        protected string BaseURL = "";
        protected string MenuFilePath = "";
        protected string EntOrgMenuPrefFilePath = string.Empty;
        protected string UserName = "";
        protected string Password = "";
        protected int TokenExpire = 30;     
        
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            log.Info("Initialize Base Controller");

            UserName = WebConfigurationManager.AppSettings["UserName"];
            Password = WebConfigurationManager.AppSettings["Password"];
            TokenExpire = WebConfigurationManager.AppSettings["TokenExpire"] == null ? 30 : int.Parse(WebConfigurationManager.AppSettings["TokenExpire"].ToString());

            var Request = System.Web.HttpContext.Current.Request;
            BaseURL = WebConfigurationManager.AppSettings["BaseURL"];
            MenuFilePath = WebConfigurationManager.AppSettings["MenuJSONFilePath"];
            EntOrgMenuPrefFilePath = WebConfigurationManager.AppSettings["EntOrgMenuPrefFilePath"];
            ClientID = WebConfigurationManager.AppSettings["ClientID"];
            //log.Info("Base");
            //log.Info(BaseURL);
            //log.Info(ClientID);
            //log.Info(Request);
            //log.Info(Request.Cookies["OrglerWebToken"]);
            if (Request != null && Request.Cookies["OrglerWebToken"] != null)
                this.Token = System.Web.HttpContext.Current.Request.Cookies["OrglerWebToken"].Value.ToString();
            log.Info(this.Token);
            //check request context for cookie and do your thang.
            if (string.IsNullOrEmpty(this.Token))
            {
                log.Info("token is empty");
                Models.Token token = new Models.Token();
                var acctoken = Task.Run(async () => await token.getToken(BaseURL, ClientID, UserName, Password));
                this.Token = acctoken.Result;

                if (!string.IsNullOrEmpty(this.Token))
                {
                    if (requestContext.HttpContext.Response.Cookies["OrglerWebToken"] != null)
                        requestContext.HttpContext.Response.Cookies.Remove("OrglerWebToken");

                    requestContext.HttpContext.Response.SetCookie(new System.Web.HttpCookie("OrglerWebToken")
                    {
                        Value = acctoken.Result.ToString(),
                        Expires = DateTime.Now.AddMinutes(TokenExpire),
                        HttpOnly = true
                    });
                }
            }

            base.Initialize(requestContext);
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

                log.Info("Upload email to address - " + User.GetUserEmail());

                UploadSubmitOutput upldOutput = JsonConvert.DeserializeObject<UploadSubmitOutput>(res);
                Utility.writeSerializedUploadJsonData(upldOutput, User.GetUserEmail(), emailSubject);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}