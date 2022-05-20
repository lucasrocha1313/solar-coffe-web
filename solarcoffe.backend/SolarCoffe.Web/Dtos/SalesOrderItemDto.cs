using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffe.Web.Dtos
{
    public class SalesOrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductDto Product { get; set; }
    }
}