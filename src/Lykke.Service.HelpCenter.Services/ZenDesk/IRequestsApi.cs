using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.Services.ZenDesk.Requests;
using Refit;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    [PublicAPI]
    [Headers(ZenDeskAuthentication.Authorization)]
    internal interface IRequestsApi
    {
        [Post(@"/api/v2/requests.json")]
        Task<RequestResponseModel> PlaceRequest([OnBehalveOfHeader] string email, [Body] CreateRequestModel toAdd);

        [Get(@"/api/v2/requests/{id}.json")]
        Task<RequestResponseModel> GetRequest([Query] string id);

        [Get(@"/api/v2/search.json")]
        Task<SearchRequestsResponseModel> SearchRequests(string query);

        [Put(@"/api/v2/requests/{id}.json")]
        Task<RequestResponseModel> UpdateRequest([OnBehalveOfHeader] string email, [Query] string id, [Body] UpdateRequestModel request);

        [Get(@"/api/v2/requests/{id}/comments.json")]
        Task<CommentsResponseModel> GetComments([Query] string id);
    }
}
