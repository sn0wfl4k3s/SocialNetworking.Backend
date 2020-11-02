using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Handlers;
using Core.Service.Requests;
using Domain.Entity;
using Domain.ViewModels.Other;
using Domain.ViewModels.User;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.ProfileCase.Crud.Handlers
{
    public class SearchProfilesHandler : IProcurarHandler<SearchProfileRequest, IEnumerable<UserResponse>>
    {
        private readonly IEntityRepository<User> _repository;
        private readonly IMapper _mapper;

        public SearchProfilesHandler(IEntityRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserResponse>>> Handle(ProcurarRequest<SearchProfileRequest, IEnumerable<UserResponse>> request, CancellationToken cancellationToken)
        {
            var error = new Response<IEnumerable<UserResponse>>();

            try
            {
                var listaUsuarios = _repository.ObterQueryEntidade().AsNoTracking().ToListAsync();

                var search = request.Entidade;

                var predicate = PredicateBuilder.New<User>(true);

                if (!string.IsNullOrEmpty(search.Name))
                {
                    predicate = predicate.And(u => u.Name.RemoveAccents()
                        .Contains(search.Name.RemoveAccents(), StringComparison.InvariantCultureIgnoreCase));
                }

                if (!string.IsNullOrEmpty(search.Lastname))
                {
                    predicate = predicate.And(u => u.LastName.RemoveAccents()
                        .Contains(search.Lastname.RemoveAccents(), StringComparison.InvariantCultureIgnoreCase));
                }

                if (!string.IsNullOrEmpty(search.Email))
                {
                    predicate = predicate.And(u => u.Email.RemoveAccents()
                        .Contains(search.Email.RemoveAccents(), StringComparison.InvariantCultureIgnoreCase));
                }

                if (!string.IsNullOrEmpty(search.Biography))
                {
                    predicate = predicate.And(u => u.Biography.RemoveAccents()
                        .Contains(search.Biography.RemoveAccents(), StringComparison.InvariantCultureIgnoreCase));
                }

                var usuariosFiltrados = (await listaUsuarios).Where(predicate).ToList();

                var response = _mapper.Map<IEnumerable<UserResponse>>(usuariosFiltrados);

                return await Task.FromResult(new Response<IEnumerable<UserResponse>>(response));
            }
            catch (Exception e)
            {
                error.AddError(e.Source, e.Message);
            }

            return await Task.FromResult(error);
        }
    }

    public static class StringExtension
    {
        public static string RemoveAccents(this string str)
        {
            return new string(str
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }
    }
}
