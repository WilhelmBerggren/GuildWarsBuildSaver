using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface ISkillService
    {   
        Task AddItemAsync(Skill skill);

        Task DeleteItemAsync(string id);

        Task<Skill> GetItemAsync(string id);

        Task<IEnumerable<Skill>> GetItemsAsync(string queryString);

        Task UpdateItemAsync(string id, Skill skill);
    }
}
