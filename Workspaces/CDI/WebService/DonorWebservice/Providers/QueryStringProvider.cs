using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using NLog;

namespace DonorWebservice.Providers
{
    public class QueryStringProvider : OAuthBearerAuthenticationProvider
    {
        readonly string _name;
        private string _token ;
        private Logger log = LogManager.GetCurrentClassLogger();
        public QueryStringProvider(string name)
        {
            _name = name;
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            //_token = context.Request.Headers.Get("Authorization");

            //if (string.IsNullOrEmpty(_token))
            //{
            //    _token = context.Request.Query.Get(_name);

            //}
            // context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            // context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type, Accept, Authorization" });

           //if(string.IsNullOrEmpty(_token))           
               _token = context.Request.Query.Get(_name);
            if (!string.IsNullOrEmpty(_token))
            {
                context.Token = _token;
                log.Info("Token :" + _token);
            }
            
            return Task.FromResult<object>(null);
        }

        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            bool IsClientIDRequired = false;
            var Claims = context.Ticket.Identity.Claims;
            string ClaimClientID = "";
            if (Claims != null)
            {
                foreach (var c in Claims)
                {
                    if (c.Type == "IsClientRequired")
                    {
                        if(c.Value == "True")  IsClientIDRequired = true;
                    }
                    else if (c.Type == "aud")
                    {
                        ClaimClientID = c.Value;
                    }
                }
            }
            var res = new Models.ClientInfo();
            res.ClientID = ClaimClientID;
            var url = context.Request.Uri.AbsolutePath;
            if( !res.IsResourceAllowed(url)){
                context.SetError("Unauthorized", "Resource not allowed.");
                return Task.FromResult<object>(null);
            }
            if (IsClientIDRequired)
            {
                var ClientID = context.Request.Headers.Get("ClientID");
                if (ClientID != null)
                {
                    if (ClientID == ClaimClientID)
                    {
                        return Task.FromResult<object>(null);
                    }
                }
                context.SetError("Unauthorized", "Client id is required, but not supplied or matched.");
            }

            //if (!string.IsNullOrEmpty(_token))
            //{
            //    var notPadded = _token.Split('.')[1];
            //    var claimsPart = Convert.FromBase64String(
            //        notPadded.PadRight(notPadded.Length + (4 - notPadded.Length % 4) % 4, '='));

            //    var obj = JObject.Parse(System.Text.Encoding.UTF8.GetString(claimsPart, 0, claimsPart.Length));

            //    // simple, not handling specific types, arrays, etc.
            //    foreach (var prop in obj.Properties().AsJEnumerable())
            //    {
            //        if (!context.Ticket.Identity.HasClaim(prop.Name, prop.Value.Value<string>()))
            //        {
            //            context.Ticket.Identity.AddClaim(new Claim(prop.Name, prop.Value.Value<string>()));
            //        }
            //    }
            //}

            return Task.FromResult<object>(null);
        }

    }
}