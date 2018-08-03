using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Requests
{
    /// <summary>
    /// Lykke request priority
    /// </summary>
    [PublicAPI]
    public enum RequestPriority
    {
        /// <summary>Low priority</summary>
        Low,

        /// <summary>Normal priority</summary>
        Normal,

        /// <summary>High priority</summary>
        High,

        /// <summary>Urgent priority</summary>
        Urgent
    }
}
