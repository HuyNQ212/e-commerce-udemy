using API.Data;
using API.Entities;
using API.Repositories.Interfaces;
using API.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories.Implements
{
    /// <summary>
    /// GenericRepository class that implements IGenericRepository interface.
    /// It provides CRUD operations for a generic entity TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity. TEntity must be a class.</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly StoreContext context;
        protected readonly DbSet<TEntity> dbSet;

        /// <summary>
        /// Constructor for GenericRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public GenericRepository(StoreContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets all entities filtered by optional parameters
        /// </summary>
        /// <param name="filter">Optional filter</param>
        /// <param name="orderBy">Optional orderby clause</param>
        /// <param name="includeProperties">Optional included navigation properties</param>
        /// <returns>Task containing list of entities</returns>
        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Gets an entity by id
        /// </summary>
        /// <param name="id">Id of entity to get</param>
        /// <returns>Task containing entity or null</returns>
        public virtual async Task<TEntity?> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <summary>
        /// Inserts a single entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        public virtual async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Inserts multiple entities
        /// </summary>
        /// <param name="entities">Array of entities to insert</param>
        public virtual async Task Insert(params TEntity[] entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Deletes an entity by id
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        public virtual async Task Delete(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entityToDelete">Entity to delete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entityToUpdate">Entity to update</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
        }

        /// <summary>
        /// Updates an entity by id
        /// </summary>
        /// <param name="id">Id of entity to update</param>
        public async Task Update(object id)
        {
            TEntity? entity = await GetByID(id);
            if (entity != null)
            {
                Update(entity);
            }
        }

        /// <summary>
        /// Saves all pending changes to the database
        /// </summary>
        /// <returns>Number of changed rows</returns>
        public async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public async Task<TEntity?> GetEntityWithSpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(context.Set<TEntity>().AsQueryable(), specification);
        }
    }
}
