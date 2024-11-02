using Application.Features.Products;
using Application.Features.User.Create;

namespace Application.Features.User;

public interface IUserService
{
    Task<ServiceResult<int>> CreateUserAsync(CreateUserRequest request);
    Task<ServiceResult<List<UserDto>>> GetAllListAsync();

    Task<ServiceResult<List<UserDto>>> GetPagedListAsync(int pageNumber, int pageSize);

    Task<ServiceResult<UserDto?>> GetByIdAsync(int id);

    Task<ServiceResult<string>> GetTokenAsync(string userName);  // Token oluşturma metodu

    Task<ServiceResult<int>> DeleteAsync(int id);
}