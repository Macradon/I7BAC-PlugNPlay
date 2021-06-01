namespace PlugNPlayBackend.Models
{
    public class PlugNPlayDatabaseSettings : IPlugNPlayDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string GamesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IPlugNPlayDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string GamesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
