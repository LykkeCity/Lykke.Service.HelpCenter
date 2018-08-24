using Autofac;
using Lykke.Service.HelpCenter.Core.Settings;
using Lykke.Service.HelpCenter.Services.ZenDesk;
using Lykke.SettingsReader;

namespace Lykke.Service.HelpCenter.Services.Modules
{
    public class ZenDeskClientsModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ZenDeskClientsModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var generator = HttpClientGenerator.HttpClientGenerator
                .BuildForUrl(_appSettings.CurrentValue.HelpCenterService.ZenDesk.Url)
                .WithoutCaching()
                .WithAdditionalDelegatingHandler(new ZenDeskAuthentication(_appSettings.CurrentValue.HelpCenterService.ZenDesk))
                .WithAdditionalDelegatingHandler(new ZenDeskCaching())
                .Create();

            builder.RegisterZenDeskClient<IRequestsApi>(generator);
            builder.RegisterZenDeskClient<IUsersApi>(generator);
        }
    }

    internal static class BuilderExtensions
    {
        public static void RegisterZenDeskClient<TApi>(this ContainerBuilder builder,
            HttpClientGenerator.HttpClientGenerator generator)
        {
            builder.Register(x => generator.Generate<TApi>())
                .SingleInstance();
        }
    }
}
