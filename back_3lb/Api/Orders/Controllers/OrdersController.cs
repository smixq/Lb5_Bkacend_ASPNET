using back_3lb.Api.Orders.Contracts;
using back_3lb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace back_3lb.Api.Orders.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Student)
            .Include(o => o.Product)
            .Select(s => new OrderResponseContract { Id = s.Id, ProductId = s.ProductId, StudentId = s.StudentId })
            .ToListAsync();
        return Ok(orders);
    }
    [HttpPost]
    public async Task<ActionResult> PostOrders([FromBody] OrderUpdateInsertStudentContract dto)
    {
        var order = new Order
        {
            StudentId = dto.StudentId,
            ProductId = dto.ProductId
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(PostOrder),
            new { Id = order.Id },
            new OrderResponseContract
            {
                Id = order.Id,
                ProductId = order.ProductId,
                StudentId = order.StudentId,
            }
            );
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> PostOrder([FromRoute] int id, [FromBody] OrderUpdateInsertStudentContract dto)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            return NotFound();
        }
        order.StudentId = dto.StudentId;
        order.ProductId = dto.ProductId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute] int id)
    {
        var deletedCnt = await _context.Orders.Where(o => o.Id == id).ExecuteDeleteAsync();
        if (deletedCnt == 0)
        {
            return NotFound();
        }
        return NoContent();
    }

}

