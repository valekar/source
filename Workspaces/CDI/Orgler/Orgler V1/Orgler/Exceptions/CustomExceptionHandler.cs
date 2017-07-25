using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orgler.Exceptions
{
    public class CustomExceptionHandler : ApplicationException
    {
        public JsonResult exceptionDetails;
        //Constuctors
        public CustomExceptionHandler(JsonResult exceptionDetails)
        {
            this.exceptionDetails = exceptionDetails;
        }

        public JsonResult myException()
        {
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
                //Redirect to the general controller methods which inturn return the respective JSON string to the client
                var url = UrlHelper.Action(((CustomExceptionHandler)filterContext.Exception).myException().Data.ToString() ?? "", "General");
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}