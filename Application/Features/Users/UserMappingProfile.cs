using Application.Features.User.Create;
using Application.Features.User.Update;
using Domain.Entities;
using AutoMapper;

namespace Application.Features.User;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Domain.Entities.User, UserDto>().ReverseMap();

        CreateMap<CreateUserRequest, Domain.Entities.User>();

        CreateMap<UpdateUserRequest, Domain.Entities.User>();

    }
}