using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.HelpCenter.AzureRepositories
{
    internal class SupportClientEntity : TableEntity
    {
        public string ZenDeskUserId { get; set; }

        [IgnoreProperty]
        public string ClientId
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
