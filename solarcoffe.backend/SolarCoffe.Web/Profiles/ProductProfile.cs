using AutoMapper;
using SolarCoffe.Data.Models;
using SolarCoffe.Web.Dtos;

namespace SolarCoffe.Web.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductInventory, ProductInventoryDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerAddress, CustomerAddressDto>();
            CreateMap<SalesOrder, InvoiceDto>();
            CreateMap<SalesOrderItem, SalesOrderItemDto>();

            CreateMap<ProductDto, Product>();
            CreateMap<ProductInventoryDto, ProductInventory>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerAddressDto, CustomerAddress>();
            CreateMap<InvoiceDto, SalesOrder>();
            CreateMap<SalesOrderItemDto, SalesOrderItem>();
            
        }        
    }
}