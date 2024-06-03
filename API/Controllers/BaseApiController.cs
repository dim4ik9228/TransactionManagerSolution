using Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController() : ControllerBase
{
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        return result.IsSuccess switch
        {
            true when result.Value is null => NotFound("Requested resource was not found."),
            true when result.Value is not null => Ok(result.Value),
            _ => BadRequest(CreateProblemDetails(
                "Bad request",
                BadRequest().StatusCode,
                result.Error!
            ))
        };
    }

    private static ProblemDetails CreateProblemDetails(
        string title, int status, Error error, Error[]? errors = null) => new()
    {
        Title = title,
        Status = status,
        Type = error.Code,
        Detail = error.Message,
        Extensions = { { nameof(errors), errors } }
    };
}