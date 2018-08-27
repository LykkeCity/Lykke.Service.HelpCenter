using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Core.Domain.Clients;

namespace Lykke.Service.HelpCenter.Core.Repositories
{
    public interface ISupportClientRepository
    {
        Task AddAsync(ClientModel model);

        Task<ClientModel> GetAsync(string clientId);

        Task DeleteAsync(string clientId);
    }
}
