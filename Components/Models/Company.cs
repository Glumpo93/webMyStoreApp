using System.ComponentModel.DataAnnotations;

namespace webMyStoreApp.Components.Models
{
    public class Company
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public byte IdType { get; set; }
        public int IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public byte AddressProvince { get; set; }
        public byte AddressCity { get; set; }
        public byte AddressDistrict { get; set; }
        public string AddressDirections { get; set; }
        public string EmailAddress { get; set; }
        public string DeliveryAddress { get; set; }

    }
}
