using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Brand
{
    public class BrandCreateDto
    {
        [Required]
        [MaxLength(80)]
        public string BrandName { get; set; }
    }
}
