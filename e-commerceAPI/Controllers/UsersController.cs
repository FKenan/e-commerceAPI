using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound();

            return Ok(user);
        }

        // GET: api/users/email/test@example.com
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();

            var existing = await _userRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _userRepository.UpdateAsync(updatedUser);
            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
