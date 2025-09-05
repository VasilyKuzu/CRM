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
    public class CharacteristicValueController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CharacteristicValueController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacteristicValueReadDto>>> GetAll()
        {
            var characteristicValue = await _context.CharacteristicValues
                .Include(p => p.Product)
                .Include(p => p.Characteristic)
                .ToListAsync();

            var dtos = characteristicValue.Select(p => new CharacteristicValueReadDto
            {
                ID = p.ID,
                ProductName = p.Product.ProductName,
                FieldName = p.Characteristic.Name,
                Value = p.Value
            }
            ).ToList();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacteristicValueReadDto>> GetById(int id)
        {
            var characteristicValue = await _context.CharacteristicValues
                .Include(p => p.Product)
                .Include(p => p.Characteristic)
                .FirstOrDefaultAsync(p => p.ID == id);
            if (characteristicValue == null) return NotFound();

            var dto = new CharacteristicValueReadDto
            {
                ID = characteristicValue.ID,
                ProductName = characteristicValue.Product.ProductName,
                FieldName = characteristicValue.Characteristic.Name,
                Value = characteristicValue.Value
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CharacteristicValueReadDto>> Create(CharacteristicValueCreateDto createDto)
        {
            var characteristicValue = new CharacteristicValue
            {
                ProductID = createDto.ProductID,
                CharacteristicID = createDto.CharacteristicID,
                Value = createDto.Value
            };

            _context.CharacteristicValues.Add(characteristicValue);
            await _context.SaveChangesAsync();

            var createdCharacteristicValue = await _context.CharacteristicValues
                .Include(p => p.Product)
                .Include(p => p.Characteristic)
                .FirstOrDefaultAsync(p => p.ID == characteristicValue.ID);
            if (createdCharacteristicValue == null) return NotFound();

            var dto = new CharacteristicValueReadDto
            {
                ID = createdCharacteristicValue.ID,
                ProductName = createdCharacteristicValue.Product.ProductName,
                FieldName = createdCharacteristicValue.Characteristic.Name,
                Value = createdCharacteristicValue.Value
            };

            return CreatedAtAction(nameof(GetById), new { id = characteristicValue.ID }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CharacteristicValueUpdateDto updateDto)
        {
            var updateCharacteristicValue = await _context.CharacteristicValues.FirstOrDefaultAsync(p => p.ID == id);
            if (updateCharacteristicValue == null) return NotFound();

            updateCharacteristicValue.Value = updateDto.Value;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var characteristicValue = await _context.CharacteristicValues.FirstOrDefaultAsync(p => p.ID == id);
            if (characteristicValue == null) return NotFound();
            _context.CharacteristicValues.Remove(characteristicValue);

            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
