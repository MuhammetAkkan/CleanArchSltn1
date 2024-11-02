using Application.Contracts.Caching;
using Application.Contracts.Persistance;
using Application.Features.Categories;
using Application.Features.Categories.Create;
using Application.Features.User.Create;
using Application.Features.User.Update;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.User;

public class UserService(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserRequest> createValidator, IValidator<UpdateUserRequest> updateValidator, IUnitOfWork unitOfWork) : IUserService
{
    public async Task<ServiceResult<int>> CreateUserAsync(CreateUserRequest request)
    {
        var user = mapper.Map<Domain.Entities.User>(request);

        await userRepository.CreateAsync(user);

        await unitOfWork.SaveChangesAsync();

        string url = $"api/categories/{user.Id}";

        return ServiceResult<int>.SuccessAsCreated(user.Id, url);
    }
   

    public async Task<ServiceResult<List<UserDto>>> GetAllListAsync()
    {
        var users = await userRepository.GetAllAsync();

        var result = mapper.Map<List<UserDto>>(users);

        return ServiceResult<List<UserDto>>.Success(result);
    }

    public async Task<ServiceResult<List<UserDto>>> GetPagedListAsync(int pageNumber, int pageSize)
    {
        var skipValue = (pageNumber - 1) * pageSize;
        
        var products = await userRepository.GetAllPagedAsync(pageNumber, pageSize);

        var productAsDto = mapper.Map<List<UserDto>>(products);

        return ServiceResult<List<UserDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<UserDto?>> GetByIdAsync(int id)
    {
        var category = await userRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult<UserDto?>.Fail($"Category is not fount:");
        }

        var result = mapper.Map<UserDto>(category);

        return ServiceResult<UserDto?>.Success(result);

    }
    public async Task<ServiceResult<string>> GetTokenAsync(string userName)
    {
        var token = await userRepository.GenerateTokenAsync(userName);
        if (token == null)
        {
            return ServiceResult<string>.Fail($"User with username '{userName}' not found.");
        }

        return ServiceResult<string>.Success(token);
    }
}