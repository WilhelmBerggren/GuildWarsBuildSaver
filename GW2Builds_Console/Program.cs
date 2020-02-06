using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GW2Builds_Console
{
    public struct DatabaseSettings {
        public string Account;
        public string Key;
        public string DatabaseName;
        public string SkillContainerName;
        public string BuildContainerName;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DatabaseSettings
            {
                Account = "https://localhost:8081",
                Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                DatabaseName = "SkillsDB",
                SkillContainerName = "SkillContainer",
                BuildContainerName = "BuildContainer"
            };

            var CosmosService = new CosmosService(db);
        }
    }

    class CosmosService
    {
        DatabaseSettings settings;
        CosmosClient _client;
        Database _database;
        Container _skillContainer;
        Container _buildContainer;

        public CosmosService(DatabaseSettings settings)
        {
            this.settings = settings;
            var clientBuilder = new CosmosClientBuilder(settings.Account, settings.Key);
            _client = clientBuilder.WithConnectionModeDirect().Build();
            _database = _client.GetDatabase(settings.DatabaseName);

            _skillContainer = _database.GetContainer(settings.SkillContainerName);
            _buildContainer = _database.GetContainer(settings.BuildContainerName);
        }

        async void RebuildDb()
        {
            //If DB exists remove it before proceeding
            try
            {
                //await _client.GetDatabase(settings.DatabaseName).DeleteAsync();
                await _database.DeleteAsync();
                Console.WriteLine("Replacing existing DB");
            }
            catch
            {
                Console.WriteLine("Creating DB...");
            }

            //Create DB and reference to DB
            Console.WriteLine("Created Database: {0}\n", _database.Id);

            //Create Container and reference to DB
            Container container = await _database.CreateContainerIfNotExistsAsync("SkillContainer", "/id");

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
    class Skill { }
}
