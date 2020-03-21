using Application.RabbitMQ;
using Application.Utils;
using Application.Utils.Interfaces;
using CSharpFunctionalExtensions;

namespace Application
{
    public sealed class CategoryUpdateCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryUpdateCommand(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        internal sealed class CategoryUpdateCommandHandler : ICommandHandler<CategoryUpdateCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CategoryUpdateCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public Result Handle(CategoryUpdateCommand categoryUpdateCommand)
            {
                var categoryRepository = _unitOfWork.CategoryCommandRepository;
                var category = MapService.Map(categoryUpdateCommand);
                categoryRepository.Update(category);
                SendNotification(categoryUpdateCommand);

                _unitOfWork.Commit();
                return Result.Ok();
            }
            private void SendNotification(CategoryUpdateCommand categoryUpdateCommand)
            {
              RabbitMQClient client = new RabbitMQClient();
              client.UpdateCategory(categoryUpdateCommand);
              client.Close();
            }
        }
    }
}
