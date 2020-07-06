using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using CrossCutting.File;
using Domain.Entity;
using Domain.ViewModels.Post;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.PostCase.Crud.Handlers
{
    public class CriarPostHandler : ICriarHandler<PostRequest, PostResponse>
    {
        private readonly IEntityRepository<Post> _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CriarPostHandler(IEntityRepository<Post> repository, IFileService fileService, IMapper mapper)
        {
            _repository = repository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Response<PostResponse>> Handle(CriarRequest<PostRequest, PostResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<PostResponse>();

            try
            {
                var taskFiles = _fileService.SaveFilesAsync(request.Entidade.Files, request.User);

                var post = _mapper.Map<PostRequest, Post>(request.Entidade);

                post.User = request.User;

                post.FileReferences = await taskFiles;

                post.Created = DateTime.Now;

                var postCriado = await _repository.CriarEntidadeAsync(post);

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
