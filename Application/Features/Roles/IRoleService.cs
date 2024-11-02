using Application.Features.Categories.Create;
using Application.Features.Categories.Update;
using Application.Features.Categories;
using Application.Features.Roles.Create;
using Application.Features.Roles.Update;



namespace Application.Features.Roles;

public interface IRoleService
{
    public Task<ServiceResult<bool>> UserHasRole(string userId, string role);

    Task<ServiceResult<int>> CreateRoleAsync(CreateRoleRequest request);

    Task<ServiceResult> UpdateRoleAsync(int id, UpdateRoleRequest request);

    Task<ServiceResult> DeleteRoleAsync(int id);

    Task<ServiceResult<List<RoleDto>>> GetAllListAsync();

    Task<ServiceResult<RoleDto?>> GetByIdAsync(int id);
}