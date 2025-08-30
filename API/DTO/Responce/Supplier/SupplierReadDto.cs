using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Responce.Supplier
{
    public class SupplierReadDto
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
