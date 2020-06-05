using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Mediator.V1.AccountCase.Register;
using System.Net.Mime;
using System.Threading.Tasks;
using WebAPI.Utils;

namespace WebAPI.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ProcessController<AccountController>
    {
        public AccountController(IMediator mediator, ILogger<AccountController> logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
            => await Process(command);

    }
}
