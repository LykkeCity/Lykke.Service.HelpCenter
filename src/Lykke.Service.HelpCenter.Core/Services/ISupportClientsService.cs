using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Core.Domain;
using Lykke.Service.HelpCenter.Core.Domain.Clients;

namespace Lykke.Service.HelpCenter.Core.Services
{
    public interface ISupportClientsService
    {
        Task<ClientModel> FindClientAsync(string clientId);
        Task<ClientModel> FindClientAsync(string clientId, string name);
        Task<ClientModel> SynchronizeUser(string clientId, string name);
        Task<ResponseModel> DeleteUser(string clientId);
    }
}
