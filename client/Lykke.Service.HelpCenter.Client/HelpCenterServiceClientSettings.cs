namespace Lykke.Service.HelpCenter.Client
{
    /// <summary>
    /// HelpCenter client settings.
    /// </summary>
    public sealed class HelpCenterServiceClientSettings
    {
        /// <summary>Service url.</summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Creates client settings based on specified help center url.
        /// </summary>
        /// <param name="helpCenterUrl">The help center service url.</param>
        public static HelpCenterServiceClientSettings Create(string helpCenterUrl)
        {
            return new HelpCenterServiceClientSettings
            {
                ServiceUrl = helpCenterUrl
            };
        }
    }
}
