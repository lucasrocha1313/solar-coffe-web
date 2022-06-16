namespace SolarCoffe.Web.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }

        public CustomerAddressDto PrimaryAddress { get; set; }
    }
}