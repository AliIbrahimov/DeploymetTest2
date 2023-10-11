using CoreLayer.Entities.Abstract;
using CoreLayer.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System.Linq.Expressions;

namespace CoreLayer.DAL.EntityFramework;

public class BaseRepository<T, TContext> : IBaseRepository<T>
    where T : IEntity, new()
    where TContext : DbContext
{
    private readonly TContext _context;

    public BaseRepository(TContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp = null, params string[] includes)
    {
        IQueryable<T> query = GetQuery(includes);
        return exp is null
            ? await query.AsNoTracking().ToListAsync()
            : await query.AsNoTracking().Where(exp).ToListAsync();
    }

    public async Task<List<T>> GetAllPaginateAsync(int page, int size, Expression<Func<T, bool>> exp = null, params string[] includes)
    {
        IQueryable<T> query = GetQuery(includes);
        return exp is null
            ? await query.AsNoTracking().Skip((page - 1) * size).Take(size).ToListAsync()
            : await query.AsNoTracking().Where(exp).Skip((page - 1) * size).Take(size).ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> exp=null, params string[] includes)
    {
        IQueryable<T> query = GetQuery(includes);
        T? result = await query.AsNoTracking().SingleOrDefaultAsync(exp);

        if (result is null)
            throw new NotFoundException($"No {typeof(T).Name} found matching the criteria.");
        
        return result;
    }



    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> exp)
    {
        return await _context.Set<T>().AnyAsync(exp);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
    private IQueryable<T> GetQuery(string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes is not null)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }
        return query;
    }
}