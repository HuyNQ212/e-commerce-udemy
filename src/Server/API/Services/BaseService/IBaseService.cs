using API.Entities;

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

    
}