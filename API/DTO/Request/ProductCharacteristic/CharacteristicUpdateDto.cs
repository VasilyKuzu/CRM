using Microsoft.AspNetCore.Mvc;

namespace CRM.API.DTO.Request.ProductCharacteristic
{
    public class CharacteristicUpdateDto
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
    }
}
