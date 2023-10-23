namespace API.Dtos
{
    public class UpdateProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        //public string? SupplierName { get; set; }
        public int? CategoryId { get; set; }
        //public string? CategoryName { get; set; }
        public string QuantityPerUnit { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
