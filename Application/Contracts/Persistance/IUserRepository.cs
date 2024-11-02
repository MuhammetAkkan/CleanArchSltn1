using Domain.Entities;

namespace Application.Contracts.Persistance;

public interface IUserRepository : IGenericRepository<User, int>
{
    Task<User?> GetUserNameAsync(int id);
    Task<string> GenerateTokenAsync(string userName); // Token oluşturma metodu
}