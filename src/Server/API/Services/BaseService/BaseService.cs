using System.Linq.Expressions;
using API.Commons;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.BaseService;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IGenericRepository<TEntity> _genericRepository;

    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = _unitOfWork.GenericRepository<TEntity>();
    }
    
    public virtual async Task<int> AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException();
        }

        await _unitOfWork.GenericRepository<TEntity>().Insert(entity);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _unitOfWork.GenericRepository<TEntity>().Insert(entities.ToArray());

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.GenericRepository<TEntity>().GetByID(id);
        if (entity == null)
        {
            throw new ArgumentNullException();
        }

        _unitOfWork.GenericRepository<TEntity>().Delete(entity);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _unitOfWork.GenericRepository<TEntity>().Delete(entity);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
    

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _unitOfWork.GenericRepository<TEntity>().Get();
    }
    

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _unitOfWork.GenericRepository<TEntity>().GetByID(id);
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException();
        }

        _unitOfWork.GenericRepository<TEntity>().Update(entity);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public virtual async Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10)
    {
        IQueryable<TEntity> query = _unitOfWork.GenericRepository<TEntity>().GetQuery();

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
            query = orderBy(query);
        }

        return await Paginated<TEntity>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
    }
}