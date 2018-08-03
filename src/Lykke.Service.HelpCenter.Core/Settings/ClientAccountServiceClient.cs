using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.HelpCenter.Core.Settings
{
    public class ClientAccountServiceClient
    {
        [HttpCheck("api/isalive")]
        public string ServiceUrl { get; set; }
    }
}
