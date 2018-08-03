using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lykke.Service.HelpCenter.Core.Domain.Requests
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestStatus
    {
        [EnumMember(Value = "new")]
        New,

        [EnumMember(Value = "open")]
        Open,

        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "hold")]
        Hold,

        [EnumMember(Value = "solved")]
        Solved,

        [EnumMember(Value = "closed")]
        Closed
    }
}
