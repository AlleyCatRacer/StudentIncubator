using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataMediator.IRepos;
using WebAPI.Model;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class FavoriteController : ControllerBase {

        private readonly IFavoriteRepository favoriteRepo;

        public FavoriteController(IFavoriteRepository favoriteRepository) {
            favoriteRepo = favoriteRepository;
        }
        
        [HttpPost]
        public async Task<ActionResult> AddFavorite([FromBody] string[] favorite) {
            
            if (ModelState.IsValid) {
                try {
                    await favoriteRepo.AddFavoriteAsync(favorite);
                    return Ok();
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    return StatusCode(500, e.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{username}")]
        public async Task<ActionResult> RemoveFavorite([FromRoute] string username, [FromQuery] string favorite) {
            try {
                string[] values = {username, favorite};
                await favoriteRepo.RemoveFavoriteAsync(values);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<string[]>> GetFavorites([FromRoute] string username) 
        {
            try
            {
                return Ok(await favoriteRepo.FindFavoritesAsync(username));
            }
            catch (NullReferenceException n)
            {
                return StatusCode(404, n.Message);
            }
        }
    }
}