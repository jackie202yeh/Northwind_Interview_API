using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly NorthwindContext _northwindContext;
        public ProductController(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<ProductsList> Get(string? ProductName, string? CompanyName, string? CategoryName, int? minPrice, int? maxPrice)
        {
            var result =
               (from p in _northwindContext.Products
                join s in _northwindContext.Suppliers on p.SupplierId equals s.SupplierId
                join c in _northwindContext.Categories on p.CategoryId equals c.CategoryId
                select new ProductsList
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    SupplierName = s.CompanyName,
                    CategoryName = c.CategoryName,
                    QuantityPerUnit = p.QuantityPerUnit,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock,
                    UnitsOnOrder = p.UnitsOnOrder,
                    Discontinued = p.Discontinued
                });
            // 搜尋產品名稱
            if (!string.IsNullOrWhiteSpace(ProductName))
            {
                result = result.Where(a => a.ProductName.Contains(ProductName));
            }
            // 搜尋供應商名稱
            if (!string.IsNullOrWhiteSpace(CompanyName))
            {
                result = result.Where(a => a.SupplierName.Contains(CompanyName));
            }
            // 搜尋類別名稱
            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                result = result.Where(a => a.CategoryName.Contains(CategoryName));
            }
            // 處理價格最大最小值範圍
            int _minPrice = 0;
            int _maxPrice = 2147483647;
            if (minPrice != null)
            {
                _minPrice = (int)minPrice;
            }
            if (maxPrice != null)
            {
                _maxPrice = (int)maxPrice;
            }
            result = result.Where(a => a.UnitPrice>= _minPrice && a.UnitPrice <= _maxPrice);
            return result;
        }

        // GET api/<ProductController>/chai
        [HttpGet("{id}")]
        public IEnumerable<ProductsList> Get(int id)
        {
            var result =   from p in _northwindContext.Products
                           where p.ProductId == id
                           join s in _northwindContext.Suppliers on p.SupplierId equals s.SupplierId
                           join c in _northwindContext.Categories on p.CategoryId equals c.CategoryId
                           select new ProductsList
                           {
                               ProductId = p.ProductId,
                               ProductName = p.ProductName,
                               SupplierName = s.CompanyName,
                               CategoryName = c.CategoryName,
                               QuantityPerUnit = p.QuantityPerUnit,
                               UnitPrice = p.UnitPrice,
                               UnitsInStock = p.UnitsInStock,
                               UnitsOnOrder = p.UnitsOnOrder,
                               Discontinued = p.Discontinued
                           };
            return result;
        }

        // POST api/<ProductController>
        [HttpPost]
        public string Post([FromBody] AddProduct value)
        {
            // 檢查SupplierID和CategoryID是否存在於資料庫中
            var existingSupplier = _northwindContext.Suppliers.FirstOrDefault(s => s.SupplierId == value.SupplierId);
            var existingCategory = _northwindContext.Categories.FirstOrDefault(c => c.CategoryId == value.CategoryId);

            if (existingSupplier == null)
            {
                return "供應商不存在";
            }
            else if (existingCategory == null)
            {
                return "類別不存在";
            }
            else
            {
                Product New_Product = new Product
                {
                    ProductName     = value.ProductName,
                    SupplierId      = value.SupplierId,
                    CategoryId      = value.CategoryId,
                    QuantityPerUnit = value.QuantityPerUnit,
                    UnitPrice       = value.UnitPrice,
                    UnitsInStock    = value.UnitsInStock,
                    UnitsOnOrder    = value.UnitsOnOrder,
                    ReorderLevel    = value.ReorderLevel,
                    Discontinued    = value.Discontinued,
                    Category        = _northwindContext.Categories.Find(value.CategoryId),
                    Supplier        = _northwindContext.Suppliers.Find(value.SupplierId)
                };
                
                _northwindContext.Products.Add(New_Product);
                _northwindContext.SaveChanges();

                return "ok";
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
