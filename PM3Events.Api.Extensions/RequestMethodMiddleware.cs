using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
using System.Reflection;

namespace PM3Events.Api.Extensions
{
    internal class RequestMethodMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _functionTargetName;

        public RequestMethodMiddleware(RequestDelegate next, string functionTargetName = "")
        {
            _next = next;
            _functionTargetName = functionTargetName;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (string.IsNullOrEmpty(_functionTargetName))
            {
                await _next(context);
                return;
            }

            var functionTarget = Type.GetType(_functionTargetName);
            if (functionTarget is null)
            {
                await _next(context);
                return;
            }

            var handleMethodInfo = functionTarget.GetMethod("HandleAsync");
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
