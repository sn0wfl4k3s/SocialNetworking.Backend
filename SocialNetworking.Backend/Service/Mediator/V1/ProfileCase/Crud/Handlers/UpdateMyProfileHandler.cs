using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using CrossCutting.Exceptions;
using Domain.Entity;
using Domain.ViewModels.User;
using InfraData;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.ProfileCase.Crud.Handlers
{
    public class UpdateMyProfileHandler : IAtualizarHandler<UserRequest, UserResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IEntityRepository<User> _repository;
        private readonly IMapper _mapper;

        public UpdateMyProfileHandler(ApplicationDbContext context, IEntityRepository<User> repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<UserResponse>> Handle(AtualizarRequest<UserRequest, UserResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<UserResponse>();

            try
            {
                if (request.User.Id == request.Id && request.Id == request.Entidade.Id)
                {
                    var user = _mapper.Map<UserRequest, User>(request.Entidade);

                    user.Password = request.User.Password;

                    user.Created = request.User.Created;

                    _context.Entry(request.User).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                    var resultado = await _repository.AtualizarEntidadeAsync(user);

                    var response = _mapper.Map<User, UserResponse>(resultado);

                    return await Task.FromResult(new Response<UserResponse>(response));
                }
                else
                {
                    throw new ActionNotPermittedException();
                }
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
