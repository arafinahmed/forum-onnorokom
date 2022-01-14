using Microsoft.EntityFrameworkCore;


namespace Onnorokom.Forum.DataAccessLayer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
