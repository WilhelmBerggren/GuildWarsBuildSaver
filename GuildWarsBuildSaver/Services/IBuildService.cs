using GuildWarsBuildSaver.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuildWarsBuildSaver.Services
{
    public interface IBuildService
    {
        Task<Build> CreateBuild(Build build);

        Task DeleteBuild(string id);

        Task<Build> GetBuild(string name);

        Task<IEnumerable<Build>> GetBuilds();

        Task<Build> UpdateBuild(string id, Build build);

        Task<IEnumerable<Build>> GetBuildByProfession(string profession);
    }
}
