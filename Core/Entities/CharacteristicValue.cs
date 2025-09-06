using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace CRM.Core.Entities
{
    public class CharacteristicValue //значения характеристик, привязанные к конкретному товару
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int CharacteristicID { get; set; }
        [Required]
        public required string Value { get; set; }
        public Product? Product { get; set; }
        public Characteristic? Characteristic { get; set; }
    }
}
