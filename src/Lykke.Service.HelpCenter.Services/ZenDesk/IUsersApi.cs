using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Services.ZenDesk.Users;
using Refit;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    public interface IUsersApi
    {
        [Get("/api/v2/users/search.json")]
        Task<UsersResponseModel> SearchUsers(string query);

        [Post("/api/v2/users/create_or_update.json")]
        Task<SaveUserResponse> SaveUser([Body] SaveUserRequest user);

        [Delete("/api/v2/users/{id}.json")]
        Task DeleteUser(string id);
    }
}
