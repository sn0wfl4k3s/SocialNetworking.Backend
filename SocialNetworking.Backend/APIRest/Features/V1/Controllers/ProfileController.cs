using APIRest.Utils;
using Core.Service;
using Core.Service.Requests;
using CrossCutting.Authentication;
using CrossCutting.Constants;
using Domain.ViewModels.Other;
using Domain.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace APIRest.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize(Roles = Roles.User)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfileController : ProcessController<ProfileController>
    {
        public ProfileController(IMediator mediator, ILogger<ProfileController> logger, IAuthenticationService authentication) : base(mediator, logger, authentication)
        {
        }

        [HttpGet]
        [Route("{usernameOrId}")]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetProfile(string usernameOrId)
            => await IdentifyUserAndProcess(new ObterRequest<UserResponse> { Parameter = usernameOrId });


        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchProfiles([FromQuery] SearchProfileRequest Search)
            => await IdentifyUserAndProcess(new ProcurarRequest<SearchProfileRequest, IEnumerable<UserResponse>>(Search));

    }
}
