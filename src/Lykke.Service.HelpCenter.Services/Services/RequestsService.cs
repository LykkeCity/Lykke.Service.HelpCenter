using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.HelpCenter.Core.Domain.Clients;
using Lykke.Service.HelpCenter.Core.Domain.Requests;
using Lykke.Service.HelpCenter.Core.Services;
using Lykke.Service.HelpCenter.Services.ZenDesk;
using Lykke.Service.HelpCenter.Services.ZenDesk.Requests;
using Refit;

namespace Lykke.Service.HelpCenter.Services.Services
{
    internal class RequestsService : IRequestsService
    {
        private readonly IRequestsApi _requests;
        private readonly ILog _log;

        public RequestsService(IRequestsApi requests, ILogFactory logFactory)
        {
            _requests = requests;
            _log = logFactory.CreateLog(this);
        }

        public async Task<string> PlaceRequest(ClientModel client, string subject, string description, RequestType type)
        {
            var request = new CreateRequestModel
            {
                Request = new CreateRequestModel.Details
                {
                    Subject = subject,
                    Type = type,
                    Requester = new RequesterModel { Email = client.Email },
                    Comment = new CommentModel { Body = description }
                }
            };

            try
            {
                var result = await _requests.PlaceRequest(request);
                return result.Request.Id;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public async Task<RequestModel> GetRequest(string id)
        {
            try
            {
                var result = await _requests.GetRequest(id);
                return result.Request;
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                _log.Error(ex);
                throw;
            }
        }

        public async Task<IEnumerable<RequestModel>> GetRequests(ClientModel client)
        {
            try
            {
                var result = await _requests.GetRequests(client.Email);
                return result.Requests;
            }
            catch (ApiException ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public async Task<RequestModel> UpdateRequest(string id, string comment)
        {
            var request = new UpdateRequestModel
            {
                Request = new UpdateRequestModel.Details
                {
                    Comment = new CommentModel { Body = comment }
                }
            };
            
            try
            {
                var result = await _requests.UpdateRequest(id, request);
                return result.Request;
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                _log.Error(ex);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetComments(string id)
        {
            try
            {
                var result = await _requests.GetComments(id);
                return result.Comments.Select(x => x.Body);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                _log.Error(ex);
                throw;
            }
        }
    }
}
