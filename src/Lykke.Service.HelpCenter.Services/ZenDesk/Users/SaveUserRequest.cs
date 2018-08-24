using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Users
{
    public class SaveUserRequest
    {
        [JsonProperty("user")]
        public UserModel User { get; set; }
    }
}
