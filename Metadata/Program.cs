using Application.Utils.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Metadata
{
  class Program
  {
    static void Main(string[] args)
    {
      FindHandlers();
    }

    public static void FindHandlers()
    {
      //var handlerTypes = typeof(ICommand).Assembly.GetTypes()
      //  .Where(x => x.GetInterfaces().Any(y => IsHandlerInterface(y)))
      //  .Where(x => x.Name.EndsWith("Handler"))
      //  .ToList();

      var assemblyTypes = typeof(ICommand).Assembly.GetTypes().ToList();
      Console.WriteLine("assemblyTypes to string : " + assemblyTypes.ToString());
      foreach (var type in assemblyTypes)
      {
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("-----------------------------------------");

        Console.WriteLine("assemblyTypes -> FullName : " + type.FullName);

        Console.WriteLine("assemblyTypes -> Name : " + type.Name);

        Console.WriteLine("assemblyTypes -> Attributes : " + type.Attributes);

        foreach (var field in type.GetFields())
          Console.WriteLine("assemblyTypes -> field : " + field);

        foreach (var method in type.GetMethods())
          Console.WriteLine("assemblyTypes -> method : " + method);

        Console.WriteLine("assemblyTypes -> GetTypeInfo : " + type.GetTypeInfo());

      }


      //var assemblyInterfaces = assemblyTypes.Where(x => x.GetInterfaces().Any());
      //Trace("assemblyInterfaces: ", assemblyInterfaces);

      //var handlerInterfaces = assemblyInterfaces.Where(y => IsHandlerInterface(y));
      //Trace("handlerInterfaces: ", handlerInterfaces);

      //var handlers = handlerInterfaces.Where(x => x.Name.EndsWith("Handler"));
      //Trace("handlers: ", handlers);


    }
    private static bool IsHandlerInterface(Type type)
    {
      if (!type.IsGenericType)
        return false;

      var typeDefinition = type.GetGenericTypeDefinition();

      return typeDefinition == typeof(ICommandHandler<>) || typeDefinition == typeof(IQueryHandler<,>);
    }

    private static void Trace(string title, object value)
    {
      Console.WriteLine("Type: " + value.ToString());
    }
    private static void Trace(string title, object[] values)
    {
      foreach (var value in values) Trace(title, value);
    }
  }

}
