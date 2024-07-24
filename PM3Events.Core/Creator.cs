
using Newtonsoft.Json;

namespace PM3Events.Core
{
    public class Creator
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The creator's email address, if available.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The creator's name, if available.
        /// </summary>
        [JsonProperty("displayName")]
        public virtual string DisplayName { get; set; } = string.Empty;
    }
}
