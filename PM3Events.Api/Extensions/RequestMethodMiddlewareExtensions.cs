

using Microsoft.AspNetCore.Builder;
using PM3Events.Api.Middleware;

namespace PM3Events.Api.Extensions
{
    internal static class RequestMethodMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMethodMiddleware(this IApplicationBuilder app, string functionTargetName)
        {
            return app.UseMiddleware<RequestMethodMiddleware>(functionTargetName);
        }
    }
}
