using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    public static class ObjectExtension : object
    {
        public static BaseResponse ToSuccessBaseResponse(this object obj, string? message = null)
            => new BaseResponse()
            {
                StatusCode = StatusCodes.Status200OK,
                Message = message,
                Data = obj
            };
    }
}
