using Lykke.HttpClientGenerator;

namespace Lykke.Service.HelpCenter.Client.Internal
{
    internal sealed class HelpCenterClient : IHelpCenterClient
    {
        public HelpCenterClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IHelpCenterApi>();
            Requests = httpClientGenerator.Generate<IRequestsApi>();
            Clients = httpClientGenerator.Generate<ISupportClientsApi>();
        }

        public IHelpCenterApi Api { get; }

        public IRequestsApi Requests { get; }

        public ISupportClientsApi Clients { get; }
    }
}
