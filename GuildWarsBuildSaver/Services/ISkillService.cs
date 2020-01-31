using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWarsBuildSaver.Models;

namespace GuildWarsBuildSaver.Services
{
    public interface ISkillService
    {   
        Task AddItemAsync(Skill skill);

        Task DeleteItemAsync(string id);

        Task<Skill> GetItemAsync(string name);

        Task<IEnumerable<Skill>> GetItemsAsync(string queryString);

        Task UpdateItemAsync(string id, Skill skill);
    }
}
