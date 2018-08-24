using System.Collections.Generic;

namespace Lykke.Service.HelpCenter.Services.ZenDesk.Common
{
    public class ZenDeskErrorValue
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class ZenDeskErrorDetails
    {
        public List<ZenDeskErrorValue> Value { get; set; }
    }

    public class ZenDeskError
    {
        public ZenDeskErrorDetails Details { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
    }
}
