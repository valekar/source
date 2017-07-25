using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Orgler.Controllers
{
    public class GeneralController : Controller
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        //Controller methods to send the exception message respectively to the client
        //Unauthorized Exception
        public JsonResult Unauthorized()
        {
            log.Info("Unauthorized Exception");
            Response.StatusCode = 423;
            return Json("LoginDenied", JsonRequestBehavior.AllowGet);
        }

        //DataBase Error Exception
        public JsonResult DatabaseError()
        {
            log.Info("Database Error Exception");
            Response.StatusCode = 500;
            return Json("Database Error", JsonRequestBehavior.AllowGet);
        }

        //Timeout Exception
        public JsonResult TimedOut()
        {
            log.Info("Timedout Exception");
            Response.StatusCode = 500;
            return Json("Timed out", JsonRequestBehavior.AllowGet);
        }

        public JsonResult InernalError()
        {
            log.Info("Internal Error Exception");
            Response.StatusCode = 500;
            return Json("Internal Error", JsonRequestBehavior.AllowGet);
        }
    }
}