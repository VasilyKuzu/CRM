using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Product
{
    public class ProductCreateDto
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Article { get; set; }
        public string? Description { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int BrandID { get; set; }
    }
}
