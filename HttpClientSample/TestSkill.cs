using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HttpClientSample
{
    class TestSkill
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public List<Fact> Facts {get;set;}

        [JsonPropertyName("description")]
        public string Desc { get; set; }

        [JsonPropertyName("weapon_type")]
        public string WeaponType { get; set; }

        [JsonPropertyName("professions")]
        public List<string> Professions { get; set; }
    }

    public class Fact
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("text")]
        public string Icon { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("hit_count")]
        public int HitCount { get; set; }

        [JsonPropertyName("dmg_multiplier")]
        public float DmgMultiplier { get; set; }
    }
}
