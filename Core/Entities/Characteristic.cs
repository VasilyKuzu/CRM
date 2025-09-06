using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CRM.Core.Entities
{
    public class Characteristic //привязанные к категори характеристики без значений
    {
        public int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required int CategoryID { get; set; }

        public Category? Category { get; set; }
    }
}
