namespace WebApplication2.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApplication2.Models;

    public interface ICosmosDbService
    {
        Task<IEnumerable<Skill>> GetItemsAsync(string query);
        Task<Skill> GetItemAsync(string id);
        Task AddItemAsync(Skill item);
        Task UpdateItemAsync(string id, Skill item);
        Task DeleteItemAsync(string id);
    }
}
