namespace SolarCoffe.Web.Dtos
{
    public class CustomerAddressDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }        
        public string AddressLine1 { get; set; }        
        public string AddressLine2 { get; set; }        
        public string City { get; set; }        
        public string State { get; set; }        
        public string PostalCode { get; set; }        
        public string Country { get; set; }   
    }
}