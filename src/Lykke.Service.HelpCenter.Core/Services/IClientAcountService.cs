using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Core.Domain.Clients;

namespace Lykke.Service.HelpCenter.Core.Services
{
    public interface IClientAcountService
    {
        Task<ClientModel> FindClient(string clientId);
    }
}
