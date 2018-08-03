using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.HelpCenter.Core.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
