using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task<TEntity> CreateAsync(TEntity entity);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    public Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
    public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    public Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
}