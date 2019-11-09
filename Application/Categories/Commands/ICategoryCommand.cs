using Application.Categories.Queries.ViewModels;

namespace Application.Categories.Commands
{
    public interface ICategoryCommand
    {
        void Add(CategoryVM categoryVM);
        void Update(CategoryVM categoryVM);
        void Delete(CategoryVM categoryVM);
    }
}