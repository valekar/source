using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace Orgler.Exceptions
{
    public class CustomExceptionHandler : ApplicationException
    {

        public JsonResult exceptionDetails;
        public CustomExceptionHandler(JsonResult exceptionDetails)
        {
            this.exceptionDetails = exceptionDetails;
        }

        public JsonResult myException()
        {

            //JsonResult myJson = new JsonResult();
           // myJson.Data = "{\"Status\":\"Timedout\",\"StatusDescription\":" + exceptionDetails + "}"; 
            return this.exceptionDetails;
        }       

        public CustomExceptionHandler(string message) : base(message) { }
        public CustomExceptionHandler(string message, Exception inner) : base(message, inner) { }
        protected CustomExceptionHandler(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }



    public class HandleExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public virtual void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.Exception != null)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                var UrlHelper = new UrlHelper(filterContext.RequestContext);
                string CustomMsg = "InternalError";
                if (filterContext.Exception.GetType() == typeof(CustomExceptionHandler))
                {
                    CustomMsg = ((CustomExceptionHandler)filterContext.Exception).myException().Data.ToString();
                }
                var url = UrlHelper.Action(CustomMsg, "General");
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}