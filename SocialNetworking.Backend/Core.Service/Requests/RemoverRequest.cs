using MediatR;

namespace Core.Service.Requests
{
    public class RemoverRequest<TSource, TResponse> : IRequest<Response<TResponse>>
    {
        public string Authorization { get; set; }
        public TSource Entidade { get; set; }
        public ulong Id { get; set; }


        public RemoverRequest(string authorization, ulong id)
        {
            Authorization = authorization;
            Id = id;
        }

        public RemoverRequest(string authorization, TSource entidade)
        {
            Authorization = authorization;
            Entidade = entidade;
        }
    }
}
