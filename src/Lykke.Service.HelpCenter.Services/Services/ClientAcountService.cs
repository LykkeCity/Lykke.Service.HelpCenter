using System;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.HelpCenter.Core.Domain.Clients;
using Lykke.Service.HelpCenter.Core.Services;
using Lykke.Service.HelpCenter.Core.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Lykke.Service.HelpCenter.Services.Services
{
    public class ClientAcountService : IClientAcountService
    {
        private readonly IClientAccountClient _clientAccounts;
        private readonly IDistributedCache _clientsCache;
        private readonly CacheSettings _settings;
        private readonly ILog _log;

        public ClientAcountService(IClientAccountClient clientAccounts, ILogFactory logFactory, IDistributedCache clientsCache, CacheSettings settings)
        {
            _clientAccounts = clientAccounts;
            _clientsCache = clientsCache;
            _settings = settings;
            _log = logFactory.CreateLog(this);
        }

        public async Task<ClientModel> FindClient(string clientId)
        {
            var cached = await TryFindCached(clientId);
            if (cached != null)
            {
                return cached;
            }

            try
            {
                var client = await _clientAccounts.GetByIdAsync(clientId);

                if (client == null)
                {
                    return null;
                }

                var model = new ClientModel
                {
                    ClientId = clientId,
                    Email = client.Email
                };

                AddToCache(model);
                return model;
            }
            catch (Exception ex)
            {
                _log.Warning("Client could not be retrieved.", ex);
                return null;
            }
        }

        private async Task<ClientModel> TryFindCached(string clientId)
        {
            var key = string.Format(_settings.ClientsCacheKeyPattern, clientId);
            var data = await _clientsCache.GetStringAsync(key);

            return string.IsNullOrEmpty(data) 
                ? null 
                : JsonConvert.DeserializeObject<ClientModel>(data);
        }

        private async void AddToCache(ClientModel client)
        {
            var key = string.Format(_settings.ClientsCacheKeyPattern, client.ClientId);
            var data = client.ToJson();

            await _clientsCache.SetStringAsync(key, data);
        }
    }
}
