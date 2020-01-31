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

        public SkillService(ISkillDatabaseSettings settings)
        {

            var clientBuilder = new CosmosClientBuilder(settings.Account, settings.Key);
            CosmosClient client = clientBuilder.WithConnectionModeDirect().Build();

            if (client.GetDatabase(settings.DatabaseName) == null)
            {
                RebuildDb(client);
            }

            this._container = client.GetContainer(settings.DatabaseName, settings.ContainerName);
        }

        public async Task AddItemAsync(Skill skill)
        {
            await this._container.CreateItemAsync<Skill>(skill, new PartitionKey("id"));
        }

        public async Task DeleteItemAsync(string name)
        {
            await this._container.DeleteItemAsync<Skill>(name, new PartitionKey("id"));
        }

        public async Task<Skill> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Skill> response = await this._container.ReadItemAsync<Skill>(id, new PartitionKey("id"));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                System.Console.WriteLine($"Did not find item: {id}");
                return null;
            }

        }

        public async Task<IEnumerable<Skill>> GetItemsAsync(string queryString)
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

        public async Task UpdateItemAsync(string id, Skill skill)
        {
            await this._container.UpsertItemAsync<Skill>(skill, new PartitionKey("id"));
        }

        public async Task<Skill> GetSkillFromDBAsync(string name, string id)
        {
            Skill skill = await _container.ReadItemAsync<Skill>(id, new PartitionKey("id"));
            return skill;
        }

        public async Task<List<Skill>> GetSkillsByProfession(string profession)
        {
            var queryText = $"SELECT * FROM SkillsContainer s WHERE s.professions = ['{profession}']";
            QueryDefinition query = new QueryDefinition(queryText);
            FeedIterator<Skill> feedIterator = _container.GetItemQueryIterator<Skill>(query);

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
                await cosmosClient.GetDatabase("SkillsDB").DeleteAsync();
                Console.WriteLine("Replacing existing DB");
            }
            catch
            {
                Console.WriteLine("Creating DB...");
            }

            //Create DB and reference to DB
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync("SkillsDB");
            Console.WriteLine("Created Database: {0}\n", database.Id);

            //Create Container and reference to DB
            Container container = await database.CreateContainerIfNotExistsAsync("SkillContainer", "/id");

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