using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.DTO.Request.Product
{
    public class ProductUpdateDto
    {
        public string ProductName { get; set; }
        public string ProductArticle { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
    }
}
