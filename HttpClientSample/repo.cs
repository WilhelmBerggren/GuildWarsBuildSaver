using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace HttpClientSample
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [JsonPropertyName("homepage")]
        public Uri Homepage { get; set; }
        
        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }

        [JsonPropertyName("owner.id")]
        public int OwnerID { get; set; }

        [JsonPropertyName("pushed_at")]
        public string JsonDate { get; set; }

        public DateTime LastPush =>
            DateTime.ParseExact(JsonDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
    }
}
