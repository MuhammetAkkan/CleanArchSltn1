namespace Application.Contracts.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task AddAsync<T>(string key, T value, TimeSpan exprTimeSpan);

    Task RemoveAsync(string key);

}