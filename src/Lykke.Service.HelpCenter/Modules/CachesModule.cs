using Autofac;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.Core.Settings;
using Lykke.SettingsReader;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;

namespace Lykke.Service.HelpCenter.Modules
{
    [UsedImplicitly]
    internal sealed class CachesModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;

        public CachesModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var settings = _settings.CurrentValue;

            RegisterCaches(builder, settings.HelpCenterService.Cache);
        }

        private static void RegisterCaches(ContainerBuilder builder, CacheSettings settings)
        {
            var clientsRedisCache = new RedisCache(new RedisCacheOptions
            {
                Configuration = settings.RedisConfiguration,
                InstanceName = settings.ClientsCacheInstance
            });

            builder.RegisterInstance(clientsRedisCache)
                .As<IDistributedCache>()
                .Keyed<IDistributedCache>(settings.ClientsCacheInstance)
                .SingleInstance();
        }
    }
}
