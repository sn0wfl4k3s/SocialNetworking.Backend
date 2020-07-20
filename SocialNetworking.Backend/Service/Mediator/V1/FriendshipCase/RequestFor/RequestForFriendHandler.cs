using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Core;
using Domain.Entity;
using Domain.ViewModels.Friendship;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.FriendshipCase.RequestFor
{
    public class RequestForFriendHandler : IRequestUserHandler<RequestForCommand, FriendshipResponse>
    {
        private readonly IEntityRepository<Friendship> _repositoryF;
        private readonly IEntityRepository<User> _repositoryU;
        private readonly IMapper _mapper;

        public RequestForFriendHandler(IEntityRepository<Friendship> repositoryF, IEntityRepository<User> repositoryU, IMapper mapper)
        {
            _repositoryF = repositoryF;
            _repositoryU = repositoryU;
            _mapper = mapper;
        }

        public async Task<Response<FriendshipResponse>> Handle(RequestForCommand request, CancellationToken cancellationToken)
        {
            var error = new Response<FriendshipResponse>();

            try
            {
                var userRequested = _repositoryU.ObterQueryEntidade().FirstOrDefault(u => u.Username == request.Username);

                var friendship = new Friendship
                {
                    From = request.User,
                    To = userRequested,
                    RequestDate = DateTime.Now
                };

                var friendshipCriado = await _repositoryF.CriarEntidadeAsync(friendship);

                var response = _mapper.Map<Friendship, FriendshipResponse>(friendshipCriado);

                return await Task.FromResult(new Response<FriendshipResponse>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
