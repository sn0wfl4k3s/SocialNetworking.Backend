using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        IQueryable<T> ObterQueryEntidade();
        Task<T> CriarEntidadeAsync(T entidade);
        Task<T> AtualizarEntidadeAsync(T entidade);
        Task<T> RemoverEntidadeAsync(ulong id);
        Task<T> RemoverEntidadeAsync(T entidade);
    }
}
