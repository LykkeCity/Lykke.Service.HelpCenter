using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client
{
    /// <summary>
    /// HelpCenter client interface.
    /// </summary>
    [PublicAPI]
    public interface IHelpCenterClient
    {
        /// <summary>Application Api interface</summary>
        IHelpCenterApi Api { get; }

        /// <summary>Requests Api interface</summary>
        IRequestsApi Requests { get; }

        /// <summary>Support clients Api interface</summary>
        ISupportClientsApi Clients { get; }
    }
}
