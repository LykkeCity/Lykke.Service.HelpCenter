using Autofac;
using Autofac.Core;
using Lykke.Service.HelpCenter.Core.Services;
using Lykke.Service.HelpCenter.Core.Settings;
using Lykke.Service.HelpCenter.Services.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.Caching.Distributed;

namespace Lykke.Service.HelpCenter.Services.Modules
{
    public sealed class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;

        public ServiceModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var settings = _settings.CurrentValue;

            builder.RegisterType<ClientAcountService>()
                .As<IClientAcountService>()
                .WithParameter(TypedParameter.From(settings.HelpCenterService.Cache))
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(IDistributedCache),
                        (pi, ctx) => ctx.ResolveKeyed<IDistributedCache>(settings.HelpCenterService.Cache.ClientsCacheInstance)))
                .SingleInstance();

            builder.RegisterType<RequestsService>()
                .As<IRequestsService>()
                .SingleInstance();
        }
    }
}
