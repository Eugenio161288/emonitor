using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Euromonitor.Server.Api.Middleware
{
    /// <summary>
    /// This middleware processes exceptions and provides HTTPS status code, response, logging (if needed etc.)
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the task.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles all exceptions asynchronously.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <param name="exception">Exception.</param>
        /// <returns>Returns server response and status code based on type of the exception</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            // Here based on Exception type code may be changed
            // ==========================================================================
            // if      (ex is MyNotFoundException)     code = HttpStatusCode.NotFound;
            // else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // etc.
            // ==========================================================================

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            // Logging...

            return context.Response.WriteAsync(result);
        }
    }
}
