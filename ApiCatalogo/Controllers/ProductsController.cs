using ApiCatalogo.Context;
using Catalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() 
        {
            var products = _context.products.AsNoTracking().ToList();
            if (products is null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> GetById(int id) 
        {
            var product = _context.products.AsNoTracking().FirstOrDefault(e => e.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product) 
        {
            if(product == null)
            {
                return BadRequest();
            }
            _context.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId}, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Product product)
        {
            if(id != product.ProductId)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _context.products.FirstOrDefault(e => e.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

             _context.Remove(product);
             _context.SaveChanges();
            return Ok(product);
        }
    }
}
