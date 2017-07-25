using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DonorWebservice.Providers
{
    public class HeaderProvider : OAuthBearerAuthenticationProvider
    {
        readonly string _name;

        public HeaderProvider(string name)
        {
            _name = name;
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var value = context.Request.Headers.Get(_name);
            if (!string.IsNullOrEmpty(value))
            {
                context.Token = value;
            }

            return Task.FromResult<object>(null);
        }
    }
}