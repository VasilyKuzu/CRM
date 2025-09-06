using CRM.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.API.DTO.Response.Product
{
    public class ProductDetailDto : ProductReadDto
    {
        public List<SuppliersPriceDto> SuppliersPrices { get; set; } = new List<SuppliersPriceDto>();
    }

    public class SuppliersPriceDto
    {
        public string? SupplierProductArticle { get; set; }
        [Required]
        public required AvailabilityStatus Availability { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        [Required]
        public required string SupplierName { get; set; }
        public decimal? Markup => RetailPrice - PurchasePrice;
    }
}
