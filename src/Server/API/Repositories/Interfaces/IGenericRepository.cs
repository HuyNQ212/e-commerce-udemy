using API.Entities;
using API.Specifications;
using System.Linq.Expressions;

namespace API.Repositories.Interfaces
{
    /// <summary>
    /// Interface that defines common CRUD methods for repositories
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets all entities filtered by optional parameters
        /// </summary>
        /// <param name="filter">Optional filter</param>
        /// <param name="orderBy">Optional orderby clause</param>
        /// <param name="includeProperties">Optional included navigation properties</param>
        /// <returns>Task containing list of entities</returns>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Gets an entity by id
        /// </summary>
        /// <param name="id">Id of entity to get</param>
        /// <returns>Task containing entity or null</returns>
        Task<TEntity?> GetByID(object id);

        /// <summary>
        /// Deletes an entity by id
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        Task Delete(object id);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entityToDelete">Entity to delete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Inserts a single entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        Task Insert(TEntity entity);

        /// <summary>
        /// Inserts multiple entities
        /// </summary>
        /// <param name="entities">Array of entities to insert</param>
        Task Insert(params TEntity[] entities);

        /// <summary>
        /// Saves all pending changes to the database
        /// </summary>
        /// <returns>Number of changed rows</returns>
        Task<int> SaveChanges();

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entityToUpdate">Entity to update</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Updates an entity by id
        /// </summary>
        /// <param name="id">Id of entity to update</param>
        Task Update(object id);

        /// <summary>
        /// Gets a queryable collection of entities with optional filtering, sorting, and inclusion of deleted items.
        /// </summary>
        /// <param name="filter">A filter expression to apply to the query (optional).</param>
        /// <param name="orderBy">A function to specify sorting order for the query (optional).</param>
        /// <returns>A queryable collection of entities meeting the specified criteria.</returns>
        /// <remarks>
        /// This method allows you to retrieve entities from the repository with optional filtering and sorting.
        /// </remarks>
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

        Task<TEntity?> GetEntityWithSpec(ISpecification<TEntity> specification);

        Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification);
    }
}