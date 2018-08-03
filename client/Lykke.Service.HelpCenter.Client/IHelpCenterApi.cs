using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.Client.Models.Health;
using Refit;

namespace Lykke.Service.HelpCenter.Client
{
    /// <summary>
    /// HelpCenter client API interface.
    /// </summary>
    [PublicAPI]
    public interface IHelpCenterApi
    {
        /// <summary>
        /// Determines whether the help center service is alive.
        /// </summary>
        [Get("/api/IsAlive")]
        Task<IsAliveModel> GetIsAliveDetails();
    }

    /// <summary>
    /// Extension methods for <see cref="IHelpCenterApi"/>.
    /// </summary>
    [PublicAPI]
    public static class HftApiExtensions
    {
        /// <summary>
        /// Determines whether this help center service is alive.
        /// </summary>
        /// <param name="api">The help center API client.</param>
        /// <returns>[true] when alive, otherwise [false]</returns>
        public static async Task<bool> IsAlive(this IHelpCenterApi api)
        {
            try
            {
                var isAlive = await api.GetIsAliveDetails();
                return isAlive != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
