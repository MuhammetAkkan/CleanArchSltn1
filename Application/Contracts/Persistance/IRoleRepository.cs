using Domain.Entities;

namespace Application.Contracts.Persistance;

public interface IRoleRepository : IGenericRepository<Role, int>
{
    public Task<bool> UserHasRoleAsync(int userId, string role);
}