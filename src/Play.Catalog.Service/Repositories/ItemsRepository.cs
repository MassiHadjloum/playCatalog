
using MongoDB.Driver;
using Play.Catalog.Service.Model;

namespace Play.Catalog.Service.Repositories
{

    public class ItemsRepository : IItemsRepository
  {

    private const string collectionName = "items";
    private readonly IMongoCollection<Item> dbCollection;

    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public ItemsRepository(IMongoDatabase database)
    {
      // connect to mangoDB
      dbCollection = database.GetCollection<Item>(collectionName);
    }

    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
      return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<Item> GetAsync(Guid id)
    {
      FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
      return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Item item)
    {
      ArgumentNullException.ThrowIfNull(item);
      await dbCollection.InsertOneAsync(item);
    }

    public async Task UpdateAsync(Item item){
      ArgumentNullException.ThrowIfNull(item);
      FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, item.Id);
      await dbCollection.ReplaceOneAsync(filter, item);
    }

    public async Task RemoveAsync(Guid id){
      ArgumentNullException.ThrowIfNull(id);
      FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
      await dbCollection.DeleteOneAsync(filter);
    }
  }
}