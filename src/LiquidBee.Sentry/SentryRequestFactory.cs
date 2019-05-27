using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SharpRaven.Core.Data;

namespace LiquidBee.Sentry
{
    public class SentryRequestFactory : ISentryRequestFactory
    {
        private readonly IHttpContextAccessor _accessor;

        public SentryRequestFactory(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ISentryRequest Create()
        {
            var httpRequest = _accessor?.HttpContext?.Request;

            if (httpRequest == null) return new SentryRequest();

            var request = new SentryRequest
            {
                Url = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.Path}",
                Method = httpRequest.Method,
                QueryString = httpRequest.QueryString.ToString(),
                Headers = httpRequest.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                Cookies = httpRequest.Cookies.ToDictionary(c => c.Key, c => c.Value.ToString())
            };

            try
            {
                request.Data = HttpRequestBodyConverter.Convert(_accessor.HttpContext);
            }
            catch (Exception)
            {
                //
            }

            return request;
        }
    }
}