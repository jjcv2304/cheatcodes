using Application.RabbitMQ.Models;
using RabbitMQ.Client;

namespace Application.RabbitMQ
{
  public class RabbitMQClient
  {
    private const string ExchangeName = "Direct_Exchange";
    private const string CategoryCreateQueueKeyName = "CategoryCreate_Queue";
    private const string CategoryDeleteQueueKeyName = "CategoryDelete_Queue";
    private const string CategoryUpdateQueueKeyName = "CategoryUpdate_Queue";
    private static ConnectionFactory _factory;
    private static IConnection _connection;
    private static IModel _model;


    public RabbitMQClient()
    {
      CreateConnection();
    }

    private static void CreateConnection()
    {
      _factory = new ConnectionFactory
      {
        HostName = "localhost",
        VirtualHost = "/",
        UserName = "guest",
        Password = "guest",
        Port = AmqpTcpEndpoint.UseDefaultPort,
        AuthMechanisms = ConnectionFactory.DefaultAuthMechanisms
      };

      _connection = _factory.CreateConnection();
      _model = _connection.CreateModel();
      _model.ExchangeDeclare(ExchangeName, "direct");

      _model.QueueDeclare(CategoryCreateQueueKeyName, true, false, false, null);
      _model.QueueDeclare(CategoryDeleteQueueKeyName, true, false, false, null);
      _model.QueueDeclare(CategoryUpdateQueueKeyName, true, false, false, null);

      _model.QueueBind(CategoryCreateQueueKeyName, ExchangeName, CategoryCreateQueueKeyName);
      _model.QueueBind(CategoryDeleteQueueKeyName, ExchangeName, CategoryDeleteQueueKeyName);
      _model.QueueBind(CategoryUpdateQueueKeyName, ExchangeName, CategoryUpdateQueueKeyName);
    }

    public void Close()
    {
      _connection.Close();
    }

    public void CreateCategory(NewCategoryEvent newCategory)
    {
      SendMessage(newCategory.Serialize(), CategoryCreateQueueKeyName);
    }

    public void DeleteCategory(CategoryDeleteCommand deletedCategory)
    {
      SendMessage(deletedCategory.Serialize(), CategoryDeleteQueueKeyName);
    }

    public void UpdateCategory(CategoryUpdateCommand updatedCategory)
    {
      SendMessage(updatedCategory.Serialize(), CategoryUpdateQueueKeyName);
    }

    public void SendMessage(byte[] message, string routingKey)
    {
      _model.BasicPublish(ExchangeName, routingKey, null, message);
    }
  }
}