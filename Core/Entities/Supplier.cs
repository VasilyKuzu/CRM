using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Entities
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(40)]
        public string Email { get; set; }
    }
}