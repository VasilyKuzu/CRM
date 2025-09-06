using CRM.Core.Entities.Enums;

namespace CRM.API.DTO.Request.Product
{
    public class ProductSupplierCreateDto
    {
        public int SupplierID { get; set; }
        public int ProductID { get; set; }
        public string? SupplierProductArticle { get; set; }
        public AvailabilityStatus Availability { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }

    }
}
