using System.Linq.Expressions;

namespace Application.Contracts.Persistance;

public interface IGenericRepository<T, in TId> where T : class where TId : struct
{
    #region MetotlarList
    /*
     * GetById => ValueTask<T>
     * Craete => Task
     * Delete => void
     * Update => void
     *Any => Task<bool>
     *
    */
    #endregion

    ValueTask<T?> GetByIdAsync(TId id);

    void Update(T entity);

    void Delete(T entity);

    ValueTask CreateAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(TId id);

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);


}

public interface IGenericRepository<T> where T : class
{
    #region MetotlarList
    /*
     * GetById => ValueTask<T>
     * Create => Task
     * Delete => void
     * Update => void
     * Any => Task<bool>
     *
     */
    #endregion

    ValueTask<T?> GetByIdAsync(int id); // Burada int tipini kullanıyoruz. İstersen başka bir tip de kullanabilirsin.

    void Update(T entity);

    void Delete(T entity);

    ValueTask CreateAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(int id); // Burada da id'nin int olduğunu varsayıyoruz.

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);
}

