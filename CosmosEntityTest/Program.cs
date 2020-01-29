//using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CosmosEntityTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var filename = @"C:\Users\wilhe\source\repos\GuildWarsBuildSaver\GuildWarsBuildSaver\response.json";

            List<Skill> skills;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = File.OpenText(filename))
            {
                skills = (List<Skill>)serializer.Deserialize(file, typeof(List<Skill>));
            }

            CosmosClient cosmosClient = new CosmosClient("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

            var databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync("SkillsDB");

            await cosmosClient.GetDatabase("SkillsDB").CreateContainerIfNotExistsAsync("SkillsContainer", "/Skills");

            var database = databaseResponse.Database;

            var container = database.GetContainer("SkillsContainer");

            //var skill = skills[0];
            foreach(Skill skill in skills)
                await container.CreateItemAsync<Skill>(skill);

            //Console.WriteLine(response);



            //using (var context = new SkillContext())
            //{
            //    await context.Database.EnsureDeletedAsync();
            //    await context.Database.EnsureCreatedAsync();
            //    context.AddRange(skills.GetRange(1, 10));
            //    await context.SaveChangesAsync();
            //
            //    var first = await context.Skills.FirstAsync();
            //    Console.WriteLine($"First: {first.Name}");
            //    Console.WriteLine(first);
            //    Console.WriteLine($"First: {first.Name}");
            //}
        }
    }

    public class Skill
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Fact> Facts { get; set; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
            //return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class Fact
    {
        private string _value;

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string FactId { get; set; }
        public string Text { get; set; }

        public string Value
        {
            //Workaround for "value" representing either integer or bool
            get => _value;
            set { _value = $"{value}"; }
        }
    }

    public class ProfessionContainer
    {
    }

    //public class SkillContext : DbContext
    //{
    //    public DbSet<Skill> Skills { get; set; }
    //
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //=> optionsBuilder.UseCosmos(
    //        "https://localhost:8081",
    //        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
    //        databaseName: "SkillsDB");
    //}
}
