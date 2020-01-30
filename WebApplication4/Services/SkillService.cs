using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public class SkillService: ISkillService
    {
        private Container _container;

        public SkillService(ISkillDatabaseSettings settings)
        {
            var clientBuilder = new CosmosClientBuilder(settings.Account, settings.Key);
            CosmosClient client = clientBuilder.WithConnectionModeDirect().Build();

            this._container = client.GetContainer(settings.DatabaseName, settings.ContainerName);
        }

        public async Task AddItemAsync(Skill skill)
        {
            await this._container.CreateItemAsync<Skill>(skill, new PartitionKey("name"));
        }

        public async Task DeleteItemAsync(string name)
        {
            await this._container.DeleteItemAsync<Skill>(name, new PartitionKey("name"));
        }

        public async Task<Skill> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Skill> response = await this._container.ReadItemAsync<Skill>(id, new PartitionKey("name"));
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
            await this._container.UpsertItemAsync<Skill>(skill, new PartitionKey("name"));
        }
    }
}
