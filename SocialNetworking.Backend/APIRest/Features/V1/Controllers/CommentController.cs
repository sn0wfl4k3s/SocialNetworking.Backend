using APIRest.Utils;
using Core.Service;
using Core.Service.Requests;
using CrossCutting.Authentication;
using CrossCutting.Constants;
using Domain.ViewModels.Comment;
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
    public class CommentController : ProcessController<CommentController>
    {
        public CommentController(IMediator mediator, ILogger<CommentController> logger, IAuthenticationService authentication) : base(mediator, logger, authentication)
        {
        }

        /// <summary>
        /// List all comments for a user. If the username is not passed, the comments of the user who made 
        /// the request will be considered.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string username)
            => await IdentifyUserAndProcess(new ListarRequest<IEnumerable<CommentResponse>> { Parameter = username });


        /// <summary>
        /// Get a comment by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(ulong id)
            => await IdentifyUserAndProcess(new ObterRequest<CommentResponse> { Id = id });


        /// <summary>
        /// Create a new comment.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromForm] CommentRequest comment)
            => await IdentifyUserAndProcess(new CriarRequest<CommentRequest, CommentResponse> { Entidade = comment });


        /// <summary>
        /// Update a comment already created.
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] CommentRequest comment, ulong id)
            => await IdentifyUserAndProcess(new AtualizarRequest<CommentRequest, CommentResponse> { Entidade = comment, Id = id });


        /// <summary>
        /// Delete a comment already created.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(ulong id)
            => await IdentifyUserAndProcess(new RemoverRequest<CommentRequest, CommentResponse> { Id = id });
    }
}
