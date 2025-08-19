using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }

        [MaxLength(80)]
        public string CategoryName { get; set; }
        
    }
}