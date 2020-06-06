using AutoMapper;
using Core.Domain;
using Core.Service;
using CrossCutting.Authentication;
using CrossCutting.Security;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.AccountCase.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Response<LoginUserVM>>
    {
        private readonly IEntityRepository<User> _repository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICryptService _cryptService;
        private readonly IMapper _mapper;

        public LoginUserHandler(IEntityRepository<User> repository, IAuthenticationService authenticationService, ICryptService cryptService, IMapper mapper)
        {
            _repository = repository;
            _authenticationService = authenticationService;
            _cryptService = cryptService;
            _mapper = mapper;
        }

        public async Task<Response<LoginUserVM>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<LoginUserVM>();

            try
            {
                var hash = _cryptService.CreateHash(request.Password);

                var user = await _repository
                    .ObterQueryEntidade()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == hash);

                if (user is null)
                {
                    response.AddError("Erro ao efetuar login", "E-mail ou senha inválidos.");

                    return await Task.FromResult(response);
                }

                var authentication = _authenticationService.CreateTokenJWT(user);

                var viewModel = new LoginUserVM
                {
                    Access_token = authentication.Token,
                    Token_type = authentication.Type,
                    Expires_in = authentication.Expires
                };

                return await Task.FromResult(new Response<LoginUserVM>(viewModel));
            }
            catch (Exception e)
            {
                response.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(response);
        }
    }
}
