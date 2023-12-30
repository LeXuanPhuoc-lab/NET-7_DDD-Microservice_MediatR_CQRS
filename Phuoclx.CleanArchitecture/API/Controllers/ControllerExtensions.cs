namespace API.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult ToResponse<TResult, TResponse>(this Result<TResult> result,
            Func<TResult, TResponse> mapper)
        {
            return result.Match<IActionResult>(obj => {
                if (obj is null) return new StatusCodeResult(500);

                //// Not cause exception 
                var response = mapper(obj);
                if (obj!.GetType() == typeof(BaseResponse))
                {
                    var res = obj as BaseResponse;

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
                    }
                }

                return new OkObjectResult(response);
            }, exception => {
                // Cause exception 
                // Catch validation exception
                if(exception is ValidationException validationException)
                    return new BadRequestObjectResult(validationException.ToProblemDetails());
                return new StatusCodeResult(500);
            });
        }
    }
}
