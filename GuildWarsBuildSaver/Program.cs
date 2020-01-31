using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GuildWarsBuildSaver.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GuildWarsBuildSaver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //NilsMain(new string[0]);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static async Task NilsMain(string[] args)
        {
            CosmosClient cosmosClient = null;
            Database database = null;
            Container container = null;
            Container buildContainer = null;

            Console.WriteLine("Connect to Database or Reboot? \n" +
                "[1]: Connect to existing database. \n" +
                "[9]: Reboot database. \n");

            ConsoleKey menu = Console.ReadKey().Key;

            switch (menu)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Connecting to existing db. . .");
                    ConnectToDatabase(out cosmosClient, out database, out container);
                    break;
                case ConsoleKey.D9:
                    Console.WriteLine("Rebooting db. . .");
                    await RebootDB(cosmosClient, database, container);
                    break;
            }
        }

        // Methods for Connecting to existing or replacing existing DB

        private static void ConnectToDatabase(out CosmosClient cosmosClient, out Database database, out Container container)
        {
            cosmosClient = new CosmosClient("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            database = cosmosClient.GetDatabase("SkillsDB");
            container = database.GetContainer("SkillContainer");
        }

        private static async Task RebootDB(CosmosClient cosmosClient, Database database, Container container)
        {
            //Create Cosmos Client and connect to CosmosDB
            cosmosClient = new CosmosClient("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

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
            database = await cosmosClient.CreateDatabaseIfNotExistsAsync("SkillsDB");
            Console.WriteLine("Created Database: {0}\n", database.Id);

            //Create Container and reference to DB
            container = await database.CreateContainerIfNotExistsAsync("SkillContainer", "/type");
            Console.WriteLine($"Created Container: {container.Id}");

            var filename = @"C:\Users\wilhe\source\repos\GuildWarsBuildSaver\GuildWarsBuildSaver\response.json";

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
    }
}
