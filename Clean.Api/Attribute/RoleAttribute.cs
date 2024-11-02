using Application.Features.Roles;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clean.Api.Attribute;

public sealed class RoleAttribute : System.Attribute, IAuthorizationFilter
{
    private readonly string _role;
    private readonly IRoleService _roleService;

    public RoleAttribute(string role, IRoleService roleService)
    {
        _role = role;
        _roleService = roleService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userHasRoleResult = _roleService.UserHasRole(userIdClaim.Value, _role).Result; // Senkron bekle

        if (!userHasRoleResult.Data) // ServiceResult'den sonucu kontrol et
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}