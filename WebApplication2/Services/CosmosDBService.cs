namespace WebApplication2.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplication2.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Skill skill)
        {
            await this._container.CreateItemAsync<Skill>(skill, new PartitionKey(skill.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Skill>(id, new PartitionKey(id));
        }

        public async Task<Skill> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Skill> response = await this._container.ReadItemAsync<Skill>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
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
            await this._container.UpsertItemAsync<Skill>(skill, new PartitionKey(id));
        }
    }
}