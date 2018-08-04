namespace Lykke.Service.HelpCenter.Core.Settings
{
    public class CacheSettings
    {
        public string RedisConfiguration { get; set; }

        public string ClientsCacheInstance { get; set; }
        public string ClientsCacheKeyPattern { get; set; }
    }
}
