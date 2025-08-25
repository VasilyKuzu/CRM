using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO.Request.Product;
using CRM.API.DTO.Responce.Product;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
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
                ProductName = p.ProductName,
                ProductArticle = p.ProductArticle,
                Description = p.Description,
                CategoryName = p.Category?.CategoryName,
                BrandName = p.Brand?.BrandName
            }
            );

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null) return NotFound();

            var dto = new ProductReadDto
            {
                ProductName = product.ProductName,
                ProductArticle = product.ProductArticle,
                Description = product.Description,
                CategoryName = product.Category?.CategoryName,
                BrandName = product.Brand?.BrandName
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
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null) return NotFound();

            var dto = new ProductDetailDto
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductArticle = product.ProductArticle,
                CategoryName = product.Category.CategoryName,
                BrandName = product.Brand.BrandName,
                SuppliersPrices = product.ProductSuppliers.Select(ps => new SuppliersPriceDto
                {
                    SupplierProductArticle = ps.SupplierProductArticle,
                    Availability = ps.Availability,
                    PurchasePrice = ps.PurchasePrice,
                    RetailPrice = ps.RetailPrice,
                    SupplierName = ps.Supplier.SupplierName
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create(ProductCreateDto createDto)
        {
            var product = new Product
            {
                ProductName = createDto.ProductName,
                ProductArticle = createDto.ProductArticle,
                Description = createDto.Description,
                CategoryID = createDto.CategoryID,
                BrandID = createDto.BrandID
            };


            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var createdProduct = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

            var dto = new ProductReadDto
            {
                ProductName = createdProduct.ProductName,
                ProductArticle = createdProduct.ProductArticle,
                Description = createdProduct.Description,
                CategoryName = createdProduct.Category?.CategoryName,
                BrandName = createdProduct.Brand?.BrandName
            };

            return CreatedAtAction(nameof(GetById), new {id = product.ProductID}, dto);
        }

        [HttpPost("product-supplier")]
        public async Task<ActionResult<ProductDetailDto>> CreateProductSupplier([FromBody]ProductSupplierCreateDto createDto)
        {
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
            .FirstOrDefaultAsync(p => p.ProductID == productSupplier.ProductID);

            var dto = new ProductReadDto
            {
                ProductName = createdProductSupplier.ProductName,
                ProductArticle = createdProductSupplier.ProductArticle,
                Description = createdProductSupplier.Description,
                CategoryName = createdProductSupplier.Category?.CategoryName,
                BrandName = createdProductSupplier.Brand?.BrandName
            };

            return CreatedAtAction(nameof(GetById), new { id = productSupplier.ProductID }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductUpdateDto updateDto)
        {
            var updateProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
            if (updateProduct == null) return NotFound();

            updateProduct.ProductName = updateDto.ProductName;
            updateProduct.ProductArticle = updateDto.ProductArticle;
            updateProduct.Description = updateDto.Description;
            updateProduct.CategoryID = updateDto.CategoryID;
            updateProduct.BrandID = updateDto.BrandID;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return NoContent();

        }

    }
}
