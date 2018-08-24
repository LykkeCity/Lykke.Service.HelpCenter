using Refit;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    public class OnBehalveOfHeaderAttribute : HeaderAttribute
    {
        public OnBehalveOfHeaderAttribute()
            : base("X-On-Behalf-Of")
        {
        }
    }
}
