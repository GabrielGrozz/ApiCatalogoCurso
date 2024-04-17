using ApiCatalogo.Context;
using ApiCatalogo.Repository.Interfaces;
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
        private readonly IRepository<Product> _context;
        public ProductsController(IRepository<Product> context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() 
        {
            var products = _context.Get();
            if (products is null)
            {
                return NotFound("não foi possível encontrar os produtos");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> GetById(int id) 
        {
            var product = _context.GetById(id);
            if (product == null)
            {
                return NotFound("não foi possível encontrar o produto");
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product) 
        {
            if(product == null)
            {
                return BadRequest("não foi possível encontrar o produto");
            }
            var createdProduct = _context.Create(product);

            return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId}, createdProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Product product)
        {
            if(id != product.ProductId)
            {
                return BadRequest("id invalido");
            }
            _context.Update(id, product);
            return Ok($"o produto {product.Name} foi alterado");
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _context.GetById(id);
            if (product == null)
            {
                return NotFound("não foi possível encontrar o produto");
            }

             _context.Delete(product);
            return Ok($"o item de id {id} foi excluido");
        }
    }
}
