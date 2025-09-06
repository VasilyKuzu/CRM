using CRM.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.DTO.Response.Product
{
    public class ProductReadDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Article { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }

    }
}
