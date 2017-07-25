using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Stuart_V2.Models
{
    public class Token
    {
        public async Task<string> getToken(string baseAddress, string clientID, string UserName , string Password)
        {
            string Result = "";
            System.Net.ServicePointManager.DefaultConnectionLimit = 10;
            HttpClient client = null;
            HttpResponseMessage response = null;
            try
            {
                using (client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://stu-agent.redcross.org/");
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content1 = new FormUrlEncodedContent(
                       new Dictionary<string, string> {
                             {"grant_type","password"},
                             {"client_Id",clientID},
                             {"username",UserName},
                             {"password",Password} 
                        });

                    // New code:
                    using (response = await client.PostAsync("Token", content1))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = await response.Content.ReadAsStringAsync();
                            JObject obj = JObject.Parse(responseString);
                            // return (string)obj["access_token"];

                            Result = (string)obj["access_token"];
                            response.Dispose();
                        }
                        else
                        {
                            Result = "";
                            //Result = response.StatusCode.ToString() + ", " + response.ReasonPhrase;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Result = "{\"Status\":\"Timedout\",\"StatusDescription\":" + ex.Message + "}";
                Result = "";
                //Result = "Error, " + ex.Message;
                //new Models.Entity.ResponseStatus { Status = "Timedout", StatusDescription = ex.Message };
            }
            finally
            {
                if (client != null) client.Dispose();
                if (response != null) response.Dispose();
            }
            return Result;
        }
    }


   /* public class checkStuartWebTokenFilter : ActionFilterAttribute, IActionFilter
    {
        protected string Token = ""; 
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies["StuartWebToken"] != null)
                this.Token = System.Web.HttpContext.Current.Request.Cookies["StuartWebToken"].Value.ToString();

            if (string.IsNullOrEmpty(Token))
            {
                var result = await GetToken();
                if (Response.Cookies["StuartWebToken"] != null)
                    Response.Cookies.Remove("StuartWebToken");

                Response.Cookies.Add(new System.Web.HttpCookie("StuartWebToken")
                {
                    Value = result,
                    Expires = DateTime.Now.AddMinutes(TokenExpire),
                    HttpOnly = true
                });
            }
        }


   
    }*/
}