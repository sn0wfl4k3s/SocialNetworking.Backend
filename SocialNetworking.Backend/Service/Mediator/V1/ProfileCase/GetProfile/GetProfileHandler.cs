using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Core;
using Domain.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.ProfileCase.GetProfile
{
    public class GetProfileHandler : IRequestUserHandler<GetProfileQuery, GetProfileVM>
    {
        private readonly IEntityRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetProfileHandler(IEntityRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public  Task<Response<GetProfileVM>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
