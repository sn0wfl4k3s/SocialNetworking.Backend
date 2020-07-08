using APIRest.Utils;
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
        /// List all posts for a user. If the username is not passed, the posts of the user who made 
        /// the request will be considered.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string username)
            => await IdentifyUserAndProcess(new ListarRequest<IEnumerable<PostResponse>> { Parameter = username });


        /// <summary>
        /// Get a post by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(ulong id)
            => await IdentifyUserAndProcess(new ObterRequest<PostResponse> { Id = id });


        /// <summary>
        /// Create a new post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromForm] PostRequest post)
            => await IdentifyUserAndProcess(new CriarRequest<PostRequest, PostResponse>(post));


        /// <summary>
        /// Update a post already created.
        /// </summary>
        /// <param name="post"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] PostRequest post, ulong id)
            => await IdentifyUserAndProcess(new AtualizarRequest<PostRequest, PostResponse>(post, id));


        /// <summary>
        /// Delete a post already created.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(ulong id)
            => await IdentifyUserAndProcess(new RemoverRequest<PostRequest, PostResponse>(id));

    }
}
