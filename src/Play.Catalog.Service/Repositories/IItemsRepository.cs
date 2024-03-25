using Play.Catalog.Service.Model;

namespace Play.Catalog.Service.Repositories
{
    public interface IItemsRepository {
    Task CreateAsync(Item item);
    Task<IReadOnlyCollection<Item>> GetAllAsync();
    Task<Item> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Item item);
  }
}