using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO.Request.Category;
using CRM.API.DTO.Responce.Category;


namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
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
                CategoryID = p.CategoryID,
                CategoryName = p.CategoryName
            }
            );

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryID == id);
            if (category == null) return NotFound();

            var dto = new CategoryReadDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto createCategory)
        {
            var category = new Category
            {
                CategoryName = createCategory.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();


            var dto = new CategoryReadDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };

            return CreatedAtAction(nameof(GetById), new {id = category.CategoryID }, dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryUpdateDto updateCategory)
        {
            var findedCategory = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryID == id);
            if (findedCategory == null) return NotFound();

            findedCategory.CategoryName = updateCategory.CategoryName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryID == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}
