using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Skill
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("professions")]
        public List<string> Professions { get; set; }

        [JsonProperty("attunement")]
        public string Attunement { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("weapon_type")]
        public string WeaponType { get; set; }

        [JsonProperty("next_chain")]
        public int? NextChain { get; set; }

        [JsonProperty("prev_chain")]
        public int? PreviousChain { get; set; }

        [JsonProperty("flip_skill")]
        public int? FlipSkill { get; set; }

        [JsonProperty("chat_link")]
        public string ChatLink { get; set; }

        [JsonProperty("slot")]
        public string Slot { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("facts")]
        public List<Fact> Facts { get; set; }

        [JsonProperty("traited_facts")]
        public List<TraitedFact> TraitedFacts { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class Fact
    {
        string _value;

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("value")]
        public string Value
        {
            get => _value;
            set { _value = $"{value}"; }
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("distance")]
        public int? Distance { get; set; }

        [JsonProperty("hit_count")]
        public virtual int? HitCount { get; set; }

        [JsonProperty("dmg_multiplier")]
        public float? DamageMultiplier { get; set; }

        [JsonProperty("percent")]
        public float? Percent { get; set; }

        [JsonProperty("duration")]
        public float? Duration { get; set; }

        [JsonProperty("apply_count")]
        public int? ApplyCount { get; set; }

        [JsonProperty("field_type")]
        public string FieldType { get; set; }

        [JsonProperty("finisher_type")]
        public string ComboFinisher { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class TraitedFact : Fact
    {
        [JsonProperty("requires_trait")]
        public int? RequiresTrait { get; set; }

        [JsonProperty("overrides")]
        public int? Override { get; set; }
    }
}
