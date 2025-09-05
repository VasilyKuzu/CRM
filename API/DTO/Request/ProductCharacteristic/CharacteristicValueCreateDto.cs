using CRM.Core.Entities;

namespace CRM.API.DTO.Request.ProductCharacteristic
{
    public class CharacteristicValueCreateDto
    {
        public int ProductID { get; set; }
        public int CharacteristicID { get; set; }
        public string Value { get; set; }
    }
}
