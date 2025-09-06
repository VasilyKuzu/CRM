using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Category
{
    public class CategoryUpdateDto
    {
        [Required]
        [MaxLength(80)]
        public required string Name { get; set; }
    }
}
