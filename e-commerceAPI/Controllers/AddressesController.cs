using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        // GET: api/addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address is null)
                return NotFound();

            return Ok(address);
        }

        // GET: api/addresses/user/3
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var addresses = await _addressRepository.GetAddressesByUserIdAsync(userId);
            return Ok(addresses);
        }

        // POST: api/addresses
        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            await _addressRepository.AddAsync(address);
            return CreatedAtAction(nameof(GetById), new { id = address.Id }, address);
        }

        // PUT: api/addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Address updatedAddress)
        {
            if (id != updatedAddress.Id)
                return BadRequest();

            var existing = await _addressRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _addressRepository.UpdateAsync(updatedAddress);
            return NoContent();
        }

        // DELETE: api/addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _addressRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            try
            {
                await _addressRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                // Check for foreign key violation (PostgreSQL error code 23503)
                if (ex.InnerException?.Message.Contains("violates foreign key constraint") == true)
                {
                    return Conflict(new
                    {
                        message = "Cannot delete address because it is referenced by one or more orders."
                    });
                }
                throw;
            }
        }
    }

}
