using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Responses;

namespace SolarCoffe.Services.Inventory.Interfaces
{
    public interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInvetory();
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetByProductId(int id);
        public List<ProductInventorySnapshot> GetSnaphostHistory();
    }
}