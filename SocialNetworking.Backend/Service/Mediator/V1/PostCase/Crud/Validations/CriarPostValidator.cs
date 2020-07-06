using Core.Service.Requests;
using Domain.ViewModels.Post;

namespace Service.Mediator.V1.PostCase.Crud.Validations
{
    public class CriarPostValidator : PostValidator<CriarRequest<PostRequest, PostResponse>>
    {
    }
}
