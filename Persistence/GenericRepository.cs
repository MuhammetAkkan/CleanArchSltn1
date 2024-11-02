using Application.Contracts.Persistance;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence;

public class GenericRepository<T, TId>(AppDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
{
    private readonly AppDbContext _context = context;


    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async ValueTask<T?> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async ValueTask CreateAsync(T entity) => await _dbSet.AddAsync(entity);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);

    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(e => e.Id.Equals(id));

    public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return _dbSet.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
    }
}

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async ValueTask<T?> GetByIdAsync(int id)
    {
        // T'nin bir Id property’si olması gerektiğini varsayıyoruz. 
        // Bunun için Reflection kullanılabilir.
        return await _dbSet.FindAsync(id);
    }

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async ValueTask CreateAsync(T entity) => await _dbSet.AddAsync(entity);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);

    public Task<bool> AnyAsync(int id) => _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id); // "Id" property'sini varsayıyoruz.

    public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return _dbSet.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
    }
}

