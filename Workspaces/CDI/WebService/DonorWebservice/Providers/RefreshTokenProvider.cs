﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace DonorWebservice.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {

        private string ComputeHash(Guid input)
        {
            byte[] source = input.ToByteArray();

            var encoder = new SHA256Managed();
            byte[] encoded = encoder.ComputeHash(source);

            return Convert.ToBase64String(encoded);
        }

        private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            //var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            //if (string.IsNullOrEmpty(clientid))
            //{
            //    return;
            //}

            var guid = Guid.NewGuid();

            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(5) //SET DATETIME to 5 Minutes
                //ExpiresUtc = DateTime.UtcNow.AddMonths(3) 
            };
            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);
            _refreshTokens.TryAdd(ComputeHash(guid), refreshTokenTicket);
            context.SetToken(guid.ToString());            
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            Guid token;

            if (Guid.TryParse(context.Token, out token))
            {
                AuthenticationTicket ticket;

                if (_refreshTokens.TryRemove(ComputeHash(token), out ticket))
                {
                    context.SetTicket(ticket);
                }
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    
    }
    
}