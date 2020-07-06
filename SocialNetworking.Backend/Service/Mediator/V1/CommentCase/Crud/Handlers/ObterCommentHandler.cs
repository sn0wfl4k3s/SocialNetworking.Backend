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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.CommentCase.Crud.Handlers
{
    public class ObterCommentHandler : IObterHandler<CommentResponse>
    {
        private readonly IEntityRepository<Comment> _repository;
        private readonly IMapper _mapper;

        public ObterCommentHandler(IEntityRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<CommentResponse>> Handle(ObterRequest<CommentResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<CommentResponse>();

            try
            {
                var comment = _repository.ObterQueryEntidade().AsNoTracking().FirstOrDefault(u => u.Id == request.Id);

                if (comment is null)
                {
                    throw new EntityNotFoundException<Comment>();
                }

                var response = _mapper.Map<Comment, CommentResponse>(comment);

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
