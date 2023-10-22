using API.Models;

namespace API.Dtos
{
    public class ProductsList
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string SupplierName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public bool? Discontinued { get; set; }
    }
}
