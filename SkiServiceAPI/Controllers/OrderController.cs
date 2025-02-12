using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.Data;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OrderController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.ServiceOrder.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _context.ServiceOrder.FindAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(ServiceOrder order)
        {
            _context.ServiceOrder.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, ServiceOrder updatedOrder)
        {
            var order = await _context.ServiceOrder.FindAsync(id);
            if (order == null)
                return NotFound();

            order.CustomerName = updatedOrder.CustomerName;
            order.Email = updatedOrder.Email;
            order.Telefon = updatedOrder.Telefon;
            order.Prioritaet = updatedOrder.Prioritaet;
            order.Dienstleistung = updatedOrder.Dienstleistung;
            order.Status = updatedOrder.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.ServiceOrder.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.ServiceOrder.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
