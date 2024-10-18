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

    ValueTask<T> GetByIdAsync(TId id);

    void Update(T entity);

    void Delete(T entity);

    ValueTask<T> CreateAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(TId id);

    Task<List<T>> GetAllAsync();


}
