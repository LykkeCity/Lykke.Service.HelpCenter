using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Client.Models.Common
{
    /// <summary>
    /// Error response model.
    /// </summary>
    [PublicAPI]
    public class ErrorModel
    {
        /// <summary>The error that occured.</summary>
        public string Error { get; set; }
    }
}
