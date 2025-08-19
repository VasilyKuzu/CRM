using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Entities
{
    public class Brand
    {
        public int BrandID { get; set; }

        [MaxLength(80)]
        public string BrandName { get; set; }

    }
}
