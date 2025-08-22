using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var category = await _context.Categories.ToListAsync();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryID == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = category.CategoryID }, category);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Update(int id, Category category)
        {
            var findedCategory = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryID == id);
            if (category == null) return NotFound();

            findedCategory.CategoryName = category.CategoryName;

            await _context.SaveChangesAsync();
            return Ok(findedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryID == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
            return Ok(category);

        }

    }
}
