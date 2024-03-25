namespace Play.Catalog.Service.Settings
{
  public class MongoDbSettings
  {
    // var mongoClient = new MongoClient("mongodb://localhost:27017");
    //   var database = mongoClient.GetDatabase("Catalog");
    public string Host { get; init; } = "";
    public int Port { get; init; }

    public string ConnexionString => $"mongodb://{Host}:{Port}";
  }
}