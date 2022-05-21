namespace SolarCoffe.Web.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public CustomerDto Customer { get; set; }
        public List<SalesOrderItemDto> SalesOrderItems { get; set; }
    }
}