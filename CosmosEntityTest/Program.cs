using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Threading.Tasks;

namespace CosmosEntityTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var filename = @"C:\Users\wilhe\source\repos\GuildWarsBuildSaver\GuildWarsBuildSaver\response.json";
            //var jsonString = File.ReadAllText(filename);
            //var asdf = JsonConvert.DeserializeObject<List<Skill>>(jsonString);
            //Console.WriteLine(asdf);

            List<Skill> skills;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = File.OpenText(filename))
            {
                skills = (List<Skill>)serializer.Deserialize(file, typeof(List<Skill>));
                //Console.WriteLine(skills[1]);
            }
            //var skills = JsonSerializer.Deserialize<List<SkillJson>>(jsonString);
            //var asdf = skills.Select((i) => i).Where((i) => i.id == 1175);


            using (var context = new SkillContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                context.AddRange(skills.GetRange(1, 10));
                await context.SaveChangesAsync();

                var first = await context.Skills.FirstAsync();
                Console.WriteLine($"First: {first.Name}");
                Console.WriteLine(first);
                Console.WriteLine($"First: {first.Name}");
            }
        }
    }

    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Fact> Facts { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class Fact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FactId { get; set; }
        public string Text { get; set; }
    }

    public class SkillContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseCosmos(
            "https://localhost:8081",
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            databaseName: "SkillsDB");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Fact>(fact =>
            //{
            //    fact.HasNoKey();
            //});
        }
    }
}
