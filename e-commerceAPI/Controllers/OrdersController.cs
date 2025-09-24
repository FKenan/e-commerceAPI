using Microsoft.AspNetCore.Mvc;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();
            return Ok(orders);
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order is null)
                return NotFound();

            return Ok(order);
        }

        // GET: api/orders/user/3
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderRepository.AddAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order updatedOrder)
        {
            if (id != updatedOrder.Id)
                return BadRequest();

            var existing = await _orderRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _orderRepository.UpdateAsync(updatedOrder);
            return NoContent();
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _orderRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
