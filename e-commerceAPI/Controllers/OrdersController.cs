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

            var orderDtos = orders.Select(order => new OrderDto
            {
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Status = order.Status,
                Address = order.Address,
                OrderItems = order.OrderItems
                    .Select(oi => new OrderItemDto
                    {
                        ProductName = oi.Product?.Name ?? string.Empty,
                        Quantity = oi.Quantity
                    })
                    .ToList()
            }).ToList();

            return Ok(orderDtos);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderRepository.AddAsync(order);

            var orderDto = new OrderDto
            {
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Status = order.Status,
                Address = order.Address,
                OrderItems = order.OrderItems
                    .Select(oi => new OrderItemDto
                    {
                        ProductName = oi.Product?.Name ?? string.Empty,
                        Quantity = oi.Quantity
                    })
                    .ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, orderDto);
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
