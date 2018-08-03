using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Requests
{
    /// <summary>
    /// Lykke request status
    /// </summary>
    [PublicAPI]
    public enum RequestStatus
    {
        /// <summary>New request</summary>
        New,

        /// <summary>Requestis open</summary>
        Open,

        /// <summary>Request is pending</summary>
        Pending,

        /// <summary>Request is on hold</summary>
        Hold,

        /// <summary>Request has been resolved</summary>
        Solved,

        /// <summary>Request has been closed</summary>
        Closed
    }
}
