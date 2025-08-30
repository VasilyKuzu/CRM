using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO.Request.Supplier;
using CRM.API.DTO.Responce.Supplier;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SupplierController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierReadDto>>> GetAll()
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            var dtos = suppliers.Select(p => new SupplierReadDto
            {
                SupplierID = p.SupplierID,
                SupplierName = p.SupplierName,
                Phone = p.Phone,
                Email = p.Email
            }
            );

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierReadDto>> GetById(int id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(p => p.SupplierID == id);
            if (supplier == null) return NotFound();

            var dto = new SupplierReadDto
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                Phone = supplier.Phone,
                Email = supplier.Email,
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierReadDto>> Create(SupplierCreateDto createDto)
        {
            var supplier = new Supplier
            {
                SupplierName = createDto.SupplierName,
                Phone = createDto.Phone,
                Email = createDto.Email
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            var dto = new SupplierReadDto
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                Phone = supplier.Phone,
                Email = supplier.Email,
            };

            return CreatedAtAction(nameof(GetById), new {id = supplier.SupplierID}, dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, SupplierUpdateDto updateSupplier)
        {
            var findedSupplier = await _context.Suppliers.FirstOrDefaultAsync(p => p.SupplierID == id);
            if (findedSupplier == null) return NotFound();

            findedSupplier.SupplierName = updateSupplier.SupplierName;
            findedSupplier.Phone = updateSupplier.Phone;
            findedSupplier.Email = updateSupplier.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Supplier>> Delete(int id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(p => p.SupplierID == id);
            if (supplier == null) return NotFound();
            _context.Suppliers.Remove(supplier);

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
