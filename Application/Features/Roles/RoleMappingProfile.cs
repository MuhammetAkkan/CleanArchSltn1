using Application.Features.Roles.Create;
using Application.Features.Roles.Update;
using AutoMapper;

namespace Application.Features.Roles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Domain.Entities.Role, RoleDto>().ReverseMap();
        CreateMap<CreateRoleRequest, Domain.Entities.Role>();
        CreateMap<UpdateRoleRequest, Domain.Entities.Role>();

        CreateMap<bool, RoleCheckDto>()
            .ForMember(dest => dest.HasRole, opt => opt.MapFrom(src => src));
    }
}