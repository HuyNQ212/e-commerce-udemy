using API.Commons;
using API.Entities;
using System.Linq.Expressions;

namespace API.Services.BaseService;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{

    Task<int> AddAsync(TEntity entity);
    
    Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
    

    Task<bool> UpdateAsync(TEntity entity);
    
    Task<bool> DeleteAsync(int id);
    

    Task<bool> DeleteAsync(TEntity entity);
    

    Task<TEntity?> GetByIdAsync(int id);
    

    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Return entities with paging, filtering, ordering
    /// </summary>
    /// <param name="filter">x=>x.Name.Contains("abc")</param>
    /// <param name="orderBy">q => q.OrderByDescending(c => c.Name);</param>
    /// <param name="includeProperties">"Products", "Authors, Category, Publisher"</param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "", int pageIndex = 1, int pageSize = 10);
}