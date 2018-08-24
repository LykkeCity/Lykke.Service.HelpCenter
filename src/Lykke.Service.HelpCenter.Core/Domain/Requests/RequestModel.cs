using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Core.Domain.Requests
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RequestModel
    {
        public string Id { get; set; }

        public RequestStatus Status { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public RequestType? Type { get; set; }

        public RequestPriority? Priority { get; set; }

        [JsonProperty("requester_id")]
        public string RequesterId { get; set; }
    }
}
