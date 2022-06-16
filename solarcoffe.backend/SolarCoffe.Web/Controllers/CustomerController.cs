using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Customer.Interfaces;
using SolarCoffe.Web.Dtos;

namespace SolarCoffe.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICostumerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ILogger<CustomerController> logger, ICostumerService customerService, IMapper mapper)
        {
            _logger = logger;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost("/api/customers")]
        public ActionResult CreateCustomer([FromBody] CustomerDto customer) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating customer");
            var customerToCreate = _mapper.Map<Customer>(customer);
            customerToCreate.CreatedOn = DateTime.UtcNow;
            customerToCreate.UpdatedOn = DateTime.UtcNow;
            customerToCreate.PrimaryAddress.CreatedOn = DateTime.UtcNow;
            customerToCreate.PrimaryAddress.UpdatedOn = DateTime.UtcNow;
            var customerCreated = _customerService.CreateCustomer(customerToCreate);
            return Ok(customerCreated);
        }

        [HttpGet("/api/customers")]
        public ActionResult GetCustomers() {
            _logger.LogInformation("Getting customers");
            var customers = _customerService.GetAllCustomers();

            var customersDto = _mapper.Map<List<CustomerDto>>(customers.OrderByDescending(c => c.CreatedOn));

            return Ok(customersDto);
        }

        [HttpDelete("/api/customers/{id}")]
        public ActionResult DeleteCustomer(int id) {
            _logger.LogInformation($"Deleting Customer {id}");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }
    }
}