using Autofac;
using Lykke.Service.HelpCenter.Core.Services;
using Lykke.Service.HelpCenter.Services.Services;

namespace Lykke.Service.HelpCenter.Services.Modules
{
    public sealed class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SupportClientsService>()
                .As<ISupportClientsService>()
                .SingleInstance();

            builder.RegisterType<RequestsService>()
                .As<IRequestsService>()
                .SingleInstance();
        }
    }
}
