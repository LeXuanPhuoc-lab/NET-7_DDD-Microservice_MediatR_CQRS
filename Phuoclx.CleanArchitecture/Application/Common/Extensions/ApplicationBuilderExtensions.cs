using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x => {
                x.Run(async context => {
                    var errorFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeatures?.Error;

                    if (!(exception is ValidationException validationException))
                        throw exception!;

                    //var errors = validationException.Errors.Select(err => new {
                    //    err.PropertyName,
                    //    err.ErrorMessage
                    //});

                    var result = validationException.Errors.ToProblemDetails();

                    var errorText = JsonSerializer.Serialize(result);
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorText, UTF8Encoding.UTF8);
                });
            });
        }
    }
}
