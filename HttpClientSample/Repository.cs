using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace HttpClientSample
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName(name:"description")]
        public string Description { get; set; }

        [JsonPropertyName(name:"html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [JsonPropertyName(name:"homepage")]
        public Uri Homepage { get; set; }

        [JsonPropertyName(name:"watchers")]
        public int Watchers { get; set; }

        [JsonPropertyName(name:"pushed_at")]
        public string JsonDate { get; set; }

        public DateTime LastPush =>
            DateTime.ParseExact(JsonDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
    }
}