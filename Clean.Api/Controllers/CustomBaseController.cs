using Application.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clean.Api.Controllers;

public class CustomBaseController : Controller
{
    [NonAction]
    public IActionResult CustomActionResult<T>(ServiceResult<T> serviceResult)
    {
        //bir servis cevabı dönme
        if(serviceResult.StatusCode == HttpStatusCode.NoContent)
            return NoContent();


        //url ve servis cevaı ile dön.
        if (serviceResult.StatusCode == HttpStatusCode.Created)
            return Created(serviceResult.UrlAsCreated, serviceResult);


        return new ObjectResult(serviceResult)
        {
            StatusCode = serviceResult.StatusCode.GetHashCode()
        };

    }


    [NonAction] //end point olmasını istemiyorum.
    public IActionResult CustomActionResult(ServiceResult result)
    {
        //TODO: burada noContent dene.
        if (result.Status == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null)
            {
                StatusCode = result.Status.GetHashCode()
            };
        }

        return new ObjectResult(result)
        {
            StatusCode = result.Status.GetHashCode()
        };

    }
}