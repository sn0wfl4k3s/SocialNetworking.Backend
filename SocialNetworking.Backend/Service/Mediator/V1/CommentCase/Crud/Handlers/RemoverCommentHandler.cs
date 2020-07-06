using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using CrossCutting.Exceptions;
using Domain.Entity;
using Domain.ViewModels.Comment;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.CommentCase.Crud.Handlers
{
    public class RemoverCommentHandler : IRemoverHandler<CommentRequest, CommentResponse>
    {
        private readonly IEntityRepository<Comment> _repository;
        private readonly IMapper _mapper;

        public RemoverCommentHandler(IEntityRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<CommentResponse>> Handle(RemoverRequest<CommentRequest, CommentResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<CommentResponse>();

            try
            {
                var temAutorizacao = _repository.ObterQueryEntidade()
                    .Any(p => p.Id == request.Id && p.User.Username == request.User.Username && p.Id == request.Entidade.Id);

                if (!temAutorizacao)
                {
                    throw new ActionNotPermittedException();
                }

                var commentRemovido = await _repository.RemoverEntidadeAsync(request.Id);

                var response = _mapper.Map<Comment, CommentResponse>(commentRemovido);

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
