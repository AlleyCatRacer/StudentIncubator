using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataMediator;
using WebAPI.DataMediator.IRepos;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepo;

        public UsersController(IUserRepository userRepository)
        {
            userRepo = userRepository;
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<User>> ValidateUserAsync([FromRoute] string username,
            [FromQuery] string password)
        {
            try
            {
                var user = await userRepo.GetUserAsync(username);
                user.Online = true;
                if (user.Password.Equals(password))
                {
                    return Ok(user);
                }
                return StatusCode(409);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();            
            }
        }

        [HttpGet]
        [Route("{username}/Details")]
        public async Task<ActionResult<User>> GetPublicUserAsync([FromRoute] string username)
        {
            try
            {
                var user = await userRepo.GetPublicUserAsync(username);
                if (user != null)
                {
                    return Ok(user);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IList<User>>> GetAllUsersAsync()
        {
            try
            {
                return Ok(await userRepo.GetAllUsersAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User newUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                newUser.Online = true;
                await userRepo.SaveAsync(newUser);
                return Created($"/{newUser.Username}", newUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{username}")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            try
            {
                await userRepo.UpdateUserAsync(user);

                return Created($"/{user.Username}", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}