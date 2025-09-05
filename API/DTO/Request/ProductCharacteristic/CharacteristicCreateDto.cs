using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CRM.API.DTO.Request.ProductCharacteristic
{
    public class CharacteristicCreateDto
    {
        [Required]
        public string Name { get; set; }
        public int CategoryID { get; set; }

    }
}
