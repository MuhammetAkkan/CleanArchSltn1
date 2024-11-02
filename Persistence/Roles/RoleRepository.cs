using Application.Contracts.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Roles;

public class RoleRepository(AppDbContext context) : GenericRepository<Role>(context), IRoleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> UserHasRoleAsync(int userId, string role)
    {
        return await _context.UserRoles
            .Include(p => p.Role)
            .AnyAsync(p => p.UserId == userId && p.Role.Name == role);
    }



}
