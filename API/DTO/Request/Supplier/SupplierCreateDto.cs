using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Supplier
{
    public class SupplierCreateDto
        {
            [Required]
            public required string Name { get; set; }

            [MaxLength(20)]
            public string? Phone { get; set; }

            [MaxLength(40)]
            public string? Email { get; set; }
        }
}

