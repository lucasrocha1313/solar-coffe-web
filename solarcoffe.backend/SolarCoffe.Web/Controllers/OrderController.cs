using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Customer.Interfaces;
using SolarCoffe.Services.Order.Interfaces;
using SolarCoffe.Web.Dtos;

namespace SolarCoffe.Web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICostumerService _customerService;
        private readonly IMapper _mapper;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ICostumerService customerService, IMapper mapper)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody]InvoiceDto invoice) {
            _logger.LogInformation("Generating invoice");
            var order = _mapper.Map<SalesOrder>(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok();
        }

        [HttpGet("/api/order")]
        public ActionResult GetOrders() {
            var order = _orderService.GetOrders();
            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }


        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id) {
            _logger.LogInformation($"Marking order {id} completre... ");
            var response = _orderService.MarkFulfilled(id);
            return Ok(response);
        }
    }
}