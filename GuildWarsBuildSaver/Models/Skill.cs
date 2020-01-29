using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GuildWarsBuildSaver.Models
{

    public class Skill
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Desc { get; set; }

        [JsonPropertyName("weapon_type")]
        public string WeaponType { get; set; }

        [JsonPropertyName("next_chain")]
        public int NextChain{ get; set; }

        [JsonPropertyName("prev_chain")]
        public int PreviousChain { get; set; }

        [JsonPropertyName("flip_skill")]
        public int FlipSkill { get; set; }

        [JsonPropertyName("chat_link")]
        public string ChatLink { get; set; }

        [JsonPropertyName("slot")]
        public string Slot { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        public class Profession
        {
            [JsonPropertyName("id")]
            public int ID { get; set; }

            [JsonPropertyName("profession")]
            public string Name { get; set; }
        }

        //[JsonPropertyName("professions")]
        //public List<Profession> Professions { get; set; }

        public class Fact
        {
            [JsonPropertyName("id")]
            public int ID { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("icon")]
            public string Icon { get; set; }

            [JsonPropertyName("value")]
            public string Value { get; set; }

            [JsonPropertyName("hit_count")]
            public int HitCount { get; set; }

            [JsonPropertyName("dmg_multiplier")]
            public float DmgMultiplier { get; set; }
        }

        public class TraitedFact : Fact
        {
            [JsonPropertyName("requires_trait")]
            public float RequiredTrait { get; set; }

            [JsonPropertyName("target")]
            public float Target { get; set; }
        }

        //[JsonPropertyName("facts")]
        //public List<Fact> Facts { get; set; }

        //[JsonPropertyName("traited_facts")]
        //public List<TraitedFact> TraitedFacts { get; set; }
    }
}

    
