using Core.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace InfraData.Core
{
    public abstract class RepositoryCore<T> : IEntityRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;

        public RepositoryCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> AtualizarEntidadeAsync(T entidade)
        {
            using var transaction = _context.Database.BeginTransaction();

            var resultado = _context.Set<T>().Update(entidade);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return await Task.FromResult(resultado.Entity);
        }

        public virtual async Task<T> CriarEntidadeAsync(T entidade)
        {
            using var transaction = _context.Database.BeginTransaction();

            var resultado = await _context.Set<T>().AddAsync(entidade);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return await Task.FromResult(resultado.Entity);
        }

        public virtual IQueryable<T> ObterQueryEntidade()
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual async Task<T> RemoverEntidadeAsync(ulong id)
        {
            using var transaction = _context.Database.BeginTransaction();

            var entidade = await _context.Set<T>().FindAsync(id);

            _context.Set<T>().Remove(entidade);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return await Task.FromResult(entidade);
        }

        public virtual async Task<T> RemoverEntidadeAsync(T entidade)
        {
            using var transaction = _context.Database.BeginTransaction();

            var resultado = _context.Set<T>().Remove(entidade);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return await Task.FromResult(resultado.Entity);
        }
    }
}

