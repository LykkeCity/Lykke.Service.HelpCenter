using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class RequesterModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
