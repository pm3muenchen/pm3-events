using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PM3Events.Api.Extensions;

namespace PM3Events.Api
{
    public class PM3EventsApiStartup : FunctionsStartup
    {
        public override void Configure(WebHostBuilderContext builderContext, IApplicationBuilder app)
        {
            var functionTarget = builderContext.Configuration.GetSection("FunctionsFramework")?.GetSection("FunctionTarget")?.Value;
            app.UseRequestMethodMiddleware(functionTarget);
        }
    }
}
