using Core.Service.Core;
using Domain.ViewModels.Post;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Service.Mediator.V1.PostCase.Crud.Validations
{
    public abstract class PostValidator<T> : AbstractValidator<T>
        where T : class, IUserCommandRequest<PostRequest, PostResponse>
    {
        public PostValidator()
        {
            RuleFor(e => e.Entidade.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(255)
                .WithName(e => nameof(e.Entidade.Title));

            RuleFor(e => e.Entidade.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MinimumLength(3)
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
