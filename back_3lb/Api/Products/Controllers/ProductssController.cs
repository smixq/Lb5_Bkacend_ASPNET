using back_3lb.Api.Products.Contracts;
using back_3lb.Data;
using back_3lb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace back_3lb.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductssController : ControllerBase
    {
        readonly ApplicationDbContext _context;
        public ProductssController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _context.Products
                .Select(s => new ProductResponseContract
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price
                }).ToArrayAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> PostProducts([FromBody] ProductUpdateInsertContract dto)
        {
            Product product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(PostProducts),
                new { Id = product.Id },
                new ProductResponseContract
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                }
            );

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutProduct([FromRoute] int id, [FromBody] ProductUpdateInsertContract dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(o => o.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            var deletedCnt = await _context.Products.Where(o => o.Id == id).ExecuteDeleteAsync();
            if (deletedCnt == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
