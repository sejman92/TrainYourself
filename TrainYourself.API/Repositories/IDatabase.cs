using MongoDB.Driver;

namespace TrainYourself.API.Repositories
{
    public interface IDatabase<T>
    {
        IMongoDatabase Database { get; }
        IMongoCollection<T> Collection { get; }
    }
}
