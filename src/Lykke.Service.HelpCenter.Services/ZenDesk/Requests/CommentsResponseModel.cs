using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class CommentsResponseModel
    {
        [JsonProperty("comments")]
        public IReadOnlyList<CommentModel> Comments { get; set; }
    }
}
