using MediatR;

namespace Core.Service.Requests
{
    public class ListarRequest<TResponse> : IRequest<Response<TResponse>>
    {
        public string Parametro1 { get; set; }
        public string Parametro2 { get; set; }
        public string Parametro3 { get; set; }
    }
}
