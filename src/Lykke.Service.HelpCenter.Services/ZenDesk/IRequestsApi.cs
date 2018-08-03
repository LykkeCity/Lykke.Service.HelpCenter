using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.Services.ZenDesk.Requests;
using Refit;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    [PublicAPI]
    [Headers("Authorization")]
    internal interface IRequestsApi
    {
        [Post("/api/v2/requests.json")]
        Task<RequestResponseModel> PlaceRequest([Body] CreateRequestModel toAdd);

        [Get("/api/v2/requests/{id}.json")]
        Task<RequestResponseModel> GetRequest([Query] string id);

        [Get("/api/v2/requests/search.json?requester:{email}")]
        Task<RequestsResponseModel> GetRequests([Query] string email);

        [Put("/api/v2/requests/{id}.json")]
        Task<RequestResponseModel> UpdateRequest([Query] string id, [Body] UpdateRequestModel request);

        [Get("/api/v2/requests/{id}/comments.json")]
        Task<CommentsResponseModel> GetComments([Query] string id);
    }
}
