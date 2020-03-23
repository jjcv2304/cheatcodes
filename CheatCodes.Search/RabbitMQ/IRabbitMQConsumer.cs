namespace CheatCodes.Search.RabbitMQ
{
  public interface IRabbitMQConsumer
  {
    void CreateConnection();
    void Close();
    void ProcessMessages();
  }
}