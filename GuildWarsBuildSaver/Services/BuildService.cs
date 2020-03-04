using GuildWarsBuildSaver.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GuildWarsBuildSaver.Services
{
    public class BuildService : IBuildService
    {
        private Container _container;
        private readonly IDatabaseSettings settings;
        private readonly CosmosClient _client;

        public BuildService(IDatabaseSettings settings)
        {
            var clientBuilder = new CosmosClientBuilder(settings.Account, settings.Key);
            this.settings = settings;

            _client = clientBuilder.WithConnectionModeDirect().Build();

            this._container = _client.GetContainer(settings.DatabaseName, settings.BuildContainerName);
        }

        public async void EnsureCreated()
        {
            try
            {
                Database database = await _client.CreateDatabaseIfNotExistsAsync(settings.DatabaseName);
                await database.CreateContainerIfNotExistsAsync(settings.BuildContainerName, "/id");
            }
            catch (HttpRequestException e)
            {
                System.Console.WriteLine("Could not connect to the database");
                throw e;
            }
        }

        public async Task<Build> CreateBuild(Build build)
        {
            if (ValidBuild(build))
                return await _container.CreateItemAsync<Build>(build);
            
            return null;
        }

        private bool ValidBuild(Build build)
        {
            if (build.Name == null || build.Id == null || build.Profession == null)
                return false;

            return true;
        }

        public Task DeleteBuild(string id)
        {
            return _container.DeleteItemAsync<Build>(id, new PartitionKey(id));
        }

        public async Task<Build> GetBuild(string id)
        {
            try
            {
                return await _container.ReadItemAsync<Build>(id, new PartitionKey(id));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Build>> GetBuildByProfession(string profession)
        {
            return await GetBuildsByQuery($"select * from {settings.BuildContainerName} c where c.profession = '{profession}'");
        }

        public async Task<IEnumerable<Build>> GetBuilds()
        {
            return await GetBuildsByQuery($"select * from {settings.BuildContainerName}");
        }

        private async Task<IEnumerable<Build>> GetBuildsByQuery(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Build>(new QueryDefinition(queryString));
            List<Build> results = new List<Build>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Build> UpdateBuild(string id, Build build)
        {
            return await _container.UpsertItemAsync<Build>(build);
        }
    }
}
