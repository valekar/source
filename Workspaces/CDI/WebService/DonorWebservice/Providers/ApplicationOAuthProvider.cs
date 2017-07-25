using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using DonorWebservice.Models;
using System.Web;
using NLog;
using System.Reflection;
namespace DonorWebservice.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private Logger log = LogManager.GetCurrentClassLogger();
        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            //validate your client  
            //var currentClient = context.ClientId;  

            //if (Client does not match)  
            //{  
            //    context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");  
            //    return Task.FromResult<object>(null);  
            //}  

            // Change authentication ticket for refresh token requests  
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            //newIdentity.AddClaim(new Claim("newClaim", "newValue"));
            newIdentity.AddClaim(new Claim("SessionID", Guid.NewGuid().ToString()));
            newIdentity.AddClaim(new Claim("Created", DateTime.Now.ToUniversalTime().ToString()));

            if (HttpContext.Current != null)
            {
                var UserAgent = (HttpContext.Current.Request.UserAgent ?? "").ToString();
                var HostAddress = (HttpContext.Current.Request.UserHostAddress ?? "").ToString();
                log.Info("IP Address: " + HostAddress + " Agent: " + UserAgent);
                //log.Info("Agent: " + UserAgent);
                newIdentity.AddClaim(new Claim("IP", HostAddress));
                newIdentity.AddClaim(new Claim("Agent", UserAgent));
            }
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        } 
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            //log.Info("ValidateClientAuthentication");            
            
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }
            //log.Info("Client ID: " + clientId);

            
            //var audience = AudiencesStore.FindAudience(context.ClientId);

            //if (audience == null)
            //{
            //    context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
            //    return Task.FromResult<object>(null);
            //}

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            context.RequestCompleted();
            var str = context.Options;
            //log.Info("AuthorizeEndpoint: ");
            //return base.AuthorizeEndpoint(context);
            return Task.FromResult<object>(null);
        }
        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {            
            return base.ValidateAuthorizeRequest(context);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var request = HttpContext.Current.Request;
           // bool isValid = false;
           // log.Info("GrantResourceOwnerCredentials: " + context.UserName);
           context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
           //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type, Accept, Authorization" });
           string ip = context.Request.RemoteIpAddress ?? request.UserHostAddress ?? "";

            var V = new Models.ClientInfo();
            V.UserName = context.UserName;
            V.Password = context.Password;
            V.ClientID = context.ClientId;
            V.IPAddress = ip;
            V.DNSName = request.UserHostName ?? "";
            var HostAddress = V.IPAddress;
            //if (context.UserName != "Test" || context.Password != "Password")
            if(!V.IsValid())
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return Task.FromResult<object>(null); 
            }
            var ClientIDReq = V.IsClientIDRequired;
            var ClientSectet = V.ClientSecret ?? "";
            if (!V.IsIPValid())
            {
                context.SetError("invalid_ip", "IP Address does not match.");
                return Task.FromResult<object>(null);
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("SessionID", Guid.NewGuid().ToString()));
            identity.AddClaim(new Claim("Created", DateTime.Now.ToUniversalTime().ToString()));

            if (HttpContext.Current != null)
            {
                var UserAgent = (HttpContext.Current.Request.UserAgent ?? "").ToString();
                //var HostAddress =  (HttpContext.Current.Request.UserHostAddress ?? "").ToString();

                log.Info("Client ID: " + context.ClientId + " IP Address: " + HostAddress + " Agent: " + UserAgent);
                //log.Info("Agent: " + UserAgent);
                identity.AddClaim(new Claim("IP", HostAddress));
                identity.AddClaim(new Claim("Agent", UserAgent));
                identity.AddClaim(new Claim("IsClientRequired", ClientIDReq.ToString()));
            }

            AuthenticationProperties properties = CreateProperties(context.ClientId, ClientSectet);       //_publicClientId     
            AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
            //context.Request.Context.Authentication.SignIn(identity);
           // log.Info("Ticket Created.");
            context.Validated(ticket);
            return Task.FromResult<object>(null);
                        
            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}

            //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
            //   OAuthDefaults.AuthenticationType);
            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        //public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //     Resource owner password credentials does not provide a client ID.
        //    if (context.ClientId == null)
        //    {
        //        context.Validated();
        //    }

        //    return Task.FromResult<object>(null);
        //}

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string clientId, string secret)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "audience", clientId },
                { "secret", secret }
            };
            return new AuthenticationProperties(data);
        }
    }
}