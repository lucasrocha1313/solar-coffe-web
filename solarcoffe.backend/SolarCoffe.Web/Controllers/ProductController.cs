using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Product.Interfaces;
using SolarCoffe.Web.Dtos;

namespace SolarCoffe.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost("/api/product")]
        public ActionResult AddProduct([FromBody] ProductDto productDto)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            _logger.LogInformation($"Adding product {productDto.Name}");
            var newProduct = _mapper.Map<Product>(productDto);
            var newProductResponse = _productService.CreateProduct(newProduct);
            return Ok(newProductResponse);
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct()
        {
            _logger.LogInformation("Getting all products");
            var product = _productService.GetAllProducts();
            return Ok(_mapper.Map<List<ProductDto>>(product));
        }

        [HttpPatch("/api/product/{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation($"Archiving product {id}");
            var archived = _productService.ArchiveProduct(id);
            return Ok(archived);
        }
    }
}