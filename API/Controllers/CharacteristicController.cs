using CRM.API.DTO.Request.Product;
using CRM.API.DTO.Request.ProductCharacteristic;
using CRM.API.DTO.Response.ProductCharacteristic;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;


namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacteristicController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CharacteristicController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacteristicReadDto>>> GetAll()
        {
            var characteristics = await _context.Characteristics
                .Include(p => p.Category)
                .ToListAsync();

            var dtos = characteristics.Select(p => new CharacteristicReadDto
            {
                ID = p.ID,
                Name = p.Name,
                CategoryName = p.Category.Name
            }
            ).ToList();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacteristicReadDto>> GetById(int id)
        {
            var characteristic = await _context.Characteristics
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ID == id);
            if (characteristic == null) return NotFound();

            var dto = new CharacteristicReadDto
            {
                ID = characteristic.ID,
                Name = characteristic.Name,
                CategoryName = characteristic.Category.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CharacteristicReadDto>> Create(CharacteristicCreateDto createDto)
        {
            if (createDto == null) return BadRequest("Данные характеристик не переданы");

            var characteristic = new Characteristic
            {
                Name = createDto.Name,
                CategoryID = createDto.CategoryID
            };


            _context.Characteristics.Add(characteristic);
            await _context.SaveChangesAsync();

            var createdCharacteristic = await _context.Characteristics
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ID == characteristic.ID);
            if (createdCharacteristic == null) return NotFound();

            var dto = new CharacteristicReadDto
            {
                ID = createdCharacteristic.ID,
                Name = createdCharacteristic.Name,
                CategoryName = createdCharacteristic.Category.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = characteristic.ID }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CharacteristicUpdateDto updateDto)
        {
            var updateCharacteristic = await _context.Characteristics.FirstOrDefaultAsync(p => p.ID == id);
            if (updateCharacteristic == null) return NotFound();

            updateCharacteristic.Name = updateDto.Name;
            updateCharacteristic.CategoryID = updateDto.CategoryID;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var characteristic = await _context.Characteristics.FirstOrDefaultAsync(p => p.ID == id);
            if (characteristic == null) return NotFound();
            _context.Characteristics.Remove(characteristic);

            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
