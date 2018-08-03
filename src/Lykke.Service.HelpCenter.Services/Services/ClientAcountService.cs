using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.HelpCenter.Core.Domain.Clients;
using Lykke.Service.HelpCenter.Core.Services;

namespace Lykke.Service.HelpCenter.Services.Services
{
    public class ClientAcountService : IClientAcountService
    {
        private readonly IClientAccountClient _clientAccounts;
        private readonly ILog _log;

        public ClientAcountService(IClientAccountClient clientAccounts, ILogFactory logFactory)
        {
            _clientAccounts = clientAccounts;
            _log = logFactory.CreateLog(this);
        }

        public async Task<ClientModel> FindClient(string clientId)
        {
            // TODO caching
            try
            {
                var client = await _clientAccounts.GetByIdAsync(clientId);

                if (client == null)
                {
                    return null;
                }

                return new ClientModel
                {
                    ClientId = clientId,
                    Email = client.Email
                };
            }
            catch (Exception ex)
            {
                _log.Warning("Client could not be retrieved.", ex);
                return null;
            }
        }
    }
}
