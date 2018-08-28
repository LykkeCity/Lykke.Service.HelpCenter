using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Common
{
    public class ZenDeskErrorValue
    {
        public string Description { get; set; }
    }

    public class ZenDeskErrorDetails
    {
        [JsonProperty("base")]
        public IReadOnlyList<ZenDeskErrorValue> Value { get; set; }
    }

    public class ZenDeskError
    {
        public ZenDeskErrorDetails Details { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }

        public override string ToString()
        {
            if (Details.Value != null && Details.Value.Any(x => !string.IsNullOrWhiteSpace(x.Description)))
            {
                return Details.Value
                    .Select(x => x.Description)
                    .FirstOrDefault();
            }

            return string.IsNullOrWhiteSpace(Description) ? Error : Description;
        }
    }
}
