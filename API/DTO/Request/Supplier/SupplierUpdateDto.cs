using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Supplier
{
    public class SupplierUpdateDto
    {
        public string SupplierName { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(40)]
        public string? Email { get; set; }
    }
}
