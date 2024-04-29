using ApiCatalogo.Context;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository.Interfaces;
using Catalog.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiCatalogo.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context) { }

        public PagedList<Product> GetProducts(ProductParameters parameters)
        {
            //return Get()
            //    //ordena pelo nome
            //    .OrderBy(p => p.Name)
            //    //pula a pagina ainterior
            //    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            //    //pega os produtos na proxima pagina
            //    .Take(parameters.PageSize).ToList();

            var products = Get().OrderBy(p => p.ProductId).AsQueryable();
            var productsOrdened = PagedList<Product>.ToPagedList(products, parameters.PageNumber, parameters.PageSize);

            return productsOrdened;
        }


        //----------ESSES MÉTODOS FORAM SUBSTITUIDOS PELOS MÉTODOS GENÉRICOS DE REPOSITORY---------------

        //public IEnumerable<Product> Get()
        //{
        //    return _context.products.ToList();
        //}

        //public Product GetById(int id)
        //{
        //    Product product = _context.products.FirstOrDefault(e => e.ProductId == id);
        //    return product;
        //}

        //public Product Create(Product product)
        //{
        //    if (product == null)            
        //        throw new ArgumentNullException(nameof(product));

        //        _context.products.Add(product);
        //        _context.SaveChanges();
        //        return product;

        //}
        //public Product Update(int id, Product product)
        //{
        //    if (product == null) 
        //        throw new ArgumentNullException(nameof(product));

        //    _context.Entry(product).State = EntityState.Modified;
        //    _context.SaveChanges();
        //    return product;
        //}

        //public Product Delete(int id)
        //{
        //    Product product = _context.products.Find(id);

        //    if (product == null)
        //        throw new ArgumentNullException();

        //    _context.products.Remove(product);
        //    _context.SaveChanges();
        //    return product;                
        //}

    }
}
