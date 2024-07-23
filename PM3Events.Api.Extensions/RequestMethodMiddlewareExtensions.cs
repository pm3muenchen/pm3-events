using Microsoft.AspNetCore.Builder;

namespace PM3Events.Api.Extensions
{
    public static class RequestMethodMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMethodMiddleware(this IApplicationBuilder app, string functionTargetName)
        {
            return app.UseMiddleware<RequestMethodMiddleware>(functionTargetName);
        }
    }
}
