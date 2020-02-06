using GuildWarsBuildSaver.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuildWarsBuildSaver.Services
{
    public interface IBuildService
    {
        Task<ItemResponse<Build>> CreateBuild(Build build);

        Task DeleteBuild(string id);

        Task<ItemResponse<Build>> GetBuild(string name);

        Task<IEnumerable<Build>> GetBuilds();

        Task<ItemResponse<Build>> UpdateBuild(string id, Build build);

        Task<List<Build>> GetBuildByProfession(string profession);
    }
}
