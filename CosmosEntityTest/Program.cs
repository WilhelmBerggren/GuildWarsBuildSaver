using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Azure.Cosmos;

namespace CosmosEntityTest
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //Create Cosmos Client and connect to CosmosDB
            CosmosClient cosmosClient = new CosmosClient("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

            //If DB exists remove it before proceeding
            try
            {
                await cosmosClient.GetDatabase("SkillsDB").DeleteAsync();
                Console.WriteLine("Replacing existing DB");
            }
            catch
            {
                Console.WriteLine("Creating DB. . .");
            }

            //Create DB and reference to DB
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync("SkillsDB");
            Console.WriteLine("Created Database: {0}\n", database.Id);

            //Create Container and reference to DB
            Container container = await database.CreateContainerIfNotExistsAsync("SkillContainer", "/name");
            Console.WriteLine($"Created Container: {container.Id}");

            var filename = @"C:\Users\nilss\Documents\Programmering\GuildWarsBuildSaver\GuildWarsBuildSaver\response.json";

            List<Skill> skills;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = File.OpenText(filename))
            {
                skills = (List<Skill>)serializer.Deserialize(file, typeof(List<Skill>));
            }

            var query = from skill in skills
                        where skill.Professions != null && skill.Professions.Count == 1
                        select skill;

            foreach (var item in query)
            {
                try
                {
                    ItemResponse<Skill> itemResponse = await container.CreateItemAsync<Skill>(item);
                }
                catch
                {
                    Console.WriteLine("Bad Request: Item already exists: ");
                }
            }
        }

        public async Task<Skill> GetSkillFromDBAsync(CosmosClient cosmosClient, string name, string id)
        {
            Container container = cosmosClient.GetContainer("SkillsDB", "SkillsContainer");

            Skill skill = await container.ReadItemAsync<Skill>(id, new PartitionKey(name));
            return skill;
        }

        public async Task<List<Skill>> GetSkillsByProfession(CosmosClient cosmosClient, string profession)
        {
            Container container = cosmosClient.GetContainer("SkillsDB", "SkillsContainer");

            var queryText = $"SELECT * FROM SkillsContainer s WHERE s.professions = ['{profession}']";
            QueryDefinition query = new QueryDefinition(queryText);
            FeedIterator<Skill> feedIterator = container.GetItemQueryIterator<Skill>(query);

            var list = new List<Skill>();
            while (feedIterator.HasMoreResults)
            {
                FeedResponse<Skill> currentResultSet = await feedIterator.ReadNextAsync();
                list.AddRange(currentResultSet);
            }

            return list;
        }

        public Skill GetItemFromList(List<Skill> list, string id)
        {
            var query = from item in list
                        where item.Id == $"{id}"
                        select item;

            return query.FirstOrDefault();
        }

        public IEnumerable<Skill> FilterList(List<Skill> list, string requestedFilter)
        {
            var query = from item in list
                        where item.Type == $"{requestedFilter}"
                        select item;

            return query;
        }
    }

    public class Build
    {
        public string Profession { get; set; }
        public string MainHandWeapon { get; set; }
        public string OffHandWeapon { get; set; }
    }

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
