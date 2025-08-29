using CRM.Core.Entities.Enums;
using System.Text.Json.Serialization;

namespace CRM.Core.Entities
{
    public class ProductSupplier
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int SupplierID { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AvailabilityStatus Availability { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string SupplierProductArticle { get; set; }

        public Product? Product { get; set; }
        public Supplier? Supplier { get; set; }

    }
}