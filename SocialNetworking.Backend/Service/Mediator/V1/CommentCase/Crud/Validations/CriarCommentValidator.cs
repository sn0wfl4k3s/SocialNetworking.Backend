using Core.Service.Requests;
using Domain.ViewModels.Comment;
using FluentValidation;

namespace Service.Mediator.V1.CommentCase.Crud.Validations
{
    public class CriarCommentValidator : AbstractValidator<CriarRequest<CommentRequest, CommentResponse>>
    {
        public CriarCommentValidator()
        {
            RuleFor(e => e.Entidade.Description)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}
