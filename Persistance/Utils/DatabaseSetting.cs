namespace Persistance.Utils
{
    public class DatabaseSetting
    {
        public string ConnectionString { get; set; }
        public DatabaseSetting(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}