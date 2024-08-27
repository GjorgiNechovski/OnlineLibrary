using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using OnlineLibrary.Domain.Exceptions;
using System.Net;

namespace OnlineLibrary.Web
{
    public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                return RenderViewAsync(context, "NotFound");
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return Task.CompletedTask;
        }

        private Task RenderViewAsync(HttpContext context, string viewName)
        {
            var viewResult = new ViewResult
            {
                ViewName = viewName
            };

            var actionContext = new ActionContext(
                context,
                context.GetRouteData(),
                new ActionDescriptor()
            );

            return viewResult.ExecuteResultAsync(actionContext);
        }
    }
}
