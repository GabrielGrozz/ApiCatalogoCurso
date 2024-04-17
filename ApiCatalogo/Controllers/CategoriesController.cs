using ApiCatalogo.Context;
using ApiCatalogo.Repository.Interfaces;
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
        //private readonly IRepository<Category> _context;
        private readonly ICategoryRepository _context;
        public CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger)
        {
            _context = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            _logger.Log(LogLevel.Warning, "------------ [ testando o registro de log no método GET do controller Categories ] ------------");

            var categories = _context.Get();
            if (categories is null)
            {
                return NotFound("não foi possível encontrar as categorias");
            }
            return Ok(categories);
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetWithProducts()
        {
            var categories = _context.GetWithProducts();
            if (categories is null)
            {
                return NotFound("não foi possível encontrar as categorias");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> GetById(int id)
        {
            var category = _context.GetById(id);
            if (category == null)
            {
                return NotFound("não foi possível encontrar a categoria");
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            var createdCategory = _context.Create(category);

            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, createdCategory);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("id invalido");
            }
            _context.Update(id, category);
            return Ok($"a categoria {category.Name} foi alterada");
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _context.GetById(id);
            if (category == null)
            {
                return NotFound("não foi possível encontrar a categoria");
            }

            _context.Delete(category);
            return Ok($"a categoria de id {id} foi excluida.");
        }
    }
}
