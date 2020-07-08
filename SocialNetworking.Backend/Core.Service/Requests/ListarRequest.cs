using Core.Service.Core;
using Domain.Entity;
using System.Collections.Generic;

namespace Core.Service.Requests
{
    public class ListarRequest<TResponse> : IRequestUser<TResponse>
    {
        public User User { get; set; }
        public string Parameter { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
    }
}
