using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
using System.Reflection;

namespace PM3Events.Api.Extensions
{
    internal class RequestMethodMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Type? _functionTargetType;

        public RequestMethodMiddleware(RequestDelegate next, Type? functionTargetType = null)
        {
            _next = next;
            _functionTargetType = functionTargetType;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_functionTargetType is null)
            {
                await _next(context);
                return;
            }

            var handleMethodInfo = _functionTargetType.GetMethod("HandleAsync");
            var httpMethodAttrs = handleMethodInfo.GetCustomAttributes<HttpMethodAttribute>().ToList();
            if (httpMethodAttrs.Count == 0)
            {
                await _next(context);
                return;
            }

            var httpMethods = httpMethodAttrs.SelectMany(a => a.HttpMethods).ToList();
            var requestMethod = context.Request.Method;
            if (httpMethods.Contains(requestMethod))
            {
                await _next(context);
                return;
            }

            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync($"'{requestMethod} {context.Request.Host}{context.Request.Path}' not found!");
        }
    }
}
