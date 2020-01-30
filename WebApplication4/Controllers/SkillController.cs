using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Services;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;

        public SkillController(SkillService skillService)
        {
            _skillService = skillService;
        }

        // GET: api/Skill
        [HttpGet]
        public async Task<IEnumerable<Skill>> Get()
        {
            Console.WriteLine(_skillService);

            return await _skillService.GetItemsAsync("select * from c");
        }

        // GET: api/Skill/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Skill>Get(string id)
        {
            return await _skillService.GetItemAsync(id);

            //return "value";
        }

        // POST: api/Skill
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Skill/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
