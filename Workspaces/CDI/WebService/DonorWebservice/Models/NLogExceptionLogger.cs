using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace DonorWebservice.Models
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();
        public override void Log(ExceptionLoggerContext context)
        {
            Nlog.Log(LogLevel.Error,context.Exception, RequestToString(context.Request) );
        }

        public async override Task LogAsync(ExceptionLoggerContext context, System.Threading.CancellationToken cancellationToken)
        {
            Nlog.Log(LogLevel.Error, context.Exception, RequestToString(context.Request));
            await base.LogAsync(context, cancellationToken);
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
                message.Append(request.Method);

            if (request.RequestUri != null)
                message.Append(" ").Append(request.RequestUri);

            return message.ToString();
        }
    }
}