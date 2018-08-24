using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Requests
{
    /// <summary>
    /// Request model for placing new requests.
    /// </summary>
    [PublicAPI]
    public class PlaceRequestModel
    {
        /// <summary>
        /// The Lyke client identifier.
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// The name of the client. When not provided the first part of the email address will be used as name.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// The request subject.
        /// </summary>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// The request description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// [Optional] The request type.
        /// </summary>
        public RequestType? Type { get; set; }
    }
}
