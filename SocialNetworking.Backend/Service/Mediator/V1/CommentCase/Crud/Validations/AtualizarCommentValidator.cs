using Core.Service.Requests;
using Domain.ViewModels.Comment;

namespace Service.Mediator.V1.CommentCase.Crud.Validations
{
    public class AtualizarCommentValidator : CommentValidator<AtualizarRequest<CommentRequest, CommentResponse>>
    {
    }
}
