namespace Persistance.Utils
{
  public class DatabaseSetting
  {
    public DatabaseSetting(string connectionString)
    {
      ConnectionString = connectionString;
    }

    public string ConnectionString { get; set; }
  }
}