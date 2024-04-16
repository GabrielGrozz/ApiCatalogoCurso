using Catalog.Models;

namespace ApiCatalogo.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        //por padrão, todos os membro de uma interface pública, são públicos
        IEnumerable<Category> Get();
        Category GetById(int id);
        Category Create(Category category);
        Category Update(int id, Category category);
        Category Delete(int id);

        

    }
}
