using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Response.Supplier
{
    public class SupplierReadDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
