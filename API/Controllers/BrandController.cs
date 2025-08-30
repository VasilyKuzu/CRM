using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO.Request.Brand;
using CRM.API.DTO.Responce.Brand;

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
                BrandID = p.BrandID,
                BrandName = p.BrandName
            }
            );

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandReadDto>> GetById(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(p => p.BrandID == id);
            if (brand == null) return NotFound();

            var dto = new BrandReadDto
            {
                BrandID = brand.BrandID,
                BrandName = brand.BrandName
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<BrandReadDto>> Create(BrandCreateDto createBrand)
        {
            var brand = new Brand
            {
                BrandName = createBrand.BrandName
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            var dto = new BrandReadDto
            {
                BrandID = brand.BrandID,
                BrandName = brand.BrandName
            };

            return CreatedAtAction(nameof(GetById), new {id = brand.BrandID }, dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BrandUpdateDto updateBrand)
        {
            var findedBrand = await _context.Brands.FirstOrDefaultAsync(p => p.BrandID == id);
            if (findedBrand == null) return NotFound();

            findedBrand.BrandName = updateBrand.BrandName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> Delete(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(p => p.BrandID == id);
            if (brand == null) return NotFound();
            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}
