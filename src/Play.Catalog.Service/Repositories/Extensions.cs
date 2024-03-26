using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Catalog.Service.Model;
using Play.Catalog.Service.Settings;

namespace Play.Catalog.Service.Repositories
{
  public static class Extensions
  {
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {

      // store document id in mongo as string
      BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
      BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

      services.AddSingleton(serviceProvider =>
      {
        var configuration = serviceProvider.GetService<IConfiguration>()!;

        // at the runtime the compiler verify configuration from appsettings.json file
        // and get the props of ServiceSettings (serviceName)
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>()!;
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
        var mongoClient = new MongoClient(mongoDbSettings!.ConnexionString);
        return mongoClient.GetDatabase(serviceSettings.ServiceName);
      });
      return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
    where T : IEntity
    {
      services.AddSingleton<IRepository<T>>(serviceProvider =>
      {
        var database = serviceProvider.GetService<IMongoDatabase>()!;
        return new MongoRepository<T>(database, collectionName);
      });
      return services;
    }
  }
}