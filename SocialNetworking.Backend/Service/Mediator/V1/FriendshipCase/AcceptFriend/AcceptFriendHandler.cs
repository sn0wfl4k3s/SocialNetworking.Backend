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

namespace Service.Mediator.V1.FriendshipCase.AcceptFriend
{
    public class AcceptFriendHandler : IRequestUserHandler<AcceptFriendCommand, FriendshipResponse>
    {
        private readonly IEntityRepository<Friendship> _repositoryF;
        private readonly IEntityRepository<User> _repositoryU;
        private readonly IMapper _mapper;

        public AcceptFriendHandler(IEntityRepository<Friendship> repositoryF, IEntityRepository<User> repositoryU, IMapper mapper)
        {
            _repositoryF = repositoryF;
            _repositoryU = repositoryU;
            _mapper = mapper;
        }

        public async Task<Response<FriendshipResponse>> Handle(AcceptFriendCommand request, CancellationToken cancellationToken)
        {
            var error = new Response<FriendshipResponse>();

            try
            {
                var userWhoDoTheRequest = _repositoryU.ObterQueryEntidade()
                    .FirstOrDefault(u => u.Username == request.Username);

                var friendship = _repositoryF.ObterQueryEntidade()
                    .FirstOrDefault(f => f.To == request.User && f.From == userWhoDoTheRequest);

                friendship.ConfirmationDate = DateTime.Now;

                var friendshipUpdated = await _repositoryF.AtualizarEntidadeAsync(friendship);

                var response = _mapper.Map<Friendship, FriendshipResponse>(friendshipUpdated);

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
