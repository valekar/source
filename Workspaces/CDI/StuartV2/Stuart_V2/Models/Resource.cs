using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Stuart_V2.Models
{
    public class WebServiceStatus
    {
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public string Result { get; set; }
    }

    public class Resource
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private static string _msg = "";

        public static async Task<string> GetResourceAsync(string url, string oauthToken, string ClientID)
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
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + oauthToken);
                    client.DefaultRequestHeaders.Add("ClientID", ClientID);
                    using (response = await client.GetAsync(url + "?=" + DateTime.Now.Ticks))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Result = await response.Content.ReadAsStringAsync();
                          
                        }
                        else
                        {
                            Result = "{\"Status\":\"" + response.StatusCode + "\",\"StatusDescription\":\"" + response.Content.ToString() + "\",\"Result\": \"\" }";
                            _msg = Result;
                            log.Info(_msg);
                            Result = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "{\"Status\":\"Timedout\",\"StatusDescription\":\"" + ex.Message + "\",\"Result\": \"\" }";
                //new Models.Entity.ResponseStatus { Status = "Timedout", StatusDescription = ex.Message };
            }
            finally
            {
                if (client != null) client.Dispose();
                if (response != null) response.Dispose();
            }
            return Result;
        }

        public static async Task<T> GetResourceAsync<T>(string url, string oauthToken, string ClientID)
        {
            T Result = default(T);
            HttpClient client = null;
            HttpResponseMessage response = null;
            try
            {
                using (client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + oauthToken);
                    client.DefaultRequestHeaders.Add("ClientID", ClientID);
                    using (response = await client.GetAsync(url + "?=" + DateTime.Now.Ticks))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Result = await response.Content.ReadAsAsync<T>();
                         
                        }
                        else
                        {
                            var res = "{\"Status\":\"" + response.StatusCode + "\",\"StatusDescription\":\"" + response.Content.ToString() + "\",\"Result\": \"\" }";
                            string Res = (string)Convert.ChangeType(res, typeof(string));
                            _msg = Res;
                            log.Info(_msg);
                            Result = default(T);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var res = new[] { "{\"Status\":\"Timedout\",\"StatusDescription\":\"" + ex.Message + "\",\"Result\": \"\" }" };
                Result = (T)Convert.ChangeType(res, typeof(T));
                //new Models.Entity.ResponseStatus { Status = "Timedout", StatusDescription = ex.Message };
            }
            finally
            {
                if (client != null) client.Dispose();
                if (response != null) response.Dispose();
            }
            return Result;
        }



        public static async Task<string> PostFileAsync(string url, string oauthToken, HttpFileCollection data, string ClientID)
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
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + oauthToken);
                    client.DefaultRequestHeaders.Add("ClientID", ClientID);
                    var content = new MultipartFormDataContent();
                    System.Web.HttpPostedFile hpf = data[0];


                    byte[] fileData = null;
                    using (var sds = new BinaryReader(hpf.InputStream))
                    {
                        fileData = sds.ReadBytes(hpf.ContentLength);
                    }
                    content.Add(new ByteArrayContent(fileData, 0, fileData.Count()));
                    //using (response = await client.PostAsJsonAsync(url + "?=" + DateTime.Now.Ticks, data))
                    using (response = await client.PostAsync(url + "?=" + DateTime.Now.Ticks, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Result = await response.Content.ReadAsStringAsync();
                         
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            Result = "{\"Status\":\"UnAuthorized\",\"StatusDescription\": \"Unauth\",\"Result\": \"\"}";
                        }
                        else
                        {
                            Result = "{\"Status\":\"" + response.StatusCode + "\",\"StatusDescription\":\"" + response.Content.ToString() + "\",\"Result\": \"\" }";
                            _msg = Result;
                            log.Info(_msg);
                            Result = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "{\"Status\":\"Timedout\",\"StatusDescription\":" + ex.Message + ",\"Result\": \'\' }";
                //new Models.Entity.ResponseStatus { Status = "Timedout", StatusDescription = ex.Message };
            }
            finally
            {
                if (client != null) client.Dispose();
                if (response != null) response.Dispose();
            }
            return Result;
        }


        public static async Task<string> PostResourceAsync(string url, string oauthToken, Object data, string ClientID)
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
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + oauthToken);
                    client.DefaultRequestHeaders.Add("ClientID", ClientID);

                    using (response = await client.PostAsJsonAsync(url + "?=" + DateTime.Now.Ticks, data))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Result = await response.Content.ReadAsStringAsync();                     
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            Result = "{\"Status\":\"UnAuthorized\",\"StatusDescription\": \"Unauth\",\"Result\": \"\"}";
                        }
                        else
                        {
                            Result = "{\"Status\":\"" + response.StatusCode + "\",\"StatusDescription\":\"" + response.Content.ToString() + "\",\"Result\": \"\" }";
                            _msg = Result;
                            log.Info(_msg);
                            Result = await response.Content.ReadAsStringAsync();  
                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "{\"Status\":\"Timedout\",\"StatusDescription\":\"" + ex.Message + "\",\"Result\": \"\" }";
                //new Models.Entity.ResponseStatus { Status = "Timedout", StatusDescription = ex.Message };
            }
            finally
            {
                if (client != null) client.Dispose();
                if (response != null) response.Dispose();
            }
            return Result;
        }

        public static async Task<T> PostResourceAsync<T>(string url, string oauthToken, object data, string ClientID)
        {
            T Result = default(T);
            HttpClient client = null;
            HttpResponseMessage response = null;
            try
            {
                using (client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + oauthToken);
                    client.DefaultRequestHeaders.Add("ClientID", ClientID);
                    using (response = await client.PostAsJsonAsync(url + "?=" + DateTime.Now.Ticks, data))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Result = await response.Content.ReadAsAsync<T>();
                           
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {

                            var res = new[] { "{\"Status\":\"UnAuthorized\",\"StatusDescription\": \"Unauth\",\"Result\": \"\"}" };
                            Result = (T)Convert.ChangeType(res, typeof(T));
                        }
                        else
                        {
                            var res = "{\"Status\":\"" + response.StatusCode + "\",\"StatusDescription\":\"" + response.Content.ToString() + "\",\"Result\": \"\" }";
                            string Res = (string)Convert.ChangeType(res, typeof(string));
                            _msg = Res;
                            log.Info(_msg);
                            Result = await response.Content.ReadAsAsync<T>();
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var res = new[] { "{\"Status\":\"Timedout\",\"StatusDescription\":\"" + ex.Message + "\",\"Result\": \"\" }" };
                Result = (T)Convert.ChangeType(res, typeof(T));
            }
            finally
            {
                if (client != null) client.Dispose();
                if (response != null) response.Dispose();
            }
            return Result;
        }
        //public static async Task<string> GetResource1(string url, string oauthToken, string ClientID)
        //{
        //    string Result = "";
        //    HttpClient client = null;
        //    HttpResponseMessage response = null;
        //    using (client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + oauthToken);
        //        client.DefaultRequestHeaders.Add("ClientID", ClientID);
        //        using (response = client.GetAsync(url + "?=" + DateTime.Now.Ticks).Result)
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                Result = await response.Content.ReadAsStringAsync();
                       
        //            }
        //        }
        //    }
        //    if (client != null) client.Dispose();
        //    if (response != null) response.Dispose();
        //    return Result;
        //}
    }
}