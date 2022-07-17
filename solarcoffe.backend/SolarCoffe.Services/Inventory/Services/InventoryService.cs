using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffe.Data;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Inventory.Interfaces;
using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Inventory.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext db, ILogger<InventoryService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public ProductInventory GetByProductId(int id) => _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == id);

        public List<ProductInventory> GetCurrentInvetory()
        {
            return _db
                .ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        public List<ProductInventorySnapshot> GetSnaphostHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductInventorySnapshots
                .Include(pis => pis.Product)
                .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                .ToList();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try{
                var inventory = _db
                    .ProductInventories
                    .Include(pi => pi.Product)
                    .First(pi => pi.Product.Id == id);

                try {
                    CreateSnapshot(inventory);
                } catch(Exception e) {
                    _logger.LogError($"Error creating snapshot: {e.Message}");
                }

                inventory.QuantityOnHand += adjustment;

                _db.SaveChanges();
                return new ServiceResponse<ProductInventory>{
                    Data = inventory,
                    Time = DateTime.Now,
                    Message = $"Product {id} inventory adjusted",
                    IsSuccess = true
                };
            } catch(Exception e) {
                return new ServiceResponse<ProductInventory>{
                    Data = null,
                    Time = DateTime.Now,
                    Message = $"Error updating Product {id} inventory: {e.Message}",
                    IsSuccess = false
                };
            }
        }        

        private void CreateSnapshot(ProductInventory productInventory)
        {
            var snapshot = new ProductInventorySnapshot {
                SnapshotTime = DateTime.UtcNow,
                Product = productInventory.Product,
                QuantityOnHand =  productInventory.QuantityOnHand
            };

            _db.Add(snapshot);
        }
    }    
}