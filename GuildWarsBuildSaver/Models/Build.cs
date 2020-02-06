using Newtonsoft.Json;

namespace GuildWarsBuildSaver.Models
{
    public class Build
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profession")]
        public string Profession { get; set; }

        [JsonProperty("mainHandWeapon")]
        public string MainHandWeapon { get; set; }

        [JsonProperty("offHandWeapon")]
        public string OffHandWeapon { get; set; }

        [JsonProperty("mainHandSwap")]
        public string MainHandSwap { get; set; }

        [JsonProperty("offHandSwap")]
        public string OffHandSwap { get; set; }

        [JsonProperty("heal")]
        public string Heal { get; set; }

        [JsonProperty("utility1")]
        public string Utility1 { get; set; }

        [JsonProperty("utility2")]
        public string Utility2 { get; set; }

        [JsonProperty("utility3")]
        public string Utility3 { get; set; }

        [JsonProperty("elite")]
        public string Elite { get; set; }
    }
}
