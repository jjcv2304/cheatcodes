using CSharpFunctionalExtensions;

namespace Application.Utils.Interfaces
{
  public interface ICommandHandler<TCommand> where TCommand : ICommand
  {
    Result Handle(TCommand command);
  }
}