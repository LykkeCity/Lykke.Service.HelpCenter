using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    public class ZenDeskCaching : DelegatingHandler
    {
        private readonly CacheControlHeaderValue _cacheControl = CacheControlHeaderValue.Parse("no-cache, no-store");

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            result.Headers.CacheControl = _cacheControl;
            return result;
        }
    }
}
