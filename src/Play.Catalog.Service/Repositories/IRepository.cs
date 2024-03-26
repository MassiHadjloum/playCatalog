using Play.Catalog.Service.Model;

namespace Play.Catalog.Service.Repositories
{
    public interface IRepository<T> where T : IEntity {
    Task CreateAsync(T item);
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(T item);
  }
}