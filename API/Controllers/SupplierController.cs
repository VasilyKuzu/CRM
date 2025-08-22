using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly AppDbContext _context;
        public SupplierController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAll()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetById(int id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(p => p.SupplierID == id);
            if (supplier == null) return NotFound();
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = supplier.SupplierID}, supplier);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Supplier>> Update(int id, Supplier supplier)
        {
            var findedSupplier = await _context.Suppliers.FirstOrDefaultAsync(p => p.SupplierID == id);
            if (supplier == null) return NotFound();

            findedSupplier.SupplierName = supplier.SupplierName;
            findedSupplier.Phone = supplier.Phone;
            findedSupplier.Email = supplier.Email;


            await _context.SaveChangesAsync();
            return Ok(findedSupplier);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Supplier>> Delete(int id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(p => p.SupplierID == id);
            if (supplier == null) return NotFound();
            _context.Suppliers.Remove(supplier);

            await _context.SaveChangesAsync();
            return Ok(supplier);
        }

    }
}
