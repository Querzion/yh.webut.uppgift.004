// using System.Linq.Expressions;
// using Business.Dtos;
// using Business.Factories;
// using Business.Models;
// using Data.Entities;
// using Microsoft.EntityFrameworkCore;
//
// namespace Business.Services;
//
// public abstract class BaseService<TEntity>(DbContext context) where TEntity : class
// {
//     private readonly DbContext context = context;
//     private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
//     
//     public async Task<TEntity> CreateAsync(TEntity form)
//     {
//         // Check if a product already exists
//         var entity = await _dbSet.GetAsync(x => x.Name == form.Name);
//         entity ??= await _productRepository.CreateAsync(ProductFactory.Create(form));
//         
//         return ProductFactory.Create(entity);
//     }
//
//     public async Task<IEnumerable<Product>> GetAllProductsAsync()
//     {
//         var entities = await _productRepository.GetAllAsync();
//         var products = entities.Select(ProductFactory.Create);
//         return products;
//     }
//     
//     public async Task<Product> GetProductAsync(Expression<Func<ProductEntity, bool>> expression)
//     {
//         var entity = await _productRepository.GetAsync(expression);
//         var product = ProductFactory.Create(entity);
//         return product ?? null!;
//     }
//
//     public async Task<Product> UpdateProductAsync(ProductUpdateForm form)
//     {
//         var entity = await _productRepository.UpdateAsync(ProductFactory.Create(form));
//         var product = ProductFactory.Create(entity);
//         return product ?? null!;
//     }
//
//     public async Task<bool> DeleteProductAsync(int id)
//     {
//         var result = await _productRepository.DeleteAsync(x => x.Id == id);
//         return result;
//     }
//
//     public async Task<bool> CheckIfAlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
//     {
//         return await _dbSet.AnyAsync(expression);
//     }
// }