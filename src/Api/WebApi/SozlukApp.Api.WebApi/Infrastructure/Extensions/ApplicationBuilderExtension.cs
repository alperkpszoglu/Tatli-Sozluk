using Microsoft.AspNetCore.Diagnostics;
using SozlukApp.Common.Infrastructure.Exceptions;
using SozlukApp.Common.Infrastructure.Results;
using System.Net;

namespace SozlukApp.Api.WebApi.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
            bool includeExceptionDetails = false,
            bool useDefaultHandlingResponse = true,
            Func<HttpContext, Exception, Task> handleException = null)
        {
            app.UseExceptionHandler(opt =>
            {
                opt.Run(context =>
                {
                    var exceptionObj = context.Features.Get<IExceptionHandlerFeature>();

                    if (!useDefaultHandlingResponse && handleException == null)
                        throw new ArgumentException("handleException cannot be null when useDefaultHandlingResponse is false");

                    if (!useDefaultHandlingResponse && handleException != null)
                        return handleException(context, exceptionObj.Error);

                    return DefaultHandleException(context, exceptionObj.Error, includeExceptionDetails);
                });
            });



            return app;
        }

        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeExceptionDetails)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "Internal server error occured.";

            if (exception is UnauthorizedAccessException)
                status = HttpStatusCode.Unauthorized;
            if (exception is DbValidationException)
            {
                status = HttpStatusCode.BadRequest;
                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteResponse(context, status, validationResponse);
                return;
            }


            var res = new
            {
                HttpStatusCode = (int)status,
                Detail = includeExceptionDetails ? exception.ToString() : message,
            };

            await WriteResponse(context, status, res);
        }

        private static async Task WriteResponse(HttpContext context, HttpStatusCode httpStatus, object responseObject)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatus;

            await context.Response.WriteAsJsonAsync(responseObject);
        }
    }
}
