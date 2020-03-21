using System;
using System.Text;
using System.Threading.Tasks;
using CheatCodes.Search.RabbitMQ.Handlers;
using CheatCodes.Search.RabbitMQ.Models;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CheatCodes.Search.RabbitMQ
{
  public class RabbitMQConsumer : IRabbitMQConsumer, IDisposable
  {
    private readonly INewCategoryEventHandler _newCategoryEventHandler;
    private static ConnectionFactory _factory;
    private static IConnection _connection;
    private static IModel _channel;

    private const string ExchangeName = "Direct_Exchange";
    private const string CategoryCreateQueueKeyName = "CategoryCreate_Queue";
    private const string CategoryDeleteQueueKeyName = "CategoryDelete_Queue";
    private const string CategoryUpdateQueueKeyName = "CategoryUpdate_Queue";

    public RabbitMQConsumer(INewCategoryEventHandler newCategoryEventHandler)
    {
      _newCategoryEventHandler = newCategoryEventHandler;
    }

    public void CreateConnection()
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
    }

    public void Close()
    {
      _channel.Close();
      _connection.Close();
    }

    public void ProcessMessages()
    {
      _connection = _factory.CreateConnection();

      _channel = _connection.CreateModel();
      _channel.ExchangeDeclare(ExchangeName, type: "direct");

      _channel.QueueDeclare(CategoryCreateQueueKeyName, true, false, false, null);
      _channel.QueueBind(CategoryCreateQueueKeyName, ExchangeName, CategoryCreateQueueKeyName);

      _channel.QueueDeclare(CategoryDeleteQueueKeyName, true, false, false, null);
      _channel.QueueBind(CategoryDeleteQueueKeyName, ExchangeName, CategoryDeleteQueueKeyName);

      _channel.QueueDeclare(CategoryUpdateQueueKeyName, true, false, false, null);
      _channel.QueueBind(CategoryUpdateQueueKeyName, ExchangeName, CategoryUpdateQueueKeyName);

      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (model, ea) =>
      {
        var body = ea.Body;
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;
        switch (routingKey)
        {
          case CategoryCreateQueueKeyName:
            var newCategory = (NewCategoryEvent)ea.Body.DeSerialize(typeof(NewCategoryEvent));
            _newCategoryEventHandler.Handle(newCategory);
            _channel.BasicAck(ea.DeliveryTag, false);
            break;
          case CategoryDeleteQueueKeyName:
              // var deletedCategory = (CategoryDeleteCommand)ea.Body.DeSerialize(typeof(CategoryDeleteCommand));
              break;
          case CategoryUpdateQueueKeyName:
              // var updatedCategory = (CategoryUpdateCommand)ea.Body.DeSerialize(typeof(CategoryUpdateCommand));
              break;
        }
      };
      _channel.BasicConsume(queue: CategoryCreateQueueKeyName, autoAck: false, consumer: consumer);
      _channel.BasicConsume(queue: CategoryDeleteQueueKeyName, autoAck: false, consumer: consumer);
      _channel.BasicConsume(queue: CategoryUpdateQueueKeyName, autoAck: false, consumer: consumer);
    }

    public void Dispose()
    {
      Close();
    }
  }
}
