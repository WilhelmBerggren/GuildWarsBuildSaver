using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWarsBuildSaver.Models;
using Microsoft.Azure.Cosmos;

namespace GuildWarsBuildSaver.Services
{
    public interface ISkillService
    {   
        Task AddItemAsync(Skill skill);

        Task DeleteItemAsync(string id);

        Task<Skill> GetItemAsync(string name);

        Task<IEnumerable<Skill>> GetItemsAsync(string queryString);

        Task UpdateItemAsync(string id, Skill skill);

        Task<Skill> GetSkillFromDBAsync(string name, string id);

        Task<List<Skill>> GetSkillsByProfession(string profession);

        Skill GetItemFromList(List<Skill> list, string id);
        
        IEnumerable<Skill> FilterList(List<Skill> list, string requestedFilter);
    }
}
