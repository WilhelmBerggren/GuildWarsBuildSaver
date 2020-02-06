using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWarsBuildSaver.Models;

namespace GuildWarsBuildSaver.Services
{
    public interface ISkillService
    {   
        Task AddSkill(Skill skill);

        Task DeleteSkill(string id);

        Task<Skill> GetSkill(string id);

        Task<IEnumerable<Skill>> GetSkills();

        Task UpdateSkill(string id, Skill skill);

        Task<IEnumerable<Skill>> GetSkillsByProfession(string profession);

        Skill GetItemFromList(List<Skill> list, string id);
        
        IEnumerable<Skill> FilterList(List<Skill> list, string requestedFilter);
    }
}
