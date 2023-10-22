namespace API.Dtos
{
    public class CategoryStockSummary
    {
        public string? CategoryName { get; set; }
        public int? TotalStock { get; set; }
        public double? AverageStock { get; set; }
    }
}
