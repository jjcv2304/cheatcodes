using System;
using System.Text;
using System.Threading.Tasks;
using CheatCodes.Search.DB;
using CheatCodes.Search.RabbitMQ.Handlers;
using CheatCodes.Search.RabbitMQ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace CheatCodes.Search.RabbitMQ
{
  public class RabbitMQConsumer : IRabbitMQConsumer, IDisposable
  {
    private readonly IServiceProvider _serviceProvider;
    private const string ExchangeName = "Direct_Exchange";
    private const string CategoryCreateQueueKeyName = "CategoryCreate_Queue";
    private const string CategoryDeleteQueueKeyName = "CategoryDelete_Queue";
    private const string CategoryUpdateQueueKeyName = "CategoryUpdate_Queue";
    private static ConnectionFactory _factory;
    private static IConnection _connection;
    private static IModel _channel;

    public RabbitMQConsumer(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public void Dispose()
    {
      Close();
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
      _channel.ExchangeDeclare(ExchangeName, "direct");

      _channel.QueueDeclare(CategoryCreateQueueKeyName, true, false, false, null);
      _channel.QueueBind(CategoryCreateQueueKeyName, ExchangeName, CategoryCreateQueueKeyName);

      _channel.QueueDeclare(CategoryDeleteQueueKeyName, true, false, false, null);
      _channel.QueueBind(CategoryDeleteQueueKeyName, ExchangeName, CategoryDeleteQueueKeyName);

      _channel.QueueDeclare(CategoryUpdateQueueKeyName, true, false, false, null);
      _channel.QueueBind(CategoryUpdateQueueKeyName, ExchangeName, CategoryUpdateQueueKeyName);

      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += async (model, ea) =>
      {
        try
        {
          var body = ea.Body;
          var message = Encoding.UTF8.GetString(body);
          var routingKey = ea.RoutingKey;
          var options = _serviceProvider.GetService<DbContextOptions<CheatCodesDbContext>>();
          var _context = new CheatCodesDbContext(options);

          switch (routingKey)
          {
            case CategoryCreateQueueKeyName:
              var newCategory = (NewCategoryEvent)ea.Body.DeSerialize(typeof(NewCategoryEvent));
              var newCategoryEventHandler = _serviceProvider.GetRequiredService<INewCategoryEventHandler>();
              await newCategoryEventHandler.Handle(newCategory, _context);
              _channel.BasicAck(ea.DeliveryTag, false);
              break;
            case CategoryDeleteQueueKeyName:
              var deletedCategory = (DeleteCategoryEvent)ea.Body.DeSerialize(typeof(DeleteCategoryEvent));
              var deleteCategoryEventHandler = _serviceProvider.GetRequiredService<IDeleteCategoryEventHandler>();
              deleteCategoryEventHandler.Handle(deletedCategory, _context);
              _channel.BasicAck(ea.DeliveryTag, false);
              break;
            case CategoryUpdateQueueKeyName:
              var updatedCategory = (UpdateCategoryEvent)ea.Body.DeSerialize(typeof(UpdateCategoryEvent));
              var updateCategoryEventHandler = _serviceProvider.GetRequiredService<IUpdateCategoryEventHandler>();
              updateCategoryEventHandler.Handle(updatedCategory, _context);
              _channel.BasicAck(ea.DeliveryTag, false);
              break;
          }
        }
        catch (Exception e)
        {
          Log.Error(e, "Error in Rabbitmq search mnodule");
        }
      };
      _channel.BasicConsume(CategoryCreateQueueKeyName, false, consumer);
      _channel.BasicConsume(CategoryDeleteQueueKeyName, false, consumer);
      _channel.BasicConsume(CategoryUpdateQueueKeyName, false, consumer);

    }
  }
}