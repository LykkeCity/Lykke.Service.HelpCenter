using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.HelpCenter.Client 
{
    /// <summary>
    /// HelpCenter client settings.
    /// </summary>
    public class HelpCenterServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
