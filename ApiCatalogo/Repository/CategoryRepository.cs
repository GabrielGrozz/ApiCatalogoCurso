using ApiCatalogo.Context;
using ApiCatalogo.Repository.Interfaces;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        protected readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context) { _context = context; }

        public IEnumerable<Category> GetWithProducts()
        {
            return _context.categories.Include(e => e.Products);
        }


        //----------ESSES MÉTODOS FORAM SUBSTITUIDOS PELOS MÉTODOS GENÉRICOS DE REPOSITORY---------------

        //public IEnumerable<Category> Get()
        //{
        //    return _context.categories.ToList();
        //}

        //public Category GetById(int id)
        //{
        //    Category category = _context.categories.FirstOrDefault(e => e.CategoryId == id);
        //    return category;
        //}

        //public Category Create(Category category)
        //{
        //    if (category == null)
        //        throw new ArgumentException(nameof(category));

        //    _context.categories.Add(category);
        //    _context.SaveChanges();
        //    return category;
        //}

        //public Category Update(int id, Category category)
        //{
        //    if(category == null)
        //        throw new ArgumentNullException(nameof(category));

        //    _context.Entry(category).State = EntityState.Modified;
        //    _context.SaveChanges();
        //    return category;
        //}

        //public Category Delete(int id)
        //{
        //    //diferentemente do FirstOrDefault, o Find procura pela chave primária e não pelas propriedades, e ele também faz uma pesquisa no contexto e não no banco de dados
        //    Category category = _context.categories.Find(id);

        //    if (category == null)
        //        throw new ArgumentNullException(nameof(category));

        //    _context.categories.Remove(category);
        //    _context.SaveChanges();
        //    return category;
        //}



    }
}
