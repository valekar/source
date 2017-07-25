using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace DonorWebservice.Models
{
    public class ExceptionLog : ApplicationException
    {
        public ExceptionLog(string message)
            : base(message)
        {

        }
    }

    public class ExceptionLogAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        private Logger log = LogManager.GetCurrentClassLogger();
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Uri url = actionExecutedContext.Request.RequestUri;

            string controllerName = url.LocalPath;
            string actionName = actionExecutedContext.Request.Method.Method;

            string exceptionMsg = actionExecutedContext.Exception.ToString();
            string innerExceptionMsg = actionExecutedContext.Exception.InnerException == null ? null : actionExecutedContext.Exception.InnerException.ToString();

            string exceptionDetail = String.Format("Controller: {0} | Action: {1} | Exception: {2}", controllerName, actionName, innerExceptionMsg == null ? exceptionMsg : innerExceptionMsg);
            log.Info(exceptionDetail);
        }
    }
}