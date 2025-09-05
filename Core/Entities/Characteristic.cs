using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CRM.Core.Entities
{
    public class Characteristic //привязанные к категори характеристики без значений
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
