using Core.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebAPI.Utils
{
    public abstract class ProcessController<C> : ControllerBase where C : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<C> _logger;

        public ProcessController(IMediator mediator, ILogger<C> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
