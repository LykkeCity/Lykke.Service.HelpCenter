using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.Client.Models.Requests;
using Refit;

namespace Lykke.Service.HelpCenter.Client
{
    /// <summary>
    /// Help center requests rest api client.
    /// </summary>
    [PublicAPI]
    public interface IRequestsApi
    {
        /// <summary>
        /// Places a new help center request.
        /// </summary>
        /// <param name="model">The request details.</param>
        /// <returns>the created request</returns>
        [Post("/api/Requests")]
        Task<RequestModel> PlaceRequest([NotNull, Body] PlaceRequestModel model);

        /// <summary>
        /// Adds a comment to an existing request.
        /// </summary>
        /// <param name="id">The ticket id.</param>
        /// <param name="model">The comment details.</param>
        [Post("/api/Requests/{id}/comments")]
        Task AddComment([NotNull]string id, [NotNull, Body] AddCommentModel model);

        /// <summary>
        /// Gets an existing request.
        /// </summary>
        /// <param name="id">The ticket id.</param>
        /// <returns>the requested request</returns>
        [Get("/api/Requests/{id}")]
        Task<RequestModel> GetRequest([NotNull]string id);

        /// <summary>
        /// Gets all requests of a lykke client.
        /// </summary>
        /// <param name="clientId">The lykke client identifier.</param>
        /// <returns>the list of requests</returns>
        [Get("/api/Requests/clients/{clientId}")]
        Task<IReadOnlyList<RequestModel>> GetRequests([NotNull]string clientId);

        /// <summary>
        /// Gets all comments of a specific request.
        /// </summary>
        /// <param name="id">The ticket id.</param>
        /// <returns>the list of comments</returns>
        [Get("/api/{id}/comments")]
        Task<IReadOnlyList<CommentModel>> GetComments([NotNull]string id);
    }
}
