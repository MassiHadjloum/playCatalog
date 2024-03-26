
using MongoDB.Driver;
using Play.Catalog.Service.Model;

namespace Play.Catalog.Service.Repositories
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
  {

    private readonly IMongoCollection<T> dbCollection;

    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
      // connect to mangoDB
      dbCollection = database.GetCollection<T>(collectionName);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
      return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<T> GetAsync(Guid id)
    {
      FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
      return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T item)
    {
      ArgumentNullException.ThrowIfNull(item);
      await dbCollection.InsertOneAsync(item);
    }

    public async Task UpdateAsync(T item){
      ArgumentNullException.ThrowIfNull(item);
      FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, item.Id);
      await dbCollection.ReplaceOneAsync(filter, item);
    }

    public async Task RemoveAsync(Guid id){
      ArgumentNullException.ThrowIfNull(id);
      FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
      await dbCollection.DeleteOneAsync(filter);
    }
  }
}