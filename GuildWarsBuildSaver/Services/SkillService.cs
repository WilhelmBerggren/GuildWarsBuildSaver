using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWarsBuildSaver.Models;
using System;
using Newtonsoft.Json;
using System.IO;

namespace GuildWarsBuildSaver.Services
{
    public class SkillService : ISkillService
    {
        private readonly Container _container;
        private readonly IDatabaseSettings settings;
        private readonly CosmosClient _client;

        public SkillService(IDatabaseSettings settings)
        {
            this.settings = settings;
            var clientBuilder = new CosmosClientBuilder(settings.Account, settings.Key);
            _client = clientBuilder.WithConnectionModeDirect().Build();
            this._container = _client.GetContainer(settings.DatabaseName, settings.SkillContainerName);
        }

        public async void EnsureCreated()
        {
            if(settings.RebuildDB.Equals("True"))
            {
                await RebuildDb(_client);
            }
        }

        public async Task AddSkill(Skill skill)
        {
            await this._container.CreateItemAsync<Skill>(skill, new PartitionKey("id"));
        }

        public async Task DeleteSkill(string name)
        {
            await this._container.DeleteItemAsync<Skill>(name, new PartitionKey("id"));
        }

        public async Task<Skill> GetSkill(string id)
        {
            try
            {
                ItemResponse<Skill> response = await _container.ReadItemAsync<Skill>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Skill>> GetSkills()
        {
            var query = this._container.GetItemQueryIterator<Skill>(new QueryDefinition($"select * from {settings.SkillContainerName}"));
            List<Skill> results = new List<Skill>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        private async Task<IEnumerable<Skill>> GetSkills(string queryString)
        {
            try
            {
                var query = this._container.GetItemQueryIterator<Skill>(new QueryDefinition(queryString));
                List<Skill> results = new List<Skill>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();

                    results.AddRange(response.ToList());

                }
                return results;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Could not find skills using query: {queryString}");
                return null;
            }
        }

        public async Task UpdateSkill(string id, Skill skill)
        {
            await this._container.UpsertItemAsync<Skill>(skill, new PartitionKey("id"));
        }

        public async Task<IEnumerable<Skill>> GetSkillsByProfession(string profession)
        {
            var queryText = $"SELECT * FROM {settings.SkillContainerName} s WHERE s.professions = ['{profession}']";
            return await GetSkills(queryText);
        }

        public Skill GetItemFromList(List<Skill> list, string id)
        {
            var query = from item in list
                        where item.Id == id
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

        private async Task RebuildDb(CosmosClient cosmosClient)
        {
            //If DB exists remove it before proceeding
            try
            {
                await cosmosClient.GetDatabase(settings.DatabaseName).DeleteAsync();
                Console.WriteLine("Replacing existing DB");
            }
            catch
            {
                Console.WriteLine("Creating DB...");
            }

            //Create DB and reference to DB
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(settings.DatabaseName);
            Console.WriteLine("Created Database: {0}\n", database.Id);

            //Create Container and reference to DB
            Container container = await database.CreateContainerIfNotExistsAsync("SkillContainer", "/id");

            Console.WriteLine($"Created Container: {container.Id}");

            string filename = Path.Combine(Environment.CurrentDirectory, "response.json");

            List<Skill> skills;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = File.OpenText(filename))
            {
                skills = (List<Skill>)serializer.Deserialize(file, typeof(List<Skill>));
            }

            //Only insert skills belonging to a single profession
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