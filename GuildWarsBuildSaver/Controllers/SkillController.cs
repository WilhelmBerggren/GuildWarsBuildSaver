using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GuildWarsBuildSaver.Services;
using GuildWarsBuildSaver.Models;

namespace GuildWarsBuildSaver.Controllers
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
        public async Task<IEnumerable<Skill>> GetSkill()
        {
            return await _skillService.GetSkills();
        }

        // GET: api/Skill/Profession/thief
        [HttpGet("Profession/{profession}", Name = "GetSkillByProfession")]
        public async Task<IEnumerable<Skill>> GetSkill(string profession)
        {
            return await _skillService.GetSkillsByProfession(profession);
        }

        // GET: api/Skill/5
        [HttpGet("{id}", Name = "GetSkill")]
        public async Task<Skill> Get(string id)
        {
            return await _skillService.GetSkill(id);
        }
    }
}
