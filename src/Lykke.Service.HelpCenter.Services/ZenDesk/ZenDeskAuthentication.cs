using System;
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
            _authHeader = "Basic " + EncodeTo64($"{settings.Email}/token:{settings.ApiKey}");
        }

        private static string EncodeTo64(string toEncode)
            => Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(toEncode));

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", _authHeader);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
