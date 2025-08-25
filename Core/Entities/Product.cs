using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductArticle { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }

        public Category? Category { get; set; }
        public Brand? Brand { get; set; }


        public ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();
    }
}
