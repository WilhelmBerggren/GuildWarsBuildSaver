using GuildWarsBuildSaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuildWarsBuildSaver.Services
{
    interface IBuildService
    {
        Task AddItemAsync(Build build);

        Task DeleteItemAsync(string id);

        Task<Build> GetItemAsync(string name);

        Task<IEnumerable<Build>> GetItemsAsync(string queryString);

        Task UpdateItemAsync(string id, Build build);

        Task<Build> GetSkillFromDBAsync(string name, string id);

        Task<List<Build>> GetBuildByProfession(string profession);

        Build GetItemFromList(List<Build> list, string id);

        IEnumerable<Build> FilterList(List<Build> list, string requestedFilter);
    }
}
