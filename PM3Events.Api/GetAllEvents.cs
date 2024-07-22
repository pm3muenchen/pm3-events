using Google;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using PM3Events.Api.Authentication;
using PM3Events.Api.Utilities;
using System.Threading.Tasks;

namespace PM3Events.Api
{
    public class GetAllEvents : IHttpFunction
    {
        /// <summary>
        /// Logic for your function goes here.
        /// </summary>
        /// <param name="context">The HTTP context, containing the request and the response.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HandleAsync(HttpContext context)
        {
            try
            {
                var serviceAccountCredential = await ServiceAccountAuthentication.GetCredentialAsync();
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    ApplicationName = ProjectProperties.ProjectId,
                    HttpClientInitializer = serviceAccountCredential
                });

                var requestListEvents = service.Events.List(ProjectProperties.CalendarId);
                requestListEvents.TimeMin = DateTimeUtilities.GetFirstDateOfCurrentMonth();
                requestListEvents.TimeMax = DateTimeUtilities.GetDecemberLastDateOfCurrentYear();
                requestListEvents.ShowDeleted = false;
                requestListEvents.SingleEvents = true;
                requestListEvents.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                var events = ((IClientServiceRequest<Events>)requestListEvents).Execute();

                await context.Response.WriteAsJsonAsync(events);
            }
            catch (GoogleApiException ex)
            {
                await context.Response.WriteAsync($"An Exception occured:\n{ex.Message}");
            }
        }
    }
}
