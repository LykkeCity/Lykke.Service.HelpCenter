using System.Collections.Generic;
using Lykke.Service.HelpCenter.Core.Domain.Requests;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class RequestsResponseModel
    {
        [JsonProperty("requests")]
        public IReadOnlyList<RequestModel> Requests { get; set; }
    }
}
