using System;

namespace SolarCoffe.Web.Dtos
{
    public class ProductInventorySnapshotDto
    {
        public List<int> QuantityOnHand { get; set; }
        public int ProductId { get; set; }
    }

    public class SnapshotResponse {
        public List<ProductInventorySnapshotDto> ProductInventorySnapshots { get; set; }
        public List<DateTime> Timeline { get; set; }
    }
}