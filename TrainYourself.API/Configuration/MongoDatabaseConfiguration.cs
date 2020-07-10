namespace TrainYourself.API.Configuration
{
    public class UsersDatabaseConfiguration : IMongoDatabaseConfiguration
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseConfiguration
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
