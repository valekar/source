using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using Orgler.Models;
using System.Threading.Tasks;
using Orgler.Models.Entities;
using Orgler.Exceptions;
using System.Security.Principal;
namespace Orgler.Controllers
{
    //[Authorize]
    //[AuthRoles("Admin", "User", "Writer")]
    public class HomeController : BaseController
    {
        public  ActionResult NewAccount()
        {
            ViewBag.Message = "New Account";
            return View();
        }

        public ActionResult TopAccount()
        {
            ViewBag.Message = "Top Account";
            return View();
        }

        //added by srini for getting userPermissions for tabs
        [HttpGet]
        public JsonResult GetUserPermissions()
        {
            TabLevelSecurityParams userPermissionsObj = Utility.getUserPermissions(User.GetUserName());
            return Json(userPermissionsObj, JsonRequestBehavior.AllowGet);
        }



        [HandleException]
        public string getUserName()
        {

            return User.GetUserName();        
            
        }


       

        private void checkExceptions(string res)
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
        }

           }
    
    public class MemoryCache<T> 
    {
        public static List<T> getListCachedData(string cookieName,HttpRequestBase Request)
        {
            if (Request != null && Request.Cookies[cookieName] != null)
            {
                var CartString = Request.Cookies[cookieName].Value.ToString();
                if (!string.IsNullOrEmpty(CartString))
                {
                    return Models.Utility.StringToObject<List<T>>(CartString);
                }
            }           
          return null;                      
        }


        public static T getCachedData(string cookieName, HttpRequestBase Request)
        {
            

            if (Request != null && Request.Cookies[cookieName] != null)
            {
                var CartString = Request.Cookies[cookieName].Value.ToString();
                if (!string.IsNullOrEmpty(CartString))
                {
                    return Models.Utility.StringToObject<T>(CartString);
                }
            }

            return default(T);


        }



        public static void setListCacheData(List<T> data, string cookieName, HttpResponseBase Response)
        {
            string cookieTimeout = WebConfigurationManager.AppSettings["StuartCartCookieTimeout"];
            var finalResult = data;
            string cartListStr = Models.Utility.ObjectToString<IList<T>>(finalResult);
            if (!string.IsNullOrEmpty(cartListStr))
            {
                Response.Cookies.Remove(cookieName);
                Response.Cookies.Add(new System.Web.HttpCookie(cookieName)
                {
                    Value = cartListStr,
                    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(cookieTimeout)),
                    HttpOnly = true
                });
            }
        }
        public static void setCacheData(T data, string cookieName, HttpResponseBase Response)
        {
            string cookieTimeout = WebConfigurationManager.AppSettings["StuartCartCookieTimeout"];
            string str = "";
            var finalResult = data;
            str = Models.Utility.ObjectToString<T>(finalResult);

            if (!string.IsNullOrEmpty(str))
            {
                Response.Cookies.Remove(cookieName);
                Response.Cookies.Add(new System.Web.HttpCookie(cookieName)
                {
                    Value = str,
                    Expires = DateTime.Now.AddMinutes(Convert.ToInt32(cookieTimeout)),
                    HttpOnly = true
                });
              
            }
        }


      
        public static void clearCacheData(string cookieName, HttpResponseBase Response, HttpRequestBase Request)
        {
            if (Request != null && Request.Cookies[cookieName] != null)
            {
                Response.Cookies.Remove(cookieName);
                Response.Cookies.Add(new System.Web.HttpCookie(cookieName)
                {
                    Value = null,
                    Expires = DateTime.Now.AddMinutes(-10),
                    HttpOnly = true
                });
            }
        }


    }

}