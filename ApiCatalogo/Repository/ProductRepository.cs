using ApiCatalogo.Context;
using ApiCatalogo.Repository.Interfaces;
using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context) { }


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
