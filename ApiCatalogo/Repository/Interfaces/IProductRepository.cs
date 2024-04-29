using ApiCatalogo.Pagination;
using Catalog.Models;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public PagedList<Product> GetProducts(ProductParameters parameters);

    }
}
