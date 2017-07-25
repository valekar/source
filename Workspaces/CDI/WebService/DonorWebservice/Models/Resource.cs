using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace DonorWebservice.Models
{
    public class Resource
    {
        public static async Task<string> PostResourceAsync(string url, Object data)
        {
            string Result = "";
            HttpClient client = null;
            HttpResponseMessage response = null;
            try
            {
                using (client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (response = await client.PostAsJsonAsync(url + "?=" + DateTime.Now.Ticks, data))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Result = await response.Content.ReadAsStringAsync();
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            Result = "{\"Status\":\"UnAuthorized\",\"StatusDescription\":\"UnAuthorized\"}";
                        }
                        else
                        {
                            Result = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "{\"Status\":\"Timedout\",\"StatusDescription\":" + ex.Message + "}";
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