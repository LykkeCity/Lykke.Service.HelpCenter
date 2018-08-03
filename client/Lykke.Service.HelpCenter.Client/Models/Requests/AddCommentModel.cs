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
        /// The comment text.
        /// </summary>
        [Required]
        public string Comment { get; set; }
    }
}
