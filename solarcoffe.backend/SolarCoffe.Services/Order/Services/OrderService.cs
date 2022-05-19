using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Order.Interfaces;
using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Order.Services
{
    public class OrderService : IOrderService
    {
        public ServiceResponse<SalesOrder> GenerateInvoiceOrder(SalesOrder order)
        {
            throw new NotImplementedException();
        }

        public List<SalesOrder> GetOrders()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<SalesOrder> MarkFulfilled(int id)
        {
            throw new NotImplementedException();
        }
    }
}