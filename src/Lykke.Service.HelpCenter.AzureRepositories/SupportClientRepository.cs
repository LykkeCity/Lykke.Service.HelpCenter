using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.HelpCenter.Core.Domain.Clients;
using Lykke.Service.HelpCenter.Core.Repositories;

namespace Lykke.Service.HelpCenter.AzureRepositories
{
    internal class SupportClientRepository : ISupportClientRepository
    {
        private readonly string _rowKey = string.Empty;
        private readonly INoSQLTableStorage<SupportClientEntity> _clientsTable;

        public SupportClientRepository(INoSQLTableStorage<SupportClientEntity> orderStateTable)
        {
            _clientsTable = orderStateTable;
        }

        public async Task<ClientModel> GetAsync(string clientId)
        {
            var entity = await _clientsTable.GetDataAsync(clientId, _rowKey);
            if (entity == null)
                return null;

            return new ClientModel
            {
                ClientId = clientId,
                Email = entity.Email,
                Name = entity.Name,
                ZenDeskUserId = entity.ZenDeskUserId
            };
        }

        public async Task DeleteAsync(string clientId)
        {
            await _clientsTable.DeleteIfExistAsync(clientId, _rowKey).ConfigureAwait(false);
        }

        public async Task AddAsync(ClientModel model)
        {
            var entity = new SupportClientEntity
            {
                ClientId = model.ClientId,
                RowKey = _rowKey,
                ZenDeskUserId = model.ZenDeskUserId,
                Email = model.Email,
                Name = model.Name
            };

            await _clientsTable.InsertOrReplaceAsync(entity).ConfigureAwait(false);
        }
    }
}
