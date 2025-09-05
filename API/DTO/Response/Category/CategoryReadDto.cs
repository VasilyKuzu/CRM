using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Response.Category
{
    public class CategoryReadDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
