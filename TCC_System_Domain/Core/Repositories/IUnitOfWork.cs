using System;

namespace TCC_System_Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Commit(string Usuario);
    }
}