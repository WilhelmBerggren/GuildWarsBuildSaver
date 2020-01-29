using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class Skill
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("facts")]
        public List<Fact> Facts { get; set; }
        public override string ToString()
        {
            //return System.Text.Json.JsonSerializer.Serialize(this);
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public class Fact
        {
            private string _value;

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("value")]
            public string Value
            {
                //Workaround for "value" representing either integer or bool
                get => _value;
                set { _value = $"{value}"; }
            }
        }
    }
}
