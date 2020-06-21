using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using Domain.Entity;
using Domain.ViewModels.Post;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.PostCase.Crud
{
    public class RemoverPostHandler : IRemoverHandler<PostRequest, PostResponse>
    {
        private readonly IEntityRepository<Post> _repository;
        private readonly IMapper _mapper;

        public RemoverPostHandler(IEntityRepository<Post> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PostResponse>> Handle(RemoverRequest<PostRequest, PostResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<PostResponse>();

            try
            {
                var postRemovido = await _repository.RemoverEntidadeAsync(request.Id);

                var response = _mapper.Map<Post, PostResponse>(postRemovido);

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
