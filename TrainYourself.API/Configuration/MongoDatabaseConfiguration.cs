﻿namespace TrainYourself.API.Configuration
{
    public class UsersDatabaseConfiguration : IMongoDatabaseConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public class MongoDatabaseConfiguration : IMongoDatabaseConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
