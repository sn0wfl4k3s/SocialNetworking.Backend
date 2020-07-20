using APIRest.Utils;
using Core.Service;
using CrossCutting.Authentication;
using CrossCutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Mediator.V1.FriendshipCase.AcceptFriend;
using Service.Mediator.V1.FriendshipCase.DenyFriend;
using Service.Mediator.V1.FriendshipCase.ListAllAccepted;
using Service.Mediator.V1.FriendshipCase.ListAllRequest;
using Service.Mediator.V1.FriendshipCase.RequestFor;
using System.Net.Mime;
using System.Threading.Tasks;

namespace APIRest.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize(Roles = Roles.User)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FriendController : ProcessController<FriendController>
    {
        public FriendController(IMediator mediator, ILogger<FriendController> logger, IAuthenticationService authentication) : base(mediator, logger, authentication)
        {
        }

        /// <summary>
        /// List all friends already accepted from a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListAllAccepted/{username}")]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ListAcceptedFriends(string username)
            => await IdentifyUserAndProcess(new ListAllAcceptedQuery(username));

        /// <summary>
        /// List all request friendship of user who did the request.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListAllRequest/{username}")]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ListAllRequestFriends(string username)
            => await IdentifyUserAndProcess(new ListAllRequestQuery(username));

        /// <summary>
        /// Create a request of friendship for a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RequestFor/{username}")]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RequestFor(string username)
            => await IdentifyUserAndProcess(new RequestForCommand(username));

        /// <summary>
        /// Accept a friendship request.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Accept/{username}")]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AcceptFriend(string username)
            => await IdentifyUserAndProcess(new AcceptFriendCommand(username));

        /// <summary>
        /// Deny a friendship request.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deny/{username}")]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DenyFriend(string username)
            => await IdentifyUserAndProcess(new DenyFriendCommand(username));
    }
}
