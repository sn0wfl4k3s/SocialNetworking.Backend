using MediatR;

namespace Core.Service.Requests
{
    public class AtualizarRequest<TSource, TResponse> : IRequest<Response<TResponse>>
    {
        public string Authorization { get; set; }
        public TSource Entidade { get; set; }
        public ulong Id { get; set; }

        public AtualizarRequest(string authorization, TSource entidade, ulong id)
        {
            Authorization = authorization;
            Entidade = entidade;
            Id = id;
        }

        public AtualizarRequest(string authorization, TSource entidade)
        {
            Authorization = authorization;
            Entidade = entidade;
        }
    }
}
