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

            if (!string.IsNullOrWhiteSpace(ProductName))
            {
                result = result.Where(a => a.ProductName.Contains(ProductName));
            }
            if (!string.IsNullOrWhiteSpace(CompanyName))
            {
                result = result.Where(a => a.SupplierName.Contains(CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                result = result.Where(a => a.CategoryName.Contains(CategoryName));
            }
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
        [HttpGet("{name}")]
        public string Get(string name)
        {
            //var result =
            //   (from p in _northwindContext.Products
            //    join s in _northwindContext.Suppliers on p.SupplierId equals s.SupplierId
            //    join c in _northwindContext.Categories on p.CategoryId equals c.CategoryId
            //    select new ProductsList
            //    {
            //        ProductId = p.ProductId,
            //        ProductName = p.ProductName,
            //        SupplierName = s.CompanyName,
            //        CategoryName = c.CategoryName,
            //        QuantityPerUnit = p.QuantityPerUnit,
            //        UnitPrice = p.UnitPrice,
            //        UnitsInStock = p.UnitsInStock,
            //        UnitsOnOrder = p.UnitsOnOrder,
            //        Discontinued = p.Discontinued
            //    });

            //if (!string.IsNullOrWhiteSpace(_ProductName))
            //{
            //    result = result.Where(a => a.ProductName.Contains(_ProductName));
            //}
            //if (!string.IsNullOrWhiteSpace(_CompanyName))
            //{
            //    result = result.Where(a => a.SupplierName == _CompanyName);
            //}
            //if (!string.IsNullOrWhiteSpace(_CategoryName))
            //{
            //    result = result.Where(a => a.CategoryName == _CategoryName);
            //}
            //if (!int.(minPrice))
            //{
            //    2,147,483,647
            //    result = result.Where(a => a.CategoryName == _CategoryName).ToList();
            //}
            //if (!string.IsNullOrWhiteSpace(maxPrice))
            //{
            //    result = result.Where(a => a.CategoryName == _CategoryName).ToList();
            //}
            return "result";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
