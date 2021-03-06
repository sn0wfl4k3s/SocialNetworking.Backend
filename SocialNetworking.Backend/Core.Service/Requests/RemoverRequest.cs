﻿using Core.Service.Core;
using Domain.Entity;

namespace Core.Service.Requests
{
    public class RemoverRequest<TRequest, TResponse> : IUserCommandRequest<TRequest, TResponse>
    {
        public User User { get; set; }
        public TRequest Entidade { get; set; }
        public ulong Id { get; set; }

        public RemoverRequest(ulong id)
        {
            Id = id;
        }

        public RemoverRequest(TRequest entidade)
        {
            Entidade = entidade;
        }
    }
}
