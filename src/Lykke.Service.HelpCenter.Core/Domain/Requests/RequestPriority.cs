using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Lykke.Service.HelpCenter.Core.Domain.Requests
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public enum RequestPriority
    {
        [EnumMember(Value = "low")]
        Low,

        [EnumMember(Value = "normal")]
        Normal,

        [EnumMember(Value = "high")]
        High,

        [EnumMember(Value = "urgent")]
        Urgent
    }
}
