using MediatR;

namespace Core.Service.Requests
{
    public class CriarRequest<TSource, TResponse> : IRequest<Response<TResponse>>
    {
        public string Authorization { get; set; }
        public TSource Entidade { get; set; }

        public CriarRequest(string authorization, TSource entidade)
        {
            Authorization = authorization;
            Entidade = entidade;
        }
    }
}
