using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Routing;
using PM3Events.Api.Extensions;
using System;
using System.Linq;
using System.Net;

namespace PM3Events.Api
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(WebHostBuilderContext builderContext, IApplicationBuilder app)
        {
            var functionTarget = builderContext.Configuration.GetSection("FunctionsFramework")?.GetSection("FunctionTarget")?.Value;
            app.UseRequestMethodMiddleware(functionTarget);
        }
    }
}
