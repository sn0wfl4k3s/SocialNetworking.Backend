using Core.Service.Requests;
using Domain.ViewModels.Comment;

namespace Service.Mediator.V1.CommentCase.Crud.Validations
{
    public class RemoverCommentValidator : CommentValidator<RemoverRequest<CommentRequest, CommentResponse>>
    {
    }
}
