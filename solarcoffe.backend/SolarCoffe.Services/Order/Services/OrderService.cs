using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffe.Data;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Inventory.Interfaces;
using SolarCoffe.Services.Order.Interfaces;
using SolarCoffe.Services.Product.Interfaces;
using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Order.Services
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventorySerice;

        public OrderService(SolarDbContext db, IProductService productService, IInventoryService inventorySerice)
        {
            _db = db;
            _productService = productService;
            _inventorySerice = inventorySerice;
        }

        public ServiceResponse<SalesOrder> GenerateOpenOrder(SalesOrder order)
        {
            foreach (var item in order.SalesOrderItems)
            {
                var product = _productService.GetProductById(item.Product.Id);
                if (product == null)
                {
                    return new ServiceResponse<SalesOrder>
                    {
                        Data = order,
                        Time = DateTime.Now,
                        Message = $"Couldn't find the product {item.Product.Id}",
                        IsSuccess = false
                    };
                }
                item.Product = product;
                var inventoryId = _inventorySerice.GetByProductId(product.Id).Id;
                _inventorySerice.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<SalesOrder>
                {
                    Data = order,
                    Time = DateTime.Now,
                    Message = $"Order {order.Id} created",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<SalesOrder>
                {
                    Data = order,
                    Time = DateTime.Now,
                    Message = $"Error creaing Order: {ex.Message}",
                    IsSuccess = false
                };
            }
        }

        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(so => so.Customer)
                    .ThenInclude(c => c.PrimaryAddress)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(soi => soi.Product)
                .ToList();
        }

        public ServiceResponse<SalesOrder> MarkFulfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);
            if (order == null)
            {
                return new ServiceResponse<SalesOrder>
                {
                    Data = order,
                    Time = DateTime.Now,
                    Message = $"Couldn't find the order {id}",
                    IsSuccess = false
                };
            }
            order.UpdatedOn = DateTime.Now;
            order.IsPaid = true;

            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<SalesOrder>
                {
                    Data = order,
                    Time = DateTime.Now,
                    Message = $"Order {order.Id} fulfilled",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<SalesOrder>
                {
                    Data = order,
                    Time = DateTime.Now,
                    Message = $"Error fulfilling order {id}: {e.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}