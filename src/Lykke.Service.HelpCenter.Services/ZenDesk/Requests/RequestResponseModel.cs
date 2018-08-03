using Lykke.Service.HelpCenter.Core.Domain.Requests;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class RequestResponseModel
    {
        [JsonProperty("request")]
        public RequestModel Request { get; set; }
    }
}
