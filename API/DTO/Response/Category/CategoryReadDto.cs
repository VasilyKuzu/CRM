using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Response.Category
{
    public class CategoryReadDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
    }
}
