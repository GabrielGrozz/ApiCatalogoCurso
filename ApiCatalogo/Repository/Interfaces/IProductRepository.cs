using ApiCatalogo.Pagination;
using Catalog.Models;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetProducts(ProductParameters parameters);

    }
}
