using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.Product
{
    public class ProductUpdateDto
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Article { get; set; }
        public string? Description { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
    }
}
