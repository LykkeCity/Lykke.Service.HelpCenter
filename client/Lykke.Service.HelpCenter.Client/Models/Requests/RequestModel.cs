using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Requests
{
    /// <summary>
    /// Response model for helpcenter requests.
    /// </summary>
    [PublicAPI]
    public class RequestModel
    {
        /// <summary>
        /// The ticket id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The request status.
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// The request subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The request description (first comment).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The request type.
        /// </summary>
        public RequestType? Type { get; set; }

        /// <summary>
        /// The request priority.
        /// </summary>
        public RequestPriority? Priority { get; set; }
    }
}
