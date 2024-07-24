using Newtonsoft.Json;
using PM3Events.Core.Utilities;

namespace PM3Events.Core
{
    public class Event
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// ETag of the resource.
        /// </summary>
        [JsonProperty("etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Description of the event. Can contain HTML. Optional.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Title of the event.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// DateTimeOffset representation of CreatedRaw.
        /// </summary>
        public DateTimeOffset? CreatedDateTimeOffset { get; set; }

        /// <summary>
        /// The (inclusive) start time of the event. For a recurring event, this is the start time of the first instance.
        /// </summary>
        [JsonProperty("start")]
        public EventDateTime Start { get; set; }

        /// <summary>
        /// The (exclusive) end time of the event. For a recurring event, this is the end time of the first instance.
        /// </summary>
        [JsonProperty("end")]
        public EventDateTime End { get; set; }

        /// <summary>
        /// The creator of the event. Read-only.
        /// </summary>
        [JsonProperty("creator")]
        public Creator Creator { get; set; }


        /// <summary>
        /// Geographic location of the event as free-form text. Optional.
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// List of RRULE, EXRULE, RDATE and EXDATE lines for a recurring event, as specified in RFC5545. 
        /// Note that DTSTART and DTEND lines are not allowed in this field; event start and end times are specified in the start and end fields. 
        /// This field is omitted for single events or instances of recurring events.
        /// </summary>
        [JsonProperty("recurrence")]
        public IList<string> Recurrence { get; set; } = new List<string>();

        [JsonIgnore]
        public DateTime StartDateTime => DateTimeRFC3339.Parse(Start.DateTimeRaw);

        [JsonIgnore]
        public DateTime EndDateTime => DateTimeRFC3339.Parse(End.DateTimeRaw);

    }
}
