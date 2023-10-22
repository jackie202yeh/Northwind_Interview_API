using API.Models;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly NorthwindContext _northwindContext;

        public TestController(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public List<CategoryStockSummary> Get()
        {
            var categoriesQueryable = _northwindContext.Categories.AsQueryable();
            var productsQueryable = _northwindContext.Products.AsQueryable();

            var result = (from c in categoriesQueryable
                          //where c.CategoryName == "Beverages"
                          join p in productsQueryable
                          on c.CategoryId equals p.CategoryId
                          group p by c.CategoryName into g
                          select new CategoryStockSummary
                          {
                              CategoryName = g.Key,
                              TotalStock = g.Sum(p => p.UnitsInStock),
                              AverageStock = g.Average(p => p.UnitsInStock)
                          }).ToList();

            return result;
            //var result = from c in _northwindContext.Categories
            //             where c.CategoryName == "Beverages"
            //             join p in _northwindContext.Products on c.CategoryId equals p.CategoryId
            //             group c by c.CategoryName into grp
            //             select new
            //             {
            //                 CategoryName = grp.Key,
            //                 Quantity = grp.Sum(p => p.UnitsInStock)
            //             };
            //return result;

            // return _northwindContext.Categories.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
