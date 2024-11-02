using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Clean.Api.ExceptionHandler;

public class CriticalExceptionHandler() : IExceptionHandler
{
   
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //business logic

        if (exception is CriticalException)
        {
            Console.WriteLine("Hata hata hata!!!!"); //bu
        }

        return ValueTask.FromResult(false); //bir sonraki handler çalışsın => GlobalExceptionHandler
    }
}