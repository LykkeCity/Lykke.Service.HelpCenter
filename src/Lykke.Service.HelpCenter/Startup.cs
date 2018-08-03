using System;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.HelpCenter.Core.Settings;
using Lykke.Service.HelpCenter.Services.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.HelpCenter
{
    [UsedImplicitly]
    internal sealed class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "HelpCenter API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "HelpCenterLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.HelpCenterService.Db.LogsConnString;
                };

                options.RegisterAdditionalModules = x =>
                {
                    x.RegisterModule<ZenDeskClientsModule>();
                    x.RegisterModule<ServiceModule>();
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;
            });
        }
    }
}
