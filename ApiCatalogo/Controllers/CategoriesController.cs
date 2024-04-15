using ApiCatalogo.Context;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context, ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            _logger.Log(LogLevel.Warning ,"------------ [ testando o registro de log no método GET do controller Categories ] ------------");

            var categories = _context.categories.AsNoTracking().ToList();
            if (categories is null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetWithProducts()
        {
            var category = _context.categories.AsNoTracking().Include(e => e.Products).ToList();
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> GetById(int id)
        {
            var categories = _context.categories.AsNoTracking().FirstOrDefault(e => e.CategoryId == id);
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            _context.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _context.categories.FirstOrDefault(e => e.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }
    }
}
