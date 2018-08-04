using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Core.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class HelpCenterSettings
    {
        public DbSettings Db { get; set; }

        public ZenDeskSettings ZenDesk { get; set; }

        public CacheSettings Cache { get; set; }
    }
}
