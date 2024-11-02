using Application.Contracts.Persistance;
using Clean.Api.JWT;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Users;

public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;
    public async Task<User?> GetUserNameAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user;
    }
    public async Task<string> GenerateTokenAsync(string userName)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);
        if (user == null) return null;

        var token = JwtProvider.GenerateToken(user);
        user.Token = token;
        await _context.SaveChangesAsync();
        return token;
    }

}