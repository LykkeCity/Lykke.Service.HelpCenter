using System.Net;
using System.Threading.Tasks;
using Lykke.Service.HelpCenter.Client.Models.Clients;
using Lykke.Service.HelpCenter.Client.Models.Common;
using Lykke.Service.HelpCenter.Core.Domain.Clients;
using Lykke.Service.HelpCenter.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.HelpCenter.Controllers
{
    /// <summary>
    /// Support clients requests controller.
    /// </summary>
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public sealed class SupportClientsController : Controller
    {
        private readonly ISupportClientsService _supportClientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportClientsController"/> class.
        /// </summary>
        /// <param name="supportClientService">The support client service.</param>
        public SupportClientsController(ISupportClientsService supportClientService)
        {
            _supportClientService = supportClientService;
        }

        /// <summary>
        /// Finds the support client details.
        /// </summary>
        /// <param name="id">The client id.</param>
        /// <response code="200">The support client</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Lykke client could not be found</response>
        [ProducesResponseType(typeof(SupportClientModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> FindUser(string id)
        {
            var user = await _supportClientService.FindClientAsync(id);

            if (user == null)
            {
                return NotFound("No support client with given id found.");
            }

            var model = ToSupportClientModel(user);
            return Ok(model);
        }

        /// <summary>
        /// Synchronizes the lykke and support client details.
        /// </summary>
        /// <param name="id">The client id.</param>
        /// <param name="name">The name of the client, when empty name will be extracted from the registered email address.</param>
        /// <response code="200">The synced client</response>
        /// <response code="400">One or more parameters are invalid</response>
        /// <response code="404">Lykke client could not be found</response>
        [ProducesResponseType(typeof(SupportClientModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("{id}")]
        public async Task<IActionResult> SaveUser(string id, string name)
        {
            var user = await _supportClientService.SynchronizeUser(id, name);

            if (user == null)
            {
                return NotFound("No lykke client with given id found.");
            }

            var model = ToSupportClientModel(user);
            return Ok(model);
        }

        /// <summary>
        /// Deletes the registered support client.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <response code="200">Client was deleted</response>
        /// <response code="422">Client could not be deleted</response>
        /// <response code="404">Lykke client could not be found</response>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.UnprocessableEntity)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _supportClientService.DeleteUser(id);

            return response.ToActionResult();
        }

        private static SupportClientModel ToSupportClientModel(ClientModel user)
        {
            return new SupportClientModel
            {
                ClientId = user.ClientId,
                SupportId = user.ZenDeskUserId,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
