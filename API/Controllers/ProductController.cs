using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO.Request.Product;
using CRM.API.DTO.Response.Product;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();

            var dtos = products.Select(p => new ProductReadDto
            {
                ID = p.ID,
                Name = p.Name,
                Article = p.Article,
                Description = p.Description,
                CategoryName = p.Category?.Name ?? "Не указано",
                BrandName = p.Brand?.Name ?? "Не указано"
            }
            ).ToList();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.ID == id);
            if (product == null) return NotFound();

            var dto = new ProductReadDto
            {
                ID = product.ID,
                Name = product.Name,
                Article = product.Article,
                Description = product.Description,
                CategoryName = product.Category?.Name ?? "Не указано",
                BrandName = product.Brand?.Name ?? "Не указано"
            };

            return Ok(dto);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<ProductDetailDto>> GetProductDetails(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSuppliers)
                    .ThenInclude(sp => sp.Supplier)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (product == null) return NotFound();

            var dto = new ProductDetailDto
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Article = product.Article,
                CategoryName = product.Category?.Name ?? "Не указано",
                BrandName = product.Brand?.Name ?? "Не указано",
                SuppliersPrices = product.ProductSuppliers.Select(ps => new SuppliersPriceDto
                {
                    SupplierProductArticle = ps.SupplierProductArticle,
                    Availability = ps.Availability,
                    PurchasePrice = ps.PurchasePrice,
                    RetailPrice = ps.RetailPrice,
                    SupplierName = ps.Supplier?.Name ?? "Не указано",
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create(ProductCreateDto createDto)
        {
            if (createDto == null) return BadRequest("Данные продукта не переданы");

            var product = new Product
            {
                Name = createDto.Name,
                Article = createDto.Article,
                Description = createDto.Description,
                CategoryID = createDto.CategoryID,
                BrandID = createDto.BrandID
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var createdProduct = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstAsync(p => p.ID == product.ID);

            var dto = new ProductReadDto
            {
                Name = createdProduct.Name,
                Article = createdProduct.Article,
                Description = createdProduct.Description,
                CategoryName = createdProduct.Category?.Name ?? "Не указано",
                BrandName = createdProduct.Brand?.Name ?? "Не указано"
            };

            return CreatedAtAction(nameof(GetById), new {id = product.ID}, dto);
        }

        [HttpPost("product-supplier")]
        public async Task<ActionResult<ProductDetailDto>> CreateProductSupplier([FromBody]ProductSupplierCreateDto createDto)
        {
            if (createDto == null) return BadRequest("Данные не переданы");

            var productSupplier = new ProductSupplier
            {
                SupplierID = createDto.SupplierID,
                ProductID = createDto.ProductID,
                SupplierProductArticle = createDto.SupplierProductArticle,
                Availability = createDto.Availability,
                PurchasePrice = createDto.PurchasePrice,
                RetailPrice = createDto.RetailPrice
            };

            _context.ProductSuppliers.Add(productSupplier);
            await _context.SaveChangesAsync();

            var createdProductSupplier = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstAsync(p => p.ID == productSupplier.ProductID);

            var dto = new ProductReadDto
            {
                Name = createdProductSupplier.Name,
                Article = createdProductSupplier.Article,
                Description = createdProductSupplier.Description,
                CategoryName = createdProductSupplier.Category?.Name ?? "Не указано",
                BrandName = createdProductSupplier.Brand?.Name ?? "Не указано"
            };

            return CreatedAtAction(nameof(GetById), new { id = productSupplier.ProductID }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductUpdateDto updateDto)
        {
            var updateProduct = await _context.Products.FirstOrDefaultAsync(p => p.ID == id);
            if (updateProduct == null) return NotFound();

            updateProduct.Name = updateDto.Name;
            updateProduct.Article = updateDto.Article;
            updateProduct.Description = updateDto.Description;
            updateProduct.CategoryID = updateDto.CategoryID;
            updateProduct.BrandID = updateDto.BrandID;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ID == id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}
