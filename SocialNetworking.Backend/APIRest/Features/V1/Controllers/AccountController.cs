using APIRest.Utils;
using CrossCutting.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Mediator.V1.AccountCase.Login;
using Service.Mediator.V1.AccountCase.Register;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebAPI.Features.V1.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ProcessController<AccountController>
    {
        public AccountController(IMediator mediator, ILogger<AccountController> logger, IAuthenticationService authentication) : base(mediator, logger, authentication)
        {
        }


        /// <summary>
        /// Sign in a user registred.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginUserQuery query)
            => await Process(query);


        /// <summary>
        /// Sign up a new user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
            => await Process(command);
    }
}
