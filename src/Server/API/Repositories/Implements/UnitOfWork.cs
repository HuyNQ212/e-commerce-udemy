using API.Data;
using API.Entities;
using API.Repositories.Interfaces;

namespace API.Repositories.Implements
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly StoreContext context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(StoreContext context)
        {
            this.context = context;
            _repositories = new Dictionary<Type, object>();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new GenericRepository<TEntity>(context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
