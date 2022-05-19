using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Customer.Interfaces
{
    public interface ICostumerService
    {
        List<Data.Models.Customer> GetAllCustomers();
        ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);
        ServiceResponse<Data.Models.Customer> DeleteCustomer(int id);
        Data.Models.Customer GetById(int id);
    }
}