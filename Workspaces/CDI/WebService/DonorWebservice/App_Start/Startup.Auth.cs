using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;

using Microsoft.Owin.Security.OAuth;
using Owin;
using DonorWebservice.Providers;
using DonorWebservice.Models;
using System.Web.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;

namespace DonorWebservice
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
       
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            var issuer = WebConfigurationManager.AppSettings["Issuer"];
            var audience = WebConfigurationManager.AppSettings["Audiance"];
            var secret = TextEncodings.Base64Url.Decode(
                WebConfigurationManager.AppSettings["ClientSecret"]);
            double TokenTimeout;
            if (!double.TryParse(WebConfigurationManager.AppSettings["WebTokenExpireTimeout"] ?? "", out TokenTimeout)) TokenTimeout = 1440;

            PublicClientId = audience;
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                //AuthorizeEndpointPath = new PathString("/home/login"),
                //AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(TokenTimeout),
                AccessTokenFormat = new CustomJWTFormat(issuer),
                //RefreshTokenFormat = new CustomJWTFormat(issuer),
                RefreshTokenProvider = new RefreshTokenProvider(),
                AllowInsecureHttp = true
            };

            //string token ="";
            var Client = new Models.ClientInfo();
            Client.LoadAudienceSecret();

            var options = new JwtBearerAuthenticationOptions
            {                
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                //AllowedAudiences = new[] {audience,"General","Salesforce","C2D61E08-F763-4786-9D8E-BEA23073733E"},
                AllowedAudiences = Client.GetAudience(),
               
                IssuerSecurityTokenProviders =
                new[]{   
                    
                    new SymmetricKeyIssuerSecurityTokenProvider(
                                issuer,
                                Client.GetSecret())
                    
                        //new SymmetricKeyIssuerSecurityTokenProvider(
                        //    issuer,
                        //    secret)
                        //    ,
                        //    new SymmetricKeyIssuerSecurityTokenProvider(
                        //    issuer,
                        //    TextEncodings.Base64Url.Decode("nh0FE10kao88xkJLJRikU7coevMr_aqE1uY_r-mJBfU"))
                        //    ,
                        //    new SymmetricKeyIssuerSecurityTokenProvider(
                        //    issuer,
                        //    TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw"))                            
                    },
                Provider = new QueryStringProvider("access_token")
            };

            //app.UseCors(CorsOptions.AllowAll);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseJwtBearerAuthentication(options);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
