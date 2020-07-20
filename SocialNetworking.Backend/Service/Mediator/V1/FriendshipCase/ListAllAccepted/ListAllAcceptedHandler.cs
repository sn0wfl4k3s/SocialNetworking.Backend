using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Core;
using Domain.Entity;
using Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.FriendshipCase.ListAllAccepted
{
    public class ListAllRequestHandler : IRequestUserHandler<ListAllAcceptedQuery, IEnumerable<UserResponse>>
    {
        private readonly IEntityRepository<Friendship> _repository;
        private readonly IMapper _mapper;

        public ListAllRequestHandler(IEntityRepository<Friendship> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserResponse>>> Handle(ListAllAcceptedQuery request, CancellationToken cancellationToken)
        {
            var error = new Response<IEnumerable<UserResponse>>();

            try
            {
                string username = string.IsNullOrEmpty(request.Username) ? request.User.Username : request.Username;

                Expression<Func<Friendship, bool>> validFriend = f =>
                            f.ConfirmationDate != null &&
                            (f.From.Username == username || f.To.Username == username);

                Expression<Func<Friendship, User>> selectFriend = null;

                if (string.IsNullOrEmpty(request.Username))
                {
                    selectFriend = f
                        => request.User.Id == f.From.Id ? f.To : f.From;
                }
                else
                {
                    selectFriend = f
                        => request.Username == f.From.Username ? f.To : f.From;
                }

                var friends = _repository
                    .ObterQueryEntidade()
                    .Where(validFriend)
                    .Select(selectFriend)
                    .ToList();

                var response = _mapper.Map<IEnumerable<UserResponse>>(friends);

                return await Task.FromResult(new Response<IEnumerable<UserResponse>>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }
}
