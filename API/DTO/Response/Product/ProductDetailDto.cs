using CRM.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.API.DTO.Response.Product
{
    public class ProductDetailDto : ProductReadDto
    {
        public List<SuppliersPriceDto> SuppliersPrices { get; set; } = new List<SuppliersPriceDto>();
    }

    public class SuppliersPriceDto
    {
        public string SupplierProductArticle { get; set; }
        public AvailabilityStatus Availability { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string SupplierName { get; set; }
        public decimal Markup => RetailPrice - PurchasePrice;
    }
}
