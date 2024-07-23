using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace PM3Events.Api.Core
{
    public static class ServiceAccountAuthentication
    {
        private static string SERVICE_ACCOUNT = "pm3eventsserviceaccount@pm3-events.iam.gserviceaccount.com";

        public static async Task<GoogleCredential> GetCredentialAsync()
        {
            var sourceCredential = await GoogleCredential.GetApplicationDefaultAsync();
            var delegates = new List<string>();

            var impersonatedCredential = sourceCredential.Impersonate(new ImpersonatedCredential.Initializer(SERVICE_ACCOUNT)
            {
                DelegateAccounts = delegates,
                Scopes = new[] { CalendarService.Scope.CalendarEventsReadonly },
                Lifetime = TimeSpan.FromHours(1)
            });

            return impersonatedCredential;
        }
    }
}
