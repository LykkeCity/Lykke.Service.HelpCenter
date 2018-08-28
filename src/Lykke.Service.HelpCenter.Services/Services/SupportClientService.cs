using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.ClientAccount.Client;
using Lykke.Service.HelpCenter.Core.Domain;
using Lykke.Service.HelpCenter.Core.Domain.Clients;
using Lykke.Service.HelpCenter.Core.Repositories;
using Lykke.Service.HelpCenter.Core.Services;
using Lykke.Service.HelpCenter.Services.ZenDesk;
using Lykke.Service.HelpCenter.Services.ZenDesk.Users;

namespace Lykke.Service.HelpCenter.Services.Services
{
    public class SupportClientsService : ISupportClientsService
    {
        private static readonly char[] ReplaceChars = { '.', '_', '-' };
        private static readonly Regex StringSplitter = new Regex(@"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))");

        private readonly IClientAccountClient _clientAccounts;
        private readonly ISupportClientRepository _repository;
        private readonly IUsersApi _users;
        private readonly ILog _log;

        public SupportClientsService(IClientAccountClient clientAccounts, ILogFactory logFactory, ISupportClientRepository repository, IUsersApi users)
        {
            _clientAccounts = clientAccounts;
            _repository = repository;
            _users = users;
            _log = logFactory.CreateLog(this);
        }

        public async Task<ClientModel> FindClientAsync(string clientId, string name)
        {
            var model = await FindClientAsync(clientId);
            if (model != null)
            {
                return model;
            }

            return await SynchronizeUser(clientId, name);
        }

        public async Task<ClientModel> FindClientAsync(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                return default;

            var cleanedId = clientId.Trim();

            return await _repository.GetAsync(cleanedId);
        }

        public async Task<ClientModel> SynchronizeUser(string clientId, string name)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            var cleanedId = clientId.Trim();

            try
            {
                var client = await _clientAccounts.GetByIdAsync(cleanedId);
                if (client == null)
                {
                    return null;
                }

                name = GetName(name, client.Email) ?? cleanedId;

                // Step 1. Search by Lykke client id
                var query = await _users.SearchUsers($"external_id:{client.Id}");
                var user = query.Users.FirstOrDefault();

                // Step 2. Search by email address
                if (user == null)
                {
                    query = await _users.SearchUsers($"email:{client.Email}");
                    user = query.Users.FirstOrDefault();
                }

                // Nothering found create new user
                if (user == null)
                {
                    user = new UserModel();
                }

                // User found synchronize data if needed
                if (NeedsUpdate(user, client, name))
                {
                    var result = await _users.SaveUser(new SaveUserRequest { User = user });
                    user = result.User ?? user;
                }

                var model = new ClientModel
                {
                    Email = user.Email,
                    ClientId = user.ClientId,
                    Name = user.Name,
                    ZenDeskUserId = user.ZenDeskId
                };

                await _repository.AddAsync(model).ConfigureAwait(false);

                return model;
            }
            catch (Exception ex)
            {
                _log.Warning("Client could not be retrieved.", ex);
                return null;
            }
        }

        public async Task<ResponseModel> DeleteUser(string clientId)
        {
            var client = await FindClientAsync(clientId);

            if (client == null)
            {
                return new ResponseModel { StatusCode = HttpStatusCode.NotFound };
            }

            var response = await _users.TryExecute(_log, x => x.DeleteUser(client.ZenDeskUserId));

            if (response.Success)
            {
                await _repository.DeleteAsync(clientId).ConfigureAwait(false);
            }

            return response;
        }

        private static bool NeedsUpdate(UserModel user, ClientAccount.Client.Models.ClientModel client, string name)
        {
            if (user.Verified
                && user.Name == name
                && user.ClientId == client.Id
                && user.Email == client.Email)
                return false;

            user.Verified = true;
            user.Email = client.Email;
            user.ClientId = client.Id;
            user.Name = name;

            return true;
        }

        public static string GetName(string name, string email)
        {
            if (!string.IsNullOrWhiteSpace(name))
                return name;

            if (string.IsNullOrWhiteSpace(email))
                return default;

            var extracted = email.Split('@').FirstOrDefault();
            if (string.IsNullOrWhiteSpace(extracted))
                return null;

            extracted = ReplaceChars.Aggregate(extracted, (current, c) => current.Replace(c, ' '));

            extracted = StringSplitter.Replace(extracted, " $0");
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(extracted.ToLower());
        }
    }
}
