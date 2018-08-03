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
    [Route("api/[controller]")]
    public class RequestsController : Controller
    {
        private readonly IClientAcountService _clientAccounts;
        private readonly IRequestsService _requests;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestsController"/> class.
        /// </summary>
        public RequestsController(IClientAcountService clientAccounts, IRequestsService requests)
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
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var client = await _clientAccounts.FindClient(model.ClientId);
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
        /// Adds a comment to an existing ticket.
        /// </summary>
        /// <param name="id">The ticket id</param>
        /// <param name="model">The comment to add</param>
        /// <response code="200">When comment was successfully added</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Request could not be found</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddComment(string id, [FromBody] AddCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var result = await _requests.UpdateRequest(id, model.Comment);

            if (result == null)
            {
                return NotFound("Request could not be found");
            }

            return Ok();
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
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var result = await _requests.GetRequest(id);
            if (result == null)
            {
                return NotFound("Request could not be found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets all client requests.
        /// </summary>
        /// <param name="clientId">The client id</param>
        /// <response code="200">The request details</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Client could not be found</response>
        [HttpGet("client/{clientId}")]
        [ProducesResponseType(typeof(IEnumerable<RequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRequests(string clientId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var client = await _clientAccounts.FindClient(clientId);
            if (client == null)
            {
                return NotFound("Client could not be found.");
            }

            var result = await _requests.GetRequests(client);
            return Ok(result);
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
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var result = await _requests.GetComments(id);
            if (result == null)
            {
                return NotFound("Request could not be found");
            }

            return Ok(result.Select(x => new CommentModel { Text = x }));
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
    }
}
