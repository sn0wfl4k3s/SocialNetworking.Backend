namespace Core.Service.Core
{
    public interface IUserCommandRequest<TRequest, TResponse> : IRequestUser<TResponse>
    {
        TRequest Entidade { get; set; }
    }
}
