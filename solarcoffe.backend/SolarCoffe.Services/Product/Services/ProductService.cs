using SolarCoffe.Data;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Product.Interfaces;
using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _dbContext;

        public ProductService(SolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try{
                var productToArchive = _dbContext.Products.Find(id);
                productToArchive.IsArchived = true;
                _dbContext.SaveChanges();
                return new ServiceResponse<Data.Models.Product> {
                    Data = productToArchive,
                    Time = DateTime.Now,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            } catch(Exception ex) {
                return new ServiceResponse<Data.Models.Product> {
                    Data = null,
                    Time = DateTime.Now,
                    Message = "Error saving product: " + ex.Message,
                    IsSuccess = false
                };
            }
        }
        
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _dbContext.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };
                _dbContext.ProductInventories.Add(newInventory);                

                _dbContext.SaveChanges();

                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.Now,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            } catch(Exception ex) {
                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.Now,
                    Message = "Error saving product: " + ex.Message,
                    IsSuccess = false
                };
            }
        }

        public List<Data.Models.Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Data.Models.Product GetProductById(int id)
        {
            return _dbContext.Products.Find(id);
        }
    }
}