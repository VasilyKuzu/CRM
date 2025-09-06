using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO.Request.Category;
using CRM.API.DTO.Response.Category;


namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();

            var dtos = categories.Select(p => new CategoryReadDto
            {
                ID = p.ID,
                Name = p.Name
            }
            ).ToList();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.ID == id);
            if (category == null) return NotFound();

            var dto = new CategoryReadDto
            {
                ID = category.ID,
                Name = category.Name
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto createCategory)
        {
            if (createCategory == null) return BadRequest("Данные категории не переданы");

            var category = new Category
            {
                Name = createCategory.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();


            var dto = new CategoryReadDto
            {
                ID = category.ID,
                Name = category.Name
            };

            return CreatedAtAction(nameof(GetById), new {id = category.ID }, dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryUpdateDto updateCategory)
        {
            var findedCategory = await _context.Categories.FirstOrDefaultAsync(p => p.ID == id);
            if (findedCategory == null) return NotFound();

            findedCategory.Name = updateCategory.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.ID == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}
