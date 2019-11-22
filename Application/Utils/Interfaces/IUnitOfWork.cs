using System;

namespace Application.Utils.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryCommandRepository CategoryCommandRepository { get; }

        void Commit();
    }
}