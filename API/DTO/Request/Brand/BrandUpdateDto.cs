using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Brand
{
    public class BrandUpdateDto
    {
        [Required]
        [MaxLength(80)]
        public required string Name { get; set; }
    }
}
