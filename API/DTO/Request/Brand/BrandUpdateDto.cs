using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Brand
{
    public class BrandUpdateDto
    {
        [MaxLength(80)]
        public string BrandName { get; set; }
    }
}
