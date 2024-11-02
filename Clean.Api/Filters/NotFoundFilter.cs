using Application.Contracts.Persistance;
using Application.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Api.Filters;

public class NotFoundFilter<T, TId>(IGenericRepository<T, TId> genericRepository) : IAsyncActionFilter where T : class where TId : struct
{



    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        var idValue = context.ActionArguments.TryGetValue("id", out var idAsObject) ? idAsObject : null;


        if (idAsObject is not TId id)
        {
            await next();
            return;
        }

        //entity db de var mı?
        var anyEntity = await genericRepository.AnyAsync(id);

        if (anyEntity)
        {
            await next();
            return;
        }

        var entityName = typeof(T).Name; //T nin adını alır.

        //action ın adını alırız
        var actionName = context.ActionDescriptor.DisplayName;


        var result = ServiceResult.Fail($"data bulunmamıştır({entityName}, ({actionName}))");

        context.Result = new NotFoundObjectResult(result);



        //action metot çalıştıktan sonra çalışacak kodlar buraya yazılır.
    }
}