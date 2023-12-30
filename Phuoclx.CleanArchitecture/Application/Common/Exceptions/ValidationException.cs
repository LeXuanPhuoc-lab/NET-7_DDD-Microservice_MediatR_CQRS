using LanguageExt.Common;

namespace Application.Common.Exceptions
{
    //public class ValidationException : Exception
    //{
    //    public ValidationException()
    //        : base("One or more validation failure have occured.")
    //    {
    //        Errors = new Dictionary<string, string[]>();
    //    }

    //    public ValidationException(IEnumerable<ValidationFailure> failures)
    //    {
    //        Errors = failures
    //            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
    //            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    //    }
    //    public IDictionary<string, string[]> Errors { get; }
    //}

    public static class ValidationExceptionExtention
    {
        public static ValidationProblemDetails ToProblemDetails(this ValidationException exeception)
        {
            ValidationProblemDetails validationProblemDetail = new()
            {
                Status = 400
            };

            // each error in ValidationResult.Errors is ValidationFailure
            // -> contain pair key, value (Property, ErrorMessage)
            foreach (ValidationFailure failure in exeception.Errors)
            {
                // if error property is already exist
                if (validationProblemDetail.Errors.ContainsKey(failure.PropertyName))
                {
                    // append old error with new error
                    validationProblemDetail.Errors[failure.PropertyName] =
                        validationProblemDetail.Errors[failure.PropertyName]
                        .Concat(new[] { failure.ErrorMessage }).ToArray();
                }
                else
                {
                    // add pair key, value if property not exist
                    validationProblemDetail.Errors.Add(new KeyValuePair<string, string[]>(
                        failure.PropertyName,
                        new[] { failure.ErrorMessage }));
                }
            }

            return validationProblemDetail;
        }
    }

}
