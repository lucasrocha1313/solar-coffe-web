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
        }        
    }
}