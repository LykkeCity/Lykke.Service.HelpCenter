using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Requests
{
    internal class CommentsResponseModel
    {
        public IReadOnlyList<Details> Comments { get; set; }

        [JsonProperty("users")]
        public IReadOnlyList<Author> Authors { get; set; }

        internal class Details
        {
            public string Body { get; set; }

            [JsonProperty("author_id")]
            public string AuthorId { get; set; }
        }

        internal class Author
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }
    }
}
