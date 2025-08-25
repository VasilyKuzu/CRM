using CRM.Core.Entities.Enums;

namespace CRM.API.DTO.Responce.Product
{
    public class ProductDetailDto
    {
        public string ProductName { get; set; }
        public string ProductArticle { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

        public List<SuppliersPriceDto> SuppliersPrices { get; set; } = new List<SuppliersPriceDto>();
    }

    public class SuppliersPriceDto
    {
        public string SupplierProductArticle { get; set; }
        public AvailabilityStatus Availability { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string SupplierName { get; set; }
    }
}
