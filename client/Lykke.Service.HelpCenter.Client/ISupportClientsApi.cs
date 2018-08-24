using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.Client.Models.Clients;
using Refit;

namespace Lykke.Service.HelpCenter.Client
{
    /// <summary>
    /// Help center support clients rest api client.
    /// </summary>
    [PublicAPI]
    public interface ISupportClientsApi
    {
        /// <summary>
        /// Saves or updates the support client.
        /// </summary>
        /// <param name="id">The client id.</param>
        /// <param name="name">The client name.</param>
        /// <returns>The support client data</returns>
        [Post("/api/SupportClients/{id}")]
        Task<SupportClientModel> SaveUser(string id, string name);

        /// <summary>
        /// Finds the support client.
        /// </summary>
        /// <param name="id">The client id.</param>
        /// <returns>The support client data</returns>
        [Get("/api/SupportClients/{id}")]
        Task<SupportClientModel> FindUser(string id);

        /// <summary>
        /// Deletes the support client.
        /// </summary>
        /// <param name="id">The client id.</param>
        [Delete("/api/SupportClients/{id}")]
        Task DeleteUser(string id);
    }
}
