namespace WebApplication2.Controllers
{
    using System;
    using System.Threading.Tasks;
    using WebApplication2.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class SkillController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        public SkillController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,Description,Completed")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(skill);
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Name,Description,Completed")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateItemAsync(skill.Id, skill);
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Skill skill = await _cosmosDbService.GetItemAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Skill skill = await _cosmosDbService.GetItemAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetItemAsync(id));
        }
    }
}