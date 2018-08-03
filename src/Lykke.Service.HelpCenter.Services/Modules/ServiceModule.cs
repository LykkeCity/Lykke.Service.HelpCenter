using Autofac;
using Lykke.Service.HelpCenter.Core.Services;
using Lykke.Service.HelpCenter.Services.Services;

namespace Lykke.Service.HelpCenter.Services.Modules
{
    public sealed class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClientAcountService>()
                .As<IClientAcountService>()
                .SingleInstance();

            builder.RegisterType<RequestsService>()
                .As<IRequestsService>()
                .SingleInstance();
        }
    }
}
