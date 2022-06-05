using LWAApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LWAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PACController : ControllerBase
    {

        
        readonly DatabaseContexta _dbContext;

        public PACController(DatabaseContexta dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public IQueryable Get()

        {
            var data1 = from k in _dbContext.ProductsTable
                        select new
                        {
                            ProductName = k.Name,
                            ProdyctPrice = k.Price
                        };

            return data1;
        }

        

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            void  Createproduct(Product product)
            {
                try
                {
                    _dbContext.ProductsTable.Add(product);
                    _dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw (ex);
                }

            }
            Createproduct(product);
            return await Task.FromResult(product);
        }


        [HttpPost]
        [Route("Category")]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            void CreateCategory(Category category)
            {
                try
                {
                    _dbContext.CategoriesTable.Add(category);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    throw;
                }

            }
            CreateCategory(category);
            return await Task.FromResult(category);
        }



    }
}
