using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.HelpCenter.Core.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public HelpCenterSettings HelpCenterService { get; set; }

        public ClientAccountServiceClient ClientAccountServiceClient { get; set; }
    }
}
