using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using Domain.Entity;
using Domain.ViewModels.Post;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.PostCase.Crud
{
    public class ListarPostsHandler : IListarHandler<IEnumerable<PostResponse>>
    {
        private readonly IEntityRepository<Post> _repository;
        private readonly IMapper _mapper;

        public ListarPostsHandler(IEntityRepository<Post> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<PostResponse>>> Handle(ListarRequest<IEnumerable<PostResponse>> request, CancellationToken cancellationToken)
        {
            var error = new Response<IEnumerable<PostResponse>>();

            try
            {
                var query = _repository.ObterQueryEntidade().AsNoTracking();

                if (!string.IsNullOrEmpty(request.Parameter))
                {
                    query = query.Where(p => p.User.Username == request.Parameter);
                }

                var response = _mapper.Map<IEnumerable<PostResponse>>(query.ToList());

                return await Task.FromResult(new Response<IEnumerable<PostResponse>>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
