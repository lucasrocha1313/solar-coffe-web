using SolarCoffe.Data.Models;
using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Order.Interfaces
{
    public interface IOrderService
    {
        List<SalesOrder> GetOrders();
        ServiceResponse<SalesOrder> GenerateInvoiceOrder(SalesOrder order);
        ServiceResponse<SalesOrder> MarkFulfilled(int id);
    }
}