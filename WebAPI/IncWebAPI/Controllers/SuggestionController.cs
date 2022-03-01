using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataMediator.IRepos;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : ControllerBase {
        private readonly ISuggestionRepository suggestionRepo;

        public SuggestionController(ISuggestionRepository suggestionRepository) {

            suggestionRepo = suggestionRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSuggestion([FromBody] string[] suggestion) {
            if (ModelState.IsValid) {
                try {
                    await suggestionRepo.CreateSuggestionAsync(suggestion);
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