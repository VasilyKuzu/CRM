using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Entities
{
    public class Brand
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(80)]
        public required string Name { get; set; }

    }
}
