﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWarsBuildSaver.Models;
using GuildWarsBuildSaver.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuildWarsBuildSaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildController : ControllerBase
    {
        private BuildService _buildService;

        public BuildController(BuildService buildService)
        {
            _buildService = buildService;
        }

        // GET: api/Build
        [HttpGet]
        public Task<IEnumerable<Build>> GetBuilds()
        {
            return _buildService.GetBuilds();
        }

        // GET: api/Build/5
        [HttpGet("{id}", Name = "GetBuild")]
        public async Task<Build> Get(string id)
        {
            return await _buildService.GetBuild(id);
        }

        // POST: api/Build
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Build build)
        {
            build.Id = build.Name;
            Console.WriteLine(build); //Det borde finnas en logger klass som ni kan använda istället
            var res = await _buildService.CreateBuild(build);
            if(res == null)
                return BadRequest();
            else
                return Ok();
        }

        // PUT: api/Build/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromForm] Build build)
        {
            var res = await _buildService.UpdateBuild(id, build);
            if (res == null)
                return BadRequest();
            else
                return Ok();
        }

        // DELETE: api/Build/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            await _buildService.DeleteBuild(id);
            return Ok();
        }
    }
}