using Google;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PM3Events.Api.Extensions;
using PM3Events.Api.Core;
using PM3Events.Api.Core.Utilities;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PM3Events.Api
{
    [FunctionsStartup(typeof(PM3EventsApiStartup))]
    public class GetEventById : IHttpFunction
    {
        private readonly ILogger _logger;

        public GetEventById(ILogger<GetEventById> logger) => _logger = logger;

        /// <summary>
        /// Logic for your function goes here.
        /// </summary>
        /// <param name="context">The HTTP context, containing the request and the response.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [HttpGet]
        public async Task HandleAsync(HttpContext context)
        {
            try
            {
                var request = context.Request;
                var eventId = ((string)request.Query["id"]) ?? await request.GetParameterValueInBodyAsync<string>("id");

                if (string.IsNullOrEmpty(eventId))
                {
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("Bad Request: 'id' parameter is missing.", context.RequestAborted);
                    return;
                }

                var serviceAccountCredential = await ServiceAccountAuthentication.GetCredentialAsync();
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    ApplicationName = ProjectProperties.ProjectId,
                    HttpClientInitializer = serviceAccountCredential
                });

                var requestEvent = service.Events.Get(ProjectProperties.CalendarId, eventId);

                var evt = requestEvent.Execute();

                await context.Response.WriteAsJsonAsync(evt, context.RequestAborted);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error parsing JSON request");
                await context.Response.WriteAsync($"An Exception on parsing Request occured:\n{jsonEx.Message}", context.RequestAborted);
            }
            catch (GoogleApiException ex)
            {
                await context.Response.WriteAsync($"An Exception occured:\n{ex.Message}", context.RequestAborted);
            }
        }
    }
}
