using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using CrossCutting.Exceptions;
using Domain.Entity;
using Domain.ViewModels.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.CommentCase.Crud
{
    public class AtualizarCommentHandler : IAtualizarHandler<CommentRequest, CommentResponse>
    {
        private readonly IEntityRepository<Comment> _repository;
        private readonly IMapper _mapper;

        public AtualizarCommentHandler(IEntityRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<CommentResponse>> Handle(AtualizarRequest<CommentRequest, CommentResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<CommentResponse>();

            try
            {
                var temAutorizacao = await _repository.ObterQueryEntidade()
                    .AnyAsync(p => p.Id == request.Id && p.User.Username == request.User.Username);

                if (!temAutorizacao)
                {
                    throw new ActionNotPermittedException();
                }

                var comment = _mapper.Map<CommentRequest, Comment>(request.Entidade);

                comment.User = request.User;

                var commentAtualizado = await _repository.AtualizarEntidadeAsync(comment);

                var response = _mapper.Map<Comment, CommentResponse>(commentAtualizado);

                return await Task.FromResult(new Response<CommentResponse>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
