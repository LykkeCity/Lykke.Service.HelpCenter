using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class ZenDeskCommentModel
    {
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
