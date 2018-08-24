using System;
using Autofac;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.PeriodicHandlers;

namespace Lykke.Service.HelpCenter.Modules
{
    [UsedImplicitly]
    internal sealed class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserDataSyncronizer>()
                .WithParameter(TypedParameter.From(TimeSpan.FromDays(1)))
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}
