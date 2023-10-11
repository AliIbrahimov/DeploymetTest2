using CoreLayer.Entities.Abstract;
using System.Linq.Expressions;

namespace CoreLayer.DAL.EntityFramework;

public interface IBaseRepository<T> where T : IEntity, new()
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp = null, params string[] includes);
    Task<List<T>> GetAllPaginateAsync(int page, int size, Expression<Func<T, bool>> exp = null, params string[] includes);
    Task<T> GetAsync(Expression<Func<T, bool>> exp=null, params string[] includes);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> exp);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
