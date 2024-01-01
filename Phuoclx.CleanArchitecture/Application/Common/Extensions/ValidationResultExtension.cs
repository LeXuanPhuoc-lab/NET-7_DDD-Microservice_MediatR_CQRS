using LanguageExt.Common;

namespace Application.Common.Extensions
{
    public static class ValidationResultExtension
    {
        public static ValidationProblemDetails ToProblemDetails(this ValidationResult result) 
        {
            ValidationProblemDetails validationProblemDetail = new() 
            {
                Status = 400
            };

            // each error in ValidationResult.Errors is ValidationFailure
            // -> contain pair key, value (Property, ErrorMessage)
            foreach (ValidationFailure failure in result.Errors) 
            {
                // if error property is already exist
                if (validationProblemDetail.Errors.ContainsKey(failure.PropertyName))
                {
                    // append old error with new error
                    validationProblemDetail.Errors[failure.PropertyName] =
                        validationProblemDetail.Errors[failure.PropertyName]
                        .Concat(new[] { failure.ErrorMessage }).ToArray();
                }

                // add pair key, value if property not exist
                validationProblemDetail.Errors.Add(new KeyValuePair<string, string[]>(
                    failure.PropertyName,
                    new[] { failure.ErrorMessage}));
            }

            return validationProblemDetail;
        }
        public static ValidationProblemDetails ToProblemDetails(this IEnumerable<ValidationFailure> errors)
        {
            ValidationProblemDetails validationProblemDetail = new()
            {
                Status = 400
            };

            // each error in ValidationResult.Errors is ValidationFailure
            // -> contain pair key, value (Property, ErrorMessage)
            foreach (ValidationFailure failure in errors)
            {
                // if error property is already exist
                if (validationProblemDetail.Errors.ContainsKey(failure.PropertyName))
                {
                    // append old error with new error
                    validationProblemDetail.Errors[failure.PropertyName] =
                        validationProblemDetail.Errors[failure.PropertyName]
                        .Concat(new[] { failure.ErrorMessage }).ToArray();
                }

                // add pair key, value if property not exist
                validationProblemDetail.Errors.Add(new KeyValuePair<string, string[]>(
                    failure.PropertyName,
                    new[] { failure.ErrorMessage }));
            }

            return validationProblemDetail;
        }
    }
}
