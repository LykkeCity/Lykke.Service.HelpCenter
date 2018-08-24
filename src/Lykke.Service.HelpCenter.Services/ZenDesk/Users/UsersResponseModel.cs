using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Users
{
    public class UsersResponseModel
    {
        [JsonProperty("users")]
        public IReadOnlyList<UserModel> Users { get; set; }
    }
}
