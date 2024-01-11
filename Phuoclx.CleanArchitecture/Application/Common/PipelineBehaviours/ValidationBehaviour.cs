namespace Application.Common.PipelineBehaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        //where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
            => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            // pre
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(x => x.Validate(context))
                                      .SelectMany(x => x.Errors)
                                      .Where(x => x != null)
                                      //.Select(failure => new { 
                                      //  failure.PropertyName,
                                      //  failure.ErrorMessage
                                      //})
                                      .Distinct()
                                      .ToList();
            if (failures.Any())
                throw new FluentValidation.ValidationException(failures);
            return await next(); // logic  
            // post 
        }
    }
}

//public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//    where TResponse : Result<TRequest>
//{
//    private readonly IEnumerable<IValidator<TRequest>> _validators;

//    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
//        => _validators = validators;

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        if (!_validators.Any())
//            return await next();

//        // pre
//        var context = new ValidationContext<TRequest>(request);
//        var failures = _validators.Select(x => x.Validate(context))
//                                  .SelectMany(x => x.Errors)
//                                  .Where(x => x != null)
//                                  .Select(failure => new {
//                                      failure.PropertyName,
//                                      failure.ErrorMessage
//                                  })
//                                  .Distinct()
//                                  .ToList();

//        if (failures.Any())
//        {
//            // Handle validation failures
//            var validationException = new Result<TRequest>(new ValidationException(failures.ToString())); // Replace 'A' with the appropriate type
//            return (TResponse)(object)validationException; // Cast to TResponse
//        }

//        return await next(); // logic  
//        // post 
//    }
//}
