using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetAll()
        {
            var brands = await _context.Brands.ToListAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetById(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(p => p.BrandID == id);
            if (brand == null) return NotFound();
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> Create(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = brand.BrandID }, brand);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> Update(int id, Brand brand)
        {
            var findedBrand = await _context.Brands.FirstOrDefaultAsync(p => p.BrandID == id);
            if (brand == null) return NotFound();

            findedBrand.BrandName = brand.BrandName;

            await _context.SaveChangesAsync();
            return Ok(findedBrand);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> Delete(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(p => p.BrandID == id);
            if (brand == null) return NotFound();
            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();
            return Ok(brand);

        }

    }
}
