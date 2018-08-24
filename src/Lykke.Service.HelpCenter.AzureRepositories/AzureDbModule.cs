using Autofac;
using AzureStorage;
using AzureStorage.Tables;
using Lykke.Common.Log;
using Lykke.Service.HelpCenter.Core.Repositories;
using Lykke.Service.HelpCenter.Core.Settings;
using Lykke.SettingsReader;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.HelpCenter.AzureRepositories
{
    public sealed class AzureDbModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;

        public AzureDbModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAzureTableStorage<SupportClientEntity>(_settings, @"zendeskclients");

            builder.RegisterType<SupportClientRepository>()
                .As<ISupportClientRepository>()
                .SingleInstance();
        }
    }

    internal static class AzureBuilderExtension
    {
        public static void RegisterAzureTableStorage<T>(this ContainerBuilder builder, IReloadingManager<AppSettings> settings, string tableName)
            where T : class, ITableEntity, new()
        {
            builder.Register(x => AzureTableStorage<T>.Create(
                    settings.ConnectionString(s => s.HelpCenterService.Db.AzureRepositoriesConnString),
                    tableName,
                    x.Resolve<ILogFactory>()))
                .As<INoSQLTableStorage<T>>();
        }
    }
}
