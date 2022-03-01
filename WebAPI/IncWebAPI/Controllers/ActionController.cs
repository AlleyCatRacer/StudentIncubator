using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataMediator.IRepos;
using Action = WebAPI.Model.Action;

namespace WebAPI.Controllers {
    namespace WebAPI.Controllers {
        [ApiController]
        [Route("[controller]")]
        public class ActionController : ControllerBase {
            private readonly IActionRepository actionRepo;

            public ActionController(IActionRepository actionRepository) {

                actionRepo = actionRepository;
            }

            [HttpPost]
            public async Task<ActionResult> CreateAction([FromBody] Action suggestedAction) {
                if (ModelState.IsValid) {
                    try {
                        await actionRepo.CreateActionAsync(suggestedAction);
                        return Ok();
                    }
                    catch (Exception e) {
                        Console.WriteLine(e);
                        return StatusCode(500, e.Message);
                    }
                }

                return BadRequest(ModelState);
            }
        }
    }
}