﻿using Newtonsoft.Json;

namespace PM3Events.Core
{
    public class EventDateTime
    {
        /// <summary>
        /// The ETag of the item.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// The date, in the format "yyyy-mm-dd", if this is an all-day event.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// The time zone in which the time is specified. (Formatted as an IANA Time Zone Database name, 
        /// e.g. "Europe/Zurich".) For recurring events this field is required and specifies the time zone in which the recurrence is expanded. 
        /// For single events this field is optional and indicates a custom time zone for the event start/end.
        /// </summary>
        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// The time, as a combined date-time value (formatted according to RFC3339). A time zone offset is required unless a time zone is explicitly specified in timeZone.
        /// </summary>
        [JsonProperty("dateTime")]
        public string DateTimeRaw { get; set; }

        /// <summary>
        /// DateTimeOffset representation of DateTimeRaw.
        /// </summary>
        public DateTimeOffset? DateTimeDateTimeOffset { get; set; }
    }
}
