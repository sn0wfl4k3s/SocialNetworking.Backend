using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using CrossCutting.File;
using CrossCutting.Time;
using Domain.Entity;
using Domain.ViewModels.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.CommentCase.Crud.Handlers
{
    public class CriarCommentHandler : ICriarHandler<CommentRequest, CommentResponse>
    {
        private readonly IEntityRepository<Comment> _repository;
        private readonly IEntityRepository<Post> _repositoryP;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CriarCommentHandler(IEntityRepository<Comment> repository, IEntityRepository<Post> repositoryP, IFileService fileService, IMapper mapper)
        {
            _repository = repository;
            _repositoryP = repositoryP;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Response<CommentResponse>> Handle(CriarRequest<CommentRequest, CommentResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<CommentResponse>();

            try
            {
                var taskFiles = _fileService.SaveFilesAsync(request.Entidade.Files, request.User);

                var taskPost = _repositoryP.ObterQueryEntidade().FirstOrDefaultAsync(p => p.Id == request.Entidade.PostId);

                var comment = _mapper.Map<CommentRequest, Comment>(request.Entidade);

                comment.User = request.User;

                comment.Created = DateTimeUtil.BrazilDateTimeNow();

                comment.Post = await taskPost;

                comment.FileReferences = await taskFiles;

                var commentCriado = await _repository.CriarEntidadeAsync(comment);

                var response = _mapper.Map<Comment, CommentResponse>(commentCriado);

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
