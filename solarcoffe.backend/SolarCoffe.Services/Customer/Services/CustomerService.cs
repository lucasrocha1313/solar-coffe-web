using Microsoft.EntityFrameworkCore;
using SolarCoffe.Data;
using SolarCoffe.Services.Customer.Interfaces;
using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Customer.Services
{
    public class CustomerService : ICostumerService
    {
        private readonly SolarDbContext _db;

        public CustomerService(SolarDbContext db)
        {
            _db = db;
        }

        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    Time = DateTime.Now,
                    Message = "Saved new customer",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    Time = DateTime.Now,
                    Message = e.Message,
                    IsSuccess = false
                };
            }

        }

        public ServiceResponse<Data.Models.Customer> DeleteCustomer(int id)
        {
            var customerToDelete = _db.Customers.Find(id);
            if (customerToDelete == null)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = null,
                    Time = DateTime.Now,
                    Message = "Customer to delete not found",
                    IsSuccess = true
                };
            }

            try
            {
                _db.Customers.Remove(customerToDelete);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customerToDelete,
                    Time = DateTime.Now,
                    Message = "Customer deleted!",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customerToDelete,
                    Time = DateTime.Now,
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();
        }

        public Data.Models.Customer GetById(int id) => _db.Customers.Find(id);
    }
}