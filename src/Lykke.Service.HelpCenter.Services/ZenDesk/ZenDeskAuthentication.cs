using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Core.Settings;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    internal class ZenDeskAuthentication : DelegatingHandler
    {
        private readonly string _authHeader;

        public ZenDeskAuthentication(ZenDeskSettings settings)
        {
            _authHeader = $"Bearer {settings.OAuthToken}";
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", _authHeader);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
