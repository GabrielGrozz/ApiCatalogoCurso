using Catalog.Models;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetWithProducts();

    }
}
