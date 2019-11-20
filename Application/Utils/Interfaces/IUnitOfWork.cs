using System;

namespace Application.Utils.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        void Commit();
    }
}