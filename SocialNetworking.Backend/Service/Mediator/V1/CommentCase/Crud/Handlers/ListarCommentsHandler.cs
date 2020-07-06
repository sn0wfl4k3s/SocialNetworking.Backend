using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using Domain.Entity;
using Domain.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.CommentCase.Crud.Handlers
{
    public class ListarCommentsHandler : IListarHandler<IEnumerable<CommentResponse>>
    {
        private readonly IEntityRepository<Comment> _repository;
        private readonly IMapper _mapper;

        public ListarCommentsHandler(IEntityRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CommentResponse>>> Handle(ListarRequest<IEnumerable<CommentResponse>> request, CancellationToken cancellationToken)
        {
            var error = new Response<IEnumerable<CommentResponse>>();

            try
            {
                var username = string.IsNullOrEmpty(request.Parameter) ? request.User.Username : request.Parameter;

                var lista = _repository.ObterQueryEntidade().Where(c => c.User.Username == username).ToList();

                var response = _mapper.Map<IEnumerable<CommentResponse>>(lista);

                return await Task.FromResult(new Response<IEnumerable<CommentResponse>>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
