using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SharpRaven.Core;
using SharpRaven.Core.Data;

namespace LiquidBee.Sentry
{
    public class SentryReporter
    {
        private readonly IRavenClient _client;

        public SentryReporter(IOptions<SentryOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            _client = new RavenClient(options.Value.Dsn ?? "", sentryRequestFactory: new SentryRequestFactory(httpContextAccessor));
        }

        public async Task<string> CaptureAsync(Exception exception)
        {
            return await _client.CaptureAsync(new SentryEvent(exception));
        }
    }
}