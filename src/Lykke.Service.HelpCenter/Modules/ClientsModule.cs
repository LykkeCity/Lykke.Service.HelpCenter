using Autofac;
using JetBrains.Annotations;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.HelpCenter.Core.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.HelpCenter.Modules
{
    [UsedImplicitly]
    internal sealed class ClientsModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;

        public ClientsModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var settings = _settings.CurrentValue;

            builder.RegisterLykkeServiceClient(settings.ClientAccountServiceClient.ServiceUrl);
        }
    }
}
