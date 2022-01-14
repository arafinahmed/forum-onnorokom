using System;


namespace Onnorokom.Forum.DataAccessLayer.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
