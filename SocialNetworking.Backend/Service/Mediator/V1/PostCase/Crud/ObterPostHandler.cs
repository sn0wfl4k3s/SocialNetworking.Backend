using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using Domain.Entity;
using Domain.ViewModels.Post;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.PostCase.Crud
{
    public class ObterPostHandler : IObterHandler<PostResponse>
    {
        private readonly IEntityRepository<Post> _repository;
        private readonly IMapper _mapper;

        public ObterPostHandler(IEntityRepository<Post> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PostResponse>> Handle(ObterRequest<PostResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<PostResponse>();

            try
            {
                var post = _repository.ObterQueryEntidade().AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.Id);

                var response = _mapper.Map<PostResponse>(await post);

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
