using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Requests
{
    /// <summary>
    /// Response model for request comments.
    /// </summary>
    [PublicAPI]
    public class CommentModel
    {
        /// <summary>
        /// The comment text.
        /// </summary>
        public string Text { get; set; }
    }
}
