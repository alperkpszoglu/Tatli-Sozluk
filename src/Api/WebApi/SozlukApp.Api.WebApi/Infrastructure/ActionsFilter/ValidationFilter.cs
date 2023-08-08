using Microsoft.AspNetCore.Mvc.Filters;

namespace SozlukApp.Api.WebApi.Infrastructure.ActionsFilter
{
    public class ValidationFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => !string.IsNullOrEmpty(x.ErrorMessage) ? x.ErrorMessage : x.Exception?.Message)
                    .Distinct().ToList();

                return;
            }

            next();
        }
    }
}
