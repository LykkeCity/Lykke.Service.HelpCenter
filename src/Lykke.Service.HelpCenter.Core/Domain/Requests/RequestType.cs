using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lykke.Service.HelpCenter.Core.Domain.Requests
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestType
    {
        [EnumMember(Value = "none")]
        None,

        [EnumMember(Value = "question")]
        Question,

        [EnumMember(Value = "incident")]
        Incident,

        [EnumMember(Value = "problem")]
        Problem,

        [EnumMember(Value = "task")]
        Task
    }
}
