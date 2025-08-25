using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.DTO.Responce.Product
{
    public class ProductReadDto
    {
        public string ProductName { get; set; }
        public string ProductArticle { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

    }
}
