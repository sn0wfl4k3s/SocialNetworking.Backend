using AutoMapper;
using Core.Domain;
using Core.Service;
using CrossCutting.Account;
using CrossCutting.Security;
using CrossCutting.Time;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Service.Mediator.V1.AccountCase.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<RegisterUserVM>>
    {
        private readonly IEntityRepository<User> _repository;
        private readonly IAccountService _accountService;
        private readonly ICryptService _cryptService;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IEntityRepository<User> repository, ICryptService cryptService, IMapper mapper, IAccountService accountService)
        {
            _repository = repository;
            _cryptService = cryptService;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<Response<RegisterUserVM>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<RegisterUserVM>();

            try
            {
                if (!_repository.ObterQueryEntidade().AsNoTracking().Any(u => u.Email == request.Email))
                {
                    var user = _mapper.Map<RegisterUserCommand, User>(request);

                    user.Created = DateTimeUtil.BrazilDateTimeNow();

                    user.Username = _accountService.GenerateUsername(user.Name, user.LastName);

                    user.Password = _cryptService.CreateHash(user.Password);

                    var userCriado = await _repository.CriarEntidadeAsync(user);

                    var resultado = _mapper.Map<User, RegisterUserVM>(userCriado);

                    return await Task.FromResult(new Response<RegisterUserVM>(resultado));
                }
                else
                {
                    response.AddError(nameof(request.Email), "E-mail já existente.");
                }
            }
            catch (Exception e)
            {
                response.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(response);
        }
    }
}
