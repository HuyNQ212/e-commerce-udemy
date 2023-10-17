using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        void Dispose();
        
        /// <summary>
        /// Gets a repository for the specified entity type, creating a new one if it doesn't exist.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity for which the repository is obtained.</typeparam>
        /// <returns>An instance of the repository for the specified entity type.</returns>
        /// <remarks>The repository is stored in a dictionary to ensure a single instance per entity type.</remarks>
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;

        Task<int> SaveChangesAsync();
    }
}