using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Orgler.Controllers
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
   }
}