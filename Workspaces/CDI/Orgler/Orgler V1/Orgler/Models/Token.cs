using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using NLog;

namespace Orgler.Models
{
    public class Token
    {
        private Logger log = LogManager.GetCurrentClassLogger();

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
                log.Info("token - " + ex.Message);
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
}