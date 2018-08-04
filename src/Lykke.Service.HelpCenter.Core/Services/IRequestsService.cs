using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Core.Domain.Clients;
using Lykke.Service.HelpCenter.Core.Domain.Requests;

namespace Lykke.Service.HelpCenter.Core.Services
{
    public interface IRequestsService
    {
        Task<string> PlaceRequest(ClientModel client, string subject, string description, RequestType type);

        Task<RequestModel> GetRequest(string id);

        Task<IEnumerable<RequestModel>> GetRequests(ClientModel client);

        Task<RequestModel> UpdateRequest(ClientModel client, string id, string comment);

        Task<IEnumerable<string>> GetComments(string id);
    }
}
