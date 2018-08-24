using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Users
{
    public class UserModel
    {
        [JsonProperty("id")]
        public string ZenDeskId { get; set; }
        [JsonProperty("external_id")]
        public string ClientId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("verified")]
        public bool Verified { get; set; }
    }
}
