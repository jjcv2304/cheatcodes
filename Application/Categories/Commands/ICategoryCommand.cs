using Dtos;

namespace Application.Categories.Commands
{
    public interface ICategoryCommand
    {
        void Add(CategoryDto categoryDto);
        void Update(CategoryDto categoryDto);
        void Delete(CategoryDto categoryDto);
    }
}