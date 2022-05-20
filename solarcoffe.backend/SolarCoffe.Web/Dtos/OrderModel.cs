namespace SolarCoffe.Web.Dtos
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsPaid { get; set; }
        public CustomerDto Customer { get; set; }
        public List<SalesOrderItemDto> SalesOrderItems { get; set; }
    }
}