using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.DTO
{
    public class ProductReadDto
    {
        public string ProductName { get; set; }
        public string ProductArticle { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

    }
}
