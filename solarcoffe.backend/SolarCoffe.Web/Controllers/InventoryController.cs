using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolarCoffe.Services.Inventory.Interfaces;
using SolarCoffe.Web.Dtos;

namespace SolarCoffe.Web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IInventoryService _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService, IMapper mapper)
        {
            _logger = logger;
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory() {
            _logger.LogInformation("Getting Current inventory...");
            var inventories = _inventoryService.GetCurrentInvetory();
            var inventoriesDto = _mapper.Map<List<ProductInventoryDto>>(inventories);
            return Ok(inventoriesDto.OrderBy(inv => inv.Product.Name));
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentDto shipmentDto) {
            _logger.LogInformation($"Updating inventory for {shipmentDto.ProductId} - Adjustment: {shipmentDto.Adjustment}");
            var invetory = _inventoryService.UpdateUnitsAvailable(shipmentDto.ProductId, shipmentDto.Adjustment);
            return Ok(invetory);
        }

        [HttpGet("/api/inventory/snapshot")]
        public ActionResult GetSnapshotHistory() {
            _logger.LogInformation("Getting snapshot history");

            try {
                var snapshotHistory = _inventoryService.GetSnaphostHistory();

                var timelineMarker = snapshotHistory
                    .Select(t => t.SnapshotTime)
                    .Distinct()
                    .ToList();

                var snapshots = snapshotHistory
                    .GroupBy(hist => hist.Product, hist => hist.QuantityOnHand,
                        (key, g) => new ProductInventorySnapshotDto {
                            ProductId = key.Id,
                            QuantityOnHand = g.ToList()
                        })
                    .OrderBy(hist => hist.ProductId)
                    .ToList();
                
                var viewModel = new SnapshotResponse {
                    Timeline = timelineMarker,
                    ProductInventorySnapshots = snapshots
                };

                return Ok(viewModel);
            }
            catch(Exception e) {
                _logger.LogError("Error getting snapshot history", e);
                return BadRequest(e.Message);
            }
            
        }
    }
}