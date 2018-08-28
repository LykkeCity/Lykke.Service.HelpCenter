using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Client.Models.Requests;
using Lykke.Service.HelpCenter.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.HelpCenter.Controllers
{
    /// <summary>
    /// HelpCenter requests controller.
    /// </summary>
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : Controller
    {
        private readonly ISupportClientsService _clientAccounts;
        private readonly IRequestsService _requests;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestsController"/> class.
        /// </summary>
        public RequestsController(ISupportClientsService clientAccounts, IRequestsService requests)
        {
            _clientAccounts = clientAccounts;
            _requests = requests;
        }

        /// <summary>
        /// Places a new request.
        /// </summary>
        /// <param name="model">The request to place</param>
        /// <response code="200">The ticket id</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Client could not be found</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PlaceRequest([FromBody] PlaceRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Subject))
            {
                return BadRequest("Subject is mandatory");
            }

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                return BadRequest("Description is mandatory");
            }

            var client = await _clientAccounts.FindClientAsync(model.ClientId, model.ClientName);
            if (client == null)
            {
                return NotFound("Client could not be found.");
            }

            var result = await _requests.PlaceRequest(
                client,
                model.Subject,
                model.Description,
                GetRequestType(model.Type));

            return Ok(result);
        }

        /// <summary>
        /// Gets an existing request by id.
        /// </summary>
        /// <param name="id">The ticket id</param>
        /// <response code="200">The request details</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Request could not be found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RequestModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRequest(string id)
        {
            var result = await _requests.GetRequest(id);
            if (result == null)
            {
                return NotFound("Request could not be found");
            }

            return Ok(ToRequestModel(result));
        }

        /// <summary>
        /// Gets all client requests.
        /// </summary>
        /// <param name="clientId">The client id</param>
        /// <response code="200">The request details</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Client could not be found</response>
        [HttpGet("clients/{clientId}")]
        [ProducesResponseType(typeof(IEnumerable<RequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRequests(string clientId)
        {
            var client = await _clientAccounts.FindClientAsync(clientId);
            if (client == null)
            {
                return NotFound("Client could not be found.");
            }

            var result = await _requests.GetRequests(client);
            return Ok(result.Select(ToRequestModel));
        }

        /// <summary>
        /// Adds a comment to an existing request.
        /// </summary>
        /// <param name="id">The request id</param>
        /// <param name="model">The comment to add</param>
        /// <response code="200">When comment was successfully added</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Request or client could not be found</response>
        [HttpPost("{id}/comments")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddComment(string id, [FromBody] AddCommentModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Comment))
            {
                return BadRequest("Comment is mandatory");
            }

            var client = await _clientAccounts.FindClientAsync(model.ClientId);
            if (client == null)
            {
                return NotFound("Client could not be found.");
            }

            var result = await _requests.UpdateRequest(client, id, model.Comment);

            if (result == null)
            {
                return NotFound("Request could not be found");
            }

            return Ok();
        }

        /// <summary>
        /// Gets the request comments
        /// </summary>
        /// <param name="id">The ticket id</param>
        /// <response code="200">The request comments</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Request could not be found</response>
        [HttpGet("{id}/comments")]
        [ProducesResponseType(typeof(IEnumerable<CommentModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetComments(string id)
        {
            var result = await _requests.GetComments(id);
            if (result == null)
            {
                return NotFound("Request could not be found");
            }

            return Ok(result.Select(x => new CommentModel
            {
                Text = x.Text,
                Author = x.Author
            }));
        }

        private static Core.Domain.Requests.RequestType GetRequestType(RequestType? modelType)
        {
            if (modelType == null)
            {
                return Core.Domain.Requests.RequestType.None;
            }

            switch (modelType.Value)
            {
                case RequestType.Question:
                    return Core.Domain.Requests.RequestType.Question;
                case RequestType.Incident:
                    return Core.Domain.Requests.RequestType.Incident;
                case RequestType.Problem:
                    return Core.Domain.Requests.RequestType.Problem;
                case RequestType.Task:
                    return Core.Domain.Requests.RequestType.Task;
                default:
                    return Core.Domain.Requests.RequestType.None;
            }
        }

        private RequestModel ToRequestModel(Core.Domain.Requests.RequestModel model)
        {
            return new RequestModel
            {
                Id = model.Id,
                Description = model.Description,
                Subject = model.Subject,
                Priority = ToPriority(model.Priority),
                Status = ToStatus(model.Status),
                Type = ToType(model.Type)
            };
        }

        private static RequestPriority? ToPriority(Core.Domain.Requests.RequestPriority? priority)
        {
            switch (priority)
            {
                case Core.Domain.Requests.RequestPriority.Low:
                    return RequestPriority.Low;
                case Core.Domain.Requests.RequestPriority.Normal:
                    return RequestPriority.Normal;
                case Core.Domain.Requests.RequestPriority.High:
                    return RequestPriority.High;
                case Core.Domain.Requests.RequestPriority.Urgent:
                    return RequestPriority.Urgent;
                default:
                    return null;
            }
        }
        private static RequestStatus ToStatus(Core.Domain.Requests.RequestStatus status)
        {
            switch (status)
            {
                case Core.Domain.Requests.RequestStatus.New:
                    return RequestStatus.New;
                case Core.Domain.Requests.RequestStatus.Open:
                    return RequestStatus.Open;
                case Core.Domain.Requests.RequestStatus.Pending:
                    return RequestStatus.Pending;
                case Core.Domain.Requests.RequestStatus.Hold:
                    return RequestStatus.Hold;
                case Core.Domain.Requests.RequestStatus.Solved:
                    return RequestStatus.Solved;
                case Core.Domain.Requests.RequestStatus.Closed:
                    return RequestStatus.Closed;
                default:
                    return RequestStatus.Open;
            }
        }

        private static RequestType? ToType(Core.Domain.Requests.RequestType? type)
        {
            switch (type)
            {
                case Core.Domain.Requests.RequestType.Question:
                    return RequestType.Question;
                case Core.Domain.Requests.RequestType.Incident:
                    return RequestType.Incident;
                case Core.Domain.Requests.RequestType.Problem:
                    return RequestType.Problem;
                case Core.Domain.Requests.RequestType.Task:
                    return RequestType.Task;
                default:
                    return null;
            }
        }
    }
}
