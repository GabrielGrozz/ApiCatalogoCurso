using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.Repository.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _context;
        public CategoriesController(ICategoryRepository repository, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _context = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            _logger.Log(LogLevel.Warning, "------------ [ testando o registro de log no método GET do controller Categories ] ------------");

            var categories = _context.Get();
            if (categories is null)
            {
                return NotFound("não foi possível encontrar as categorias");
            }

            var dto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(dto);
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<CategoryDTO>> GetWithProducts()
        {
            var categories = _context.GetWithProducts();
            if (categories is null)
            {
                return NotFound("não foi possível encontrar as categorias");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<CategoryDTO> GetById(int id)
        {
            var category = _context.GetById(id);
            if (category == null)
            {
                return NotFound("não foi possível encontrar a categoria");
            }

            var dto = _mapper.Map<CategoryDTO>(category);

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Post(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            if (category == null)
            {
                return BadRequest();
            }

            _context.Create(category);

            var dto = _mapper.Map<CategoryDTO>(category);

            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, dto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest("id invalido");
            }

            var category = _mapper.Map<Category>(categoryDto);

            _context.Update(id, category);

            var dto = _mapper.Map<CategoryDTO>(category);

            return Ok($"a categoria {dto.Name} foi alterada");
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

            var dto = _mapper.Map<CategoryDTO>(category);

            return Ok($"a categoria {dto.Name} foi excluida.");
        }
    }
}
