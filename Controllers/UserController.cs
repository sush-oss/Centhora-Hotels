using Centhora_Hotels.Models.DTO;
using Centhora_Hotels.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Centhora_Hotels.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize]
        [Route("api/[controller]/getAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/addNewUser")]
        public async Task<ActionResult<PostUserDto>> AddNewUser([FromForm]PostUserDto user, IFormFile file)
        {
            var newUser = await _userRepository.AddNewUser(user, file);
            return Ok(newUser);

        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Authorize]
        [Route("api/[controller]/updateUser/{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UserDto user)
        {
            try
            {
                await _userRepository.UpdateUser(id, user);
                return Ok();

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Internal Server Error! Unable to update the user with id {id}", id);
                return StatusCode(500, ex.Message);
            }

        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        [Authorize]
        [Route("api/[controller]/removeUser/{id}")]
        public async Task<ActionResult> RemoveUser(int id)
        {
            await _userRepository.DeleteUser(id);
            return Ok();
        }

        
    }
}
