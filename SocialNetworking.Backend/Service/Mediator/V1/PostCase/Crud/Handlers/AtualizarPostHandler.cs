using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using CrossCutting.Exceptions;
using Domain.Entity;
using Domain.ViewModels.Post;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.PostCase.Crud.Handlers
{
    public class AtualizarPostHandler : IAtualizarHandler<PostRequest, PostResponse>
    {
        private readonly IEntityRepository<Post> _repository;
        private readonly IMapper _mapper;

        public AtualizarPostHandler(IEntityRepository<Post> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PostResponse>> Handle(AtualizarRequest<PostRequest, PostResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<PostResponse>();

            try
            {
                var temAutorizacao = await _repository.ObterQueryEntidade()
                    .AnyAsync(p => p.Id == request.Id && p.User.Username == request.User.Username);

                if (!temAutorizacao)
                {
                    throw new ActionNotPermittedException();
                }

                var post = _mapper.Map<PostRequest, Post>(request.Entidade);

                post.User = request.User;

                var postCriado = await _repository.AtualizarEntidadeAsync(post);

                var response = _mapper.Map<Post, PostResponse>(postCriado);

                return await Task.FromResult(new Response<PostResponse>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
