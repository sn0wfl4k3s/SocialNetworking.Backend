using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using Domain.Entity;
using Domain.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.ProfileCase.Crud.Handlers
{
    public class ObterProfileHandler : IObterHandler<UserResponse>
    {
        private readonly IEntityRepository<User> _repository;
        private readonly IMapper _mapper;

        public ObterProfileHandler(IEntityRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<UserResponse>> Handle(ObterRequest<UserResponse> request, CancellationToken cancellationToken)
        {
            var error = new Response<UserResponse>();

            try
            {
                var user = await _repository
                    .ObterQueryEntidade()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Username == request.Parameter);

                var response = _mapper.Map<User, UserResponse>(user);

                return await Task.FromResult(new Response<UserResponse>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
