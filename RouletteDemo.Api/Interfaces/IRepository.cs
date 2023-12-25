
namespace RouletteDemo.Api.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
        IEnumerable<T> GetItems(Func<T, bool> filter);
        Task<T> GetLastItem(CancellationToken cancellationToken);
    }
}