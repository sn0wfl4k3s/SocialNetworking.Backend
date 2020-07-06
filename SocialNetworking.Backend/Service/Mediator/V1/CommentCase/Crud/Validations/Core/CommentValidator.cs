using Core.Service.Core;
using Domain.ViewModels.Comment;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Service.Mediator.V1.CommentCase.Crud.Validations
{
    public abstract class CommentValidator<T> : AbstractValidator<T>
        where T : class, IUserCommandRequest<CommentRequest, CommentResponse>
    {
        public CommentValidator()
        {
            RuleFor(e => e.Entidade.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(255)
                .WithName(e => nameof(e.Entidade.Description));

            RuleFor(e => e.Entidade.Files)
                .Must(TamanhoMaximo)
                .WithName(e => nameof(e.Entidade.Files));
        }


        private bool TamanhoMaximo(IEnumerable<IFormFile> arquivos)
        {
            if (arquivos is null)
                return true;

            var total = arquivos.Sum(a => a.Length);

            return total <= 2 * 1000 * 1000;
        }
    }
}
