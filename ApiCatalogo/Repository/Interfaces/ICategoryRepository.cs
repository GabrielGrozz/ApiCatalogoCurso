using Catalog.Models;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetWithProducts();

    }
}
