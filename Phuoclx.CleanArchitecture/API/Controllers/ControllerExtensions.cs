using System.Data;

namespace API.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult ToResponse<TResult, TResponse>(this Result<TResult> result,
            Func<TResult, TResponse> mapper)
        {
            return result.Match<IActionResult>(obj => {
                //// Not cause exception 
                //var response = mapper(obj);
                var response = mapper(obj);
                if (response!.GetType() == typeof(BaseResponse))
                {
                    var res = response as BaseResponse;

                    switch (res!.StatusCode)
                    {
                        case StatusCodes.Status200OK:
                            return new OkObjectResult(res);
                        case StatusCodes.Status400BadRequest:
                            return new BadRequestObjectResult(res);
                        case StatusCodes.Status401Unauthorized: 
                            return new UnauthorizedObjectResult(res);
                        case StatusCodes.Status403Forbidden:
                            return new ForbidResult();
                        case StatusCodes.Status404NotFound:
                            return new NotFoundObjectResult(res);
                        case StatusCodes.Status201Created:
                            return new CreatedResult("", res);
                        case StatusCodes.Status500InternalServerError:
                            return new StatusCodeResult(res.StatusCode);
                    }
                }

                return new OkObjectResult(response);
            }, exception => {
                var statusCode = StatusCodes.Status500InternalServerError;
                // Cause exception 
                // Catch validation exception
                if (exception is ValidationException validationException)
                    return new BadRequestObjectResult(validationException.ToProblemDetails());
                else if (exception is DuplicateNameException)
                    statusCode = StatusCodes.Status400BadRequest;

                return new ObjectResult(new BaseResponse{
                    StatusCode = statusCode,
                    Message = exception.Message,
                    Data = exception.Data
                }){ StatusCode = statusCode};
            });
        }
    }
}
