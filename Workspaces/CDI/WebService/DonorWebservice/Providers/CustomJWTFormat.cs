using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thinktecture.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Constants;
using System.Security.Cryptography;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using NLog;
using System.IdentityModel.Tokens;

namespace DonorWebservice.Providers
{
   
   public class CustomJWTFormat : ISecureDataFormat<AuthenticationTicket>
        {
            private const string AudiencePropertyKey = "audience";
            private const string SecretPropertyKey = "secret";

            //private Logger log = LogManager.GetCurrentClassLogger();

            private readonly string _issuer = string.Empty;

            public CustomJWTFormat(string issuer)
            {
                _issuer = issuer;
            }

            public string Protect(AuthenticationTicket data)
            {
                //log.Info("Protect");
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;
                string symmetricKeyAsBase64 = data.Properties.Dictionary.ContainsKey(SecretPropertyKey) ? data.Properties.Dictionary[SecretPropertyKey] : null;
                if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");
                if (string.IsNullOrWhiteSpace(symmetricKeyAsBase64)) throw new InvalidOperationException("Secretkey.Properties does not include audience");

                //Audience audience = AudiencesStore.FindAudience(audienceId);

                //var key = new byte[32];
                //RNGCryptoServiceProvider.Create().GetBytes(key);
                //var base64Secret = TextEncodings.Base64Url.Encode(key);

                //string symmetricKeyAsBase64 = "";
                //if (audienceId == "General")
                //{
                //    symmetricKeyAsBase64 = "nh0FE10kao88xkJLJRikU7coevMr_aqE1uY_r-mJBfU";
                //}
                //else
                //{
                //    symmetricKeyAsBase64 = "vtqJ429_Uy-1TjVNWzLhf_WoDM8FRb549a7_axb9cSU";
                //}

                var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

                var signingKey = new HmacSigningCredentials(keyByteArray);

                var issued = data.Properties.IssuedUtc;
                var expires = data.Properties.ExpiresUtc ;

                var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

                var handler = new JwtSecurityTokenHandler();

                var jwt = handler.WriteToken(token);
                //log.Info("JWT Token Written.");
                return jwt;
            }

            public AuthenticationTicket Unprotect(string protectedText)
            {
                throw new NotImplementedException();
            }
        }
    
}