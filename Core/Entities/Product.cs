using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Entities
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Article { get; set; }
        public string? Description { get; set; }
        [Required]
        public required int CategoryID { get; set; }
        [Required]
        public required int BrandID { get; set; }

        public Category? Category { get; set; }
        public Brand? Brand { get; set; }


        public ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();
        public ICollection<Characteristic> Characteristics { get; set; } = new List<Characteristic>();

    }
}
