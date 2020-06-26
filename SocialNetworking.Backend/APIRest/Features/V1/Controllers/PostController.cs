using Core.Service;
using Core.Service.Requests;
using CrossCutting.Authentication;
using CrossCutting.Constants;
using Domain.ViewModels.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using APIRest.Utils;
using System.Linq;

namespace WebAPI.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize(Roles = Roles.User)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostController : ProcessController<PostController>
    {
        public PostController(IMediator mediator, ILogger<PostController> logger, IAuthenticationService authentication) : base(mediator, logger, authentication)
        {
        }


        /// <summary>
        /// Lista todos os post de um usuário. Caso não seja passado o username será considerado 
        /// os posts do próprio usuário que fez a requisição.
        /// </summary>
        /// <param name="username">Username do usuário</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string username)
            => await IdentifyUserAndProcess(new ListarRequest<IEnumerable<PostResponse>> { Parameter = username });


        /// <summary>
        /// Obtém um post através do ID
        /// </summary>
        /// <param name="id">ID do post</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(ulong id)
            => await IdentifyUserAndProcess(new ObterRequest<PostResponse> { Id = id });


        /// <summary>
        /// Cria um novo post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromForm]PostRequest post)
            => await IdentifyUserAndProcess(new CriarRequest<PostRequest, PostResponse> { Entidade = post });


        /// <summary>
        /// Atualiza um post já criado.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm]PostRequest post)
            => await IdentifyUserAndProcess(new AtualizarRequest<PostRequest, PostResponse> { Entidade = post });


        /// <summary>
        /// Deleta um post já criado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(ulong id)
            => await IdentifyUserAndProcess(new RemoverRequest<PostRequest, PostResponse> { Id = id });

    }
}
