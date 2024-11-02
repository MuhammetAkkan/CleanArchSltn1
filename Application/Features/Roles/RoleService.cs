using System;
using System.Net;
using Application.Contracts.Caching;
using Application.Contracts.Persistance;
using Application.Features.Categories;
using Application.Features.Roles.Create;
using Application.Features.Roles.Update;
using Application.Features.User;
using Application.Features.User.Create;
using Application.Features.User.Update;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Roles;

public sealed class RoleService(IRoleRepository roleRepository, IMapper mapper, IValidator<CreateRoleRequest> creValidator, IValidator<UpdateRoleRequest> updateValidator, IUnitOfWork unitOfWork, ICacheService cacheService) : IRoleService
{
    public async Task<ServiceResult<bool>> UserHasRole(string userId, string role)
    {
        try
        {
            // userId'yi int'e dönüştür
            int userIdInt = Convert.ToInt32(userId);

            // Kullanıcının belirtilen role sahip olup olmadığını kontrol et
            var userHasRole = await roleRepository.UserHasRoleAsync(userIdInt, role);
            return ServiceResult<bool>.Success(userHasRole);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>.Fail(new List<string> { ex.Message }, HttpStatusCode.InternalServerError);
        }
    }


    public async Task<ServiceResult<int>> CreateRoleAsync(CreateRoleRequest request)
    {
        var role = mapper.Map<Role>(request);

        await roleRepository.CreateAsync(role);

        await unitOfWork.SaveChangesAsync();

        string url = $"api/roles/{role.Id}";

        return ServiceResult<int>.SuccessAsCreated(role.Id, url);
    }

    public async Task<ServiceResult> UpdateRoleAsync(int id, UpdateRoleRequest request)
    {
        var role = await roleRepository.GetByIdAsync(id);

        if (role is null)
        {
            return ServiceResult.Fail("Role not found");
        }

        mapper.Map(request, role);

        roleRepository.Update(role);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();

    }

    public async Task<ServiceResult> DeleteRoleAsync(int id)
    {
        var role = await roleRepository.GetByIdAsync(id);

        if (role is null)
        {
            return ServiceResult.Fail("Role not found");
        }

        roleRepository.Delete(role);

        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<List<RoleDto>>> GetAllListAsync()
    {
        var roles = await roleRepository.GetAllAsync();

        var result = mapper.Map<List<RoleDto>>(roles);

        return ServiceResult<List<RoleDto>>.Success(result);

    }

    public async Task<ServiceResult<RoleDto?>> GetByIdAsync(int id)
    {
        var role = await roleRepository.GetByIdAsync(id);

        if (role is null)
        {
            return ServiceResult<RoleDto?>.Fail($"Role is not found:");
        }

        var result = mapper.Map<RoleDto>(role);

        return ServiceResult<RoleDto?>.Success(result);
    }
}