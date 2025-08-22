using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.DTO
{
    public class ProductUpdateDto
    {
        public string ProductName { get; set; }
        public string ProductArticle { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
    }
}
