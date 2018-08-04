using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Requests
{
    /// <summary>
    /// Request model for adding new comments to requests.
    /// </summary>
    [PublicAPI]
    public class AddCommentModel
    {
        /// <summary>
        /// The id of the client that adds a comment to the request.
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// The comment text.
        /// </summary>
        [Required]
        public string Comment { get; set; }
    }
}
