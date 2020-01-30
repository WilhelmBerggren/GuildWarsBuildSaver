using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Services
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
