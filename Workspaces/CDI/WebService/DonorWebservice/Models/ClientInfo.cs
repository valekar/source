using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorWebservice.Models
{
    public class ClientInfo
    {
        public string ClientID { get; set; }
        public string UserName { get; set; }
        public string ClientName { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public string DNSName { get; set; }
        public string UserAgent { get; set; }
        public bool IsIPRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsClientIDRequired { get; set; }
        public string ClientSecret { get; set; }
        public string ResourceName { get; set; }
        
        private bool _IsIPvalid = false;

        private IEnumerable<string> _Audience { get;  set; }
        private IEnumerable<byte[]> _Secret { get; set; }
        public bool IsValid()
        {
            Guid ClientID;

            if (!Guid.TryParse(this.ClientID, out ClientID)) return false;

            var repClient = new ClientValidation.Repository<ClientValidation.ClientInfo>();
            var client = repClient.Get(g => g.ClientID == ClientID).FirstOrDefault();

            //Hardcoding values since SQL is currently not installed in the server.
            
            //ClientValidation.ClientInfo client = new ClientValidation.ClientInfo();
            //client.ClientID = new Guid("C2D61E08-F763-4786-9D8E-BEA23073733E");
            //client.ClientSecret = "nh0FE10kao88xkJLJRikU7coevMr_aqE1uY_r-mJBfU";
            //client.Client_Name = "Stuart";
            //client.IPAddresss = "";
            //client.DNSName = "";
            //client.IsIPRequired = false;
            //client.IsClientIDRequired = false;
            //client.UserName = "StuartSVC";
            //client.Password = "St@S3rv!ce";

            if (client == null) return false;

            if (client.ClientID != ClientID) return false;
                
            this.IsActive = client.Active;
            this.ClientName = client.Client_Name;
            this.ClientSecret = client.ClientSecret;
           
            if (this.IPAddress == client.IPAddresss || (this.DNSName ?? "").ToLower() == (client.DNSName ?? "").ToLower()) _IsIPvalid = true;
            this.IsIPRequired = client.IsIPRequired ?? false;
            this.IsClientIDRequired = client.IsClientIDRequired ?? false;                
                
            if ( this.UserName == client.UserName && this.Password == client.Password)
            {
                    
                return true;
            }

            return false; 
        }

        public bool IsIPValid()
        {
            if(this.IsIPRequired)
            return _IsIPvalid;

            return true;
        }

        public IEnumerable<string> GetAudience()
        {
            return _Audience;
        }

        public IEnumerable<byte[]> GetSecret()
        {
            return _Secret;
        }

        public bool LoadAudienceSecret()
        {
             var repClient = new ClientValidation.Repository<ClientValidation.ClientInfo>();
             var client = repClient.GetAll();     


            ////Hardcoding values since SQL is currently not installed in the server.
            // List<ClientValidation.ClientInfo> client = new List<ClientValidation.ClientInfo>();
            //ClientValidation.ClientInfo cInfo = new ClientValidation.ClientInfo();
            //cInfo.ClientID = new Guid("C2D61E08-F763-4786-9D8E-BEA23073733E");
            //cInfo.ClientSecret = "nh0FE10kao88xkJLJRikU7coevMr_aqE1uY_r-mJBfU";

            //client.Add(cInfo);

             if (client != null)
             {
                 _Audience = client.Select(s => s.ClientID.ToString().ToUpper()).Distinct();
                 _Secret = client.Select(s => TextEncodings.Base64Url.Decode(s.ClientSecret.ToString())).Distinct();
                 return true;
             }

             return false;
        }

        public bool IsResourceAllowed(string url)
        {
            Guid ClientID;

            if (!Guid.TryParse(this.ClientID, out ClientID)) return false;

            var repClient = new ClientValidation.Repository<ClientValidation.Resource>();
            var client = repClient.Get(g => g.ClientID == ClientID).ToList(); //Commented out since the SQL server is not yet set up


            // The below values are hardcoded since the SQL server is not yet set up. Once the server and database is set up, these can be deleted. 
            //List<ClientValidation.Resource> client = new List<ClientValidation.Resource>();
            //ClientValidation.Resource cinfo = new ClientValidation.Resource();
            //cinfo.ClientID = new Guid("C2D61E08-F763-4786-9D8E-BEA23073733E");
            //cinfo.ResourceName = "advsearch";
            //client.Add(cinfo);

            if (client == null) return false;           
            url = (url ?? "").ToLower();
            foreach (var c in client)
            {
                if(url.Contains(c.ResourceName.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}