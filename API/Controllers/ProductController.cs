using Microsoft.AspNetCore.Mvc;
using CRM.Core.Entities;
using CRM.DataAccess;
using Microsoft.EntityFrameworkCore;
using CRM.API.DTO;

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
                CategoryName = product.Category?.CategoryName,
                BrandName = product.Brand?.BrandName
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = product.ProductID}, product);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, Product product)
        {
            var findedProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null) return NotFound();

            findedProduct.ProductName = product.ProductName;
            findedProduct.ProductArticle = product.ProductArticle;
            findedProduct.CategoryID = product.CategoryID;
            findedProduct.BrandID = product.BrandID;

            await _context.SaveChangesAsync();
            return Ok(findedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return Ok(product);

        }

    }
}
