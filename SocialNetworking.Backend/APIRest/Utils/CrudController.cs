using Core.Service;
using Core.Service.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Utils
{
    public abstract class CrudController<TController, TRequest, TResponse> : ProcessController<TController>
        where TController : ControllerBase 
    {
        public CrudController(IMediator mediator, ILogger<TController> logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Lista todos as entidades ou todas de um determinado username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] string username)
            => await Process(new ListarRequest<IEnumerable<TResponse>> { Parametro1 = username });


        /// <summary>
        /// Obtém uma determinada entidade através de seu id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(ulong Id)
            => await Process(new ObterRequest<TResponse> { Id = Id });


        /// <summary>
        /// Insere uma nova entidade no sistema.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromHeader] string authorization, TRequest post)
            => await Process(new CriarRequest<TRequest, TResponse>(authorization, post));


        /// <summary>
        /// Atualiza uma entidade no sistema.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromHeader] string authorization, TRequest post)
            => await Process(new AtualizarRequest<TRequest, TResponse>(authorization, post));


        /// <summary>
        /// Remove uma entidade do sistema.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromHeader] string authorization, ulong Id)
            => await Process(new RemoverRequest<TRequest, TResponse>(authorization, Id));
    }
}
