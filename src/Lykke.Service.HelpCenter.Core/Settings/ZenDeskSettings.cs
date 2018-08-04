using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.HelpCenter.Core.Settings
{
    public class ZenDeskSettings
    {
        [HttpCheck("")]
        public string Url { get; set; }
        public string OAuthToken { get; set; }
    }
}
