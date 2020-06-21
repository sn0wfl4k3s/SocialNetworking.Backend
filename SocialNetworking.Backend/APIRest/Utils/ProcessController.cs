using Core.Service;
using Core.Service.Requests.Core;
using CrossCutting.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Utils
{
    public abstract class ProcessController<C> : ControllerBase where C : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<C> _logger;
        private readonly IAuthenticationService _authentication;

        protected ProcessController(IMediator mediator, ILogger<C> logger, IAuthenticationService authentication)
        {
            _mediator = mediator;
            _logger = logger;
            _authentication = authentication;
        }

        protected virtual async Task<IActionResult> IdentifyUserAndProcess<T>(IRequestUser<T> command)
        {
            var token = Request.Headers["Authorization"].First().Split(" ").Last();

            command.User = _authentication.GetUserByToken(token);

            var task = _mediator.Send(command);

            _logger.LogInformation(JsonConvert.SerializeObject(command));

            var response = await task;

            if (response.HasError())
            {
                _logger.LogError(JsonConvert.SerializeObject(response.Errors));

                return BadRequest(response);
            }

            return Ok(response);
        }

        protected virtual async Task<IActionResult> Process<T>(IRequest<Response<T>> command)
        {
            var task = _mediator.Send(command);

            _logger.LogInformation(JsonConvert.SerializeObject(command));

            var response = await task;

            if (response.HasError())
            {
                _logger.LogError(JsonConvert.SerializeObject(response.Errors));

                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
