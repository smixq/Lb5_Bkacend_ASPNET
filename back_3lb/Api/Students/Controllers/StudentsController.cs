using back_3lb.Api.Students.Contracts;
using back_3lb.Data;
using back_3lb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace back_3lb.Api.Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetStudentsList()
        {

            var students = await _context.Students.ToArrayAsync();

            var response = students.Select(s => new StudentResponseContract
            {
                Id = s.Id,
                Name = s.Name,
                Specialization = s.Specialization,
                BirthDate = s.BirthDate

            });
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult> PostStudent([FromBody] StudentUpdateInsertContract dto)
        {

            Student student = new Student
            {
                Name = dto.Name,
                Specialization = dto.Specialization,
                BirthDate = dto.BirthDate
            };
            _context.Students.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
            return CreatedAtAction(
                nameof(PostStudent),
                new { id = student.Id },
                new StudentResponseContract
                {
                    Id = student.Id,
                    Name = student.Name,
                    Specialization = student.Specialization,
                    BirthDate = student.BirthDate
                }
            );

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutStudent([FromRoute] int id, [FromBody] StudentUpdateInsertContract dto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(o => o.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = dto.Name;
            student.Specialization = dto.Specialization;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteStudent([FromRoute] int id)
        {
            int deletedCnt = await _context.Students.Where(o => o.Id == id).ExecuteDeleteAsync();
            if (deletedCnt == 0)
            {
                return NotFound();
            }
            return NoContent();

        }
        [HttpGet("getByName/{name}")]
        public async Task<ActionResult> GetStudentsByName(string name)
        {
            var students = await _context.Students
                .Where(s => s.Name.ToLower() == name.ToLower())
                .Select(s => new StudentResponseContract
                {
                    Id = s.Id,
                    Name = s.Name,
                    Specialization = s.Specialization,
                    BirthDate = s.BirthDate
                })
                .ToArrayAsync();

            if (students.Length == 0)
                return NotFound();

            return Ok(students);
        }
        [HttpGet("getByYear/{year}")]
        public async Task<ActionResult> GetStudentByYear([FromRoute] int year)
        {
            var students = await _context.Students.Where(s => s.BirthDate != null && s.BirthDate.Value.Year == year).Select(s => new StudentResponseContract { Id = s.Id, Name = s.Name, Specialization = s.Specialization, BirthDate = s.BirthDate }).ToArrayAsync();
            if (students.Length == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetStudentById([FromRoute] int id)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Select(s => new StudentResponseContract
                {
                    Id = s.Id,
                    Name = s.Name,
                    Specialization = s.Specialization,
                    BirthDate = s.BirthDate
                })
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound();

            return Ok(student);
        }
        [HttpGet("bySlug/{slug:minlength(3)}")]
        public ActionResult GetBySlug(string slug)
        {
            return Ok($"Slug: {slug}");
        }
        [HttpGet("guid/{id:guid}")]
        public ActionResult GetByGuid(Guid id)
        {
            return Ok($"Guid: {id}");
        }
        [HttpGet("byDate/{date:datetime}")]
        public ActionResult GetByDate(DateTime date)
        {
            return Ok($"Date: {date}");
        }
        [HttpGet("optional/{id?}")]
        public ActionResult GetOptional(int? id)
        {
            if (id == null)
                return Ok("Id не передан");

            return Ok($"Id: {id}");
        }
        [HttpGet("page/{page:int=1}")]
        public ActionResult GetPage(int page)
        {
            return Ok($"Page: {page}");
        }
        [HttpGet("{id:int}/orders")]
        public async Task<ActionResult> GetStudentOrders(int id)
        {
            var orders = await _context.Orders
                .Where(o => o.StudentId == id)
                .Select(o => new
                {
                    o.Id,
                    o.ProductId
                })
                .ToListAsync();

            return Ok(orders);
        }
        [HttpGet("filter")]
        public async Task<ActionResult> GetStudentsFiltered(
        int page = 1,
        int pageSize = 10,
        string? sort = null)
        {
            var query = _context.Students.AsQueryable();

            if (sort == "name")
                query = query.OrderBy(s => s.Name);

            var students = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentResponseContract
                {
                    Id = s.Id,
                    Name = s.Name,
                    Specialization = s.Specialization,
                    BirthDate = s.BirthDate
                })
                .ToListAsync();

            return Ok(students);
        }
    }

}

