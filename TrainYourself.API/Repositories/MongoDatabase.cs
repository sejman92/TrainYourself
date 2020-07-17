using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainYourself.API.Configuration;

namespace TrainYourself.API.Repositories
{
    public class MongoDatabase<T> : IDatabase<T>
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<T> Collection { get; }

        public MongoDatabase(IOptions<MongoDatabaseConfiguration> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            Database = client.GetDatabase(config.Value.DatabaseName);
            Collection = Database.GetCollection<T>(config.Value.CollectionName);

            //register class map ???
        }
    }
}
