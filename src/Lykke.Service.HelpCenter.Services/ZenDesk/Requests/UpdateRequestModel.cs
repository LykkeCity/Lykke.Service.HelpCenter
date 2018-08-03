using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class UpdateRequestModel
    {
        [JsonProperty("request")]
        public Details Request { get; set; }

        public class Details
        {
            [JsonProperty("comment")]
            public CommentModel Comment { get; set; }
        }
    }
}
