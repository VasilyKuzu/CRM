using CRM.Core.Entities;

namespace CRM.API.DTO.Response.ProductCharacteristic
{
    public class CharacteristicValueReadDto
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
