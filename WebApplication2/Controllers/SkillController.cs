namespace WebApplication2.Controllers
{
    using System;
    using System.Threading.Tasks;
    using WebApplication2.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;

        public SkillController(SkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return (IActionResult) await _skillService.GetItemsAsync("Select * from c");
        }

        [HttpGet("{id}")]
        public async Task<Skill> Get(string id)
        {
            return await _skillService.GetItemAsync(id);
        }

        //[ActionName("Index")]
        //public async Task<IActionResult> Index()
        //{
        //    return await _skillService.GetItemsAsync("SELECT * FROM c"));
        //}

        //[ActionName("Create")]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<ActionResult> CreateAsync([Bind("Id,Name,Description,Completed")] Skill skill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        skill.Id = Guid.NewGuid().ToString();
        //        await _cosmosDbService.AddItemAsync(skill);
        //        return RedirectToAction("Index");
        //    }
        //
        //    return View(skill);
        //}

        //[HttpPost]
        //[ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditAsync([Bind("Id,Name,Description,Completed")] Skill skill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _cosmosDbService.UpdateItemAsync(skill.Id, skill);
        //        return RedirectToAction("Index");
        //    }
        //
        //    return View(skill);
        //}

        //[ActionName("Edit")]
        //public async Task<ActionResult> EditAsync(string id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //
        //    Skill skill = await _cosmosDbService.GetItemAsync(id);
        //    if (skill == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return View(skill);
        //}

        //[ActionName("Delete")]
        //public async Task<ActionResult> DeleteAsync(string id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //
        //    Skill skill = await _cosmosDbService.GetItemAsync(id);
        //    if (skill == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return View(skill);
        //}
        //
        //[HttpPost]
        //[ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        //{
        //    await _cosmosDbService.DeleteItemAsync(id);
        //    return RedirectToAction("Index");
        //}
        //
        //[ActionName("Details")]
        //public async Task<ActionResult> DetailsAsync(string id)
        //{
        //    return View(await _cosmosDbService.GetItemAsync(id));
        //}
    }
}