using System.Linq.Expressions;
using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    // Task<ProductEntity> CreateAsync(ProductEntity entity);
    // Task<IEnumerable<ProductEntity>> GetAllAsync();
    // Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression);
    // Task<ProductEntity> UpdateAsync(Expression<Func<ProductEntity, bool>> expression, ProductEntity updatedEntity);
    // Task<bool> DeleteAsync(Expression<Func<ProductEntity, bool>> expression);
    // Task<bool> AlreadyExistsAsync(Expression<Func<ProductEntity, bool>> expression);
}