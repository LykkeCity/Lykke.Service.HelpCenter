using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Requests
{
    /// <summary>
    /// Lykke request type
    /// </summary>
    [PublicAPI]
    public enum RequestType
    {
        /// <summary>A question</summary>
        Question,

        /// <summary>A incident</summary>
        Incident,

        /// <summary>A problem</summary>
        Problem,

        /// <summary>A task</summary>
        Task
    }
}
