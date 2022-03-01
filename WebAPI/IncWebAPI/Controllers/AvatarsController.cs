using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataMediator;
using WebAPI.DataMediator.IRepos;
using WebAPI.Events;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvatarsController : ControllerBase
    {
        private readonly IAvatarRepository avatarRepo;

        public AvatarsController(IAvatarRepository avatarRepository)
        {
            avatarRepo = avatarRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Avatar>> GetAvatarAsync([FromQuery] string owner)
        {
            return Ok(await avatarRepo.GetAvatarAsync(owner));
        }

        [HttpPost]
        [Route("{owner}")]
        public async Task<ActionResult<Avatar>> CreateAvatarAsync([FromBody] Avatar newAvatar)
        {
            try
            {
                if (newAvatar.AvatarName==null || newAvatar.AvatarName.Length > 16)
                {
                    return StatusCode(400);
                }
                await avatarRepo.CreateAvatarAsync(newAvatar);
                return Created($"/{newAvatar.Owner}", newAvatar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{owner}")]
        public async Task<ActionResult<Avatar>> PatchAvatarAsync([FromBody] Avatar avatar, [FromQuery] string key)
        {
            try
            {
                if(key != "Hug")
                {
                    RandomEvent.GetInstance().FeelingLuckyPunk(avatar, 1, avatarRepo);
                    if (avatar.EventDescription != null) return Ok(avatar);
                }
                avatar.Actions[key].Invoke();
                return Ok(await avatarRepo.UpdateAvatarAsync(avatar));
            }
            catch (Exception e) {
                string cause = e.Message;
                if (avatar.EventDescription != null) {
                    cause += $"Cause of death: {avatar.EventDescription}";
                }
                return StatusCode(500, cause);
            }
        }

        [HttpDelete]
        [Route("{owner}")]
        public async Task<ActionResult> DeleteAvatarAsync([FromRoute] string owner)
        {
            try
            {
                await avatarRepo.DeleteAvatarAsync(owner);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}