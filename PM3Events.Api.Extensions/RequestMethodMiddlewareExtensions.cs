using Microsoft.AspNetCore.Builder;

namespace PM3Events.Api.Extensions
{
    public static class RequestMethodMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMethodMiddleware(this IApplicationBuilder app, Type functionTargetType)
        {
            return app.UseMiddleware<RequestMethodMiddleware>(functionTargetType);
        }
    }
}
