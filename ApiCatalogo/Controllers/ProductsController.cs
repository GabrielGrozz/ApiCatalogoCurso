using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository.Interfaces;
using AutoMapper;
using Catalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _context;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            var products = _context.Get();
            if (products is null)
            {
                return NotFound("não foi possível encontrar os produtos");
            }

            var dto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(dto);
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts([FromQuery] ProductParameters parameters) 
        {
            var products = _context.GetProducts(parameters);


            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };

            //transforma em json e adiciona no header
            Response.Headers.Append("X-pagination1", JsonConvert.SerializeObject(metadata));

            var dto = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(dto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<ProductDTO> GetById(int id)
        {
            var product = _context.GetById(id);
            if (product == null)
            {
                return NotFound("não foi possível encontrar o produto");
            }
            //mapeia os produtos para um dto
            var dto = _mapper.Map<ProductDTO>(product);

            return Ok(dto);
        }

        [HttpPost]
        //recebe um dto e retorna um dto
        public ActionResult<ProductDTO> Post(ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("não foi possível encontrar o produto");
            }

            var prod = _mapper.Map<Product>(productDto);

            _context.Create(prod);

            var dto = _mapper.Map<ProductDTO>(prod);

            return new CreatedAtRouteResult("GetProduct", new { id = dto.ProductId }, dto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest("id invalido");
            }

            var prod = _mapper.Map<Product>(productDto);

            _context.Update(id, prod);

            var dto = _mapper.Map<ProductDTO>(prod);

            return Ok($"o produto {dto.Name} foi alterado");
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _context.GetById(id);
            if (product == null)
            {
                return NotFound("não foi possível encontrar o produto");
            }

            _context.Delete(product);

            var dto = _mapper.Map<ProductDTO>(product);

            return Ok($"o item {dto.Name} foi excluido");
        }
    }
}
