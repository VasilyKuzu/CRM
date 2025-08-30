using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Category
{
    public class CategoryUpdateDto
    {
        [MaxLength(80)]
        public string CategoryName { get; set; }
    }
}
