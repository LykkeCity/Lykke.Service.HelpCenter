using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Clients
{
    /// <summary>
    /// Model for lykke support clients.
    /// </summary>
    [PublicAPI]
    public class SupportClientModel
    {
        /// <summary>The Lykke client id</summary>
        public string ClientId { get; set; }

        /// <summary>The support client id (Zendesk)</summary>
        public string SupportId { get; set; }

        /// <summary>The support client name</summary>
        public string Name { get; set; }

        /// <summary>The support client email</summary>
        public string Email { get; set; }
    }
}
