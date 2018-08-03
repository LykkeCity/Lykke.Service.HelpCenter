using Lykke.Service.HelpCenter.Core.Domain.Requests;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class CreateRequestModel
    {
        [JsonProperty("request")]
        public Details Request { get; set; }

        public class Details
        {
            [JsonProperty("requester")]
            public RequesterModel Requester { get; set; }

            [JsonProperty("subject")]
            public string Subject { get; set; }

            [JsonProperty("comment")]
            public CommentModel Comment { get; set; }

            [JsonProperty("type")]
            public RequestType Type { get; set; }
        }
    }
}
