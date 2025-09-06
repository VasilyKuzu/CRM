using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO.Request.Brand;
using CRM.API.DTO.Response.Brand;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandReadDto>>> GetAll()
        {
            var brands = await _context.Brands.ToListAsync();

            var dtos = brands.Select(p => new BrandReadDto
            {
                ID = p.ID,
                Name = p.Name
            }
            ).ToList();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandReadDto>> GetById(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(p => p.ID == id);
            if (brand == null) return NotFound();

            var dto = new BrandReadDto
            {
                ID = brand.ID,
                Name = brand.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<BrandReadDto>> Create(BrandCreateDto createBrand)
        {
            if (createBrand == null) return BadRequest("Данные бренда не переданы");

            var brand = new Brand
            {
                Name = createBrand.Name
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            var dto = new BrandReadDto
            {
                ID = brand.ID,
                Name = brand.Name
            };

            return CreatedAtAction(nameof(GetById), new {id = brand.ID }, dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BrandUpdateDto updateBrand)
        {
            var findedBrand = await _context.Brands.FirstOrDefaultAsync(p => p.ID == id);
            if (findedBrand == null) return NotFound();

            findedBrand.Name = updateBrand.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(p => p.ID == id);
            if (brand == null) return NotFound();
            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}
