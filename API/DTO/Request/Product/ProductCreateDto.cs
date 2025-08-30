using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Product
{
    public class ProductCreateDto
    {
        [Required]
        public string ProductName { get; set; }
        public string ProductArticle { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int BrandID { get; set; }
    }
}
