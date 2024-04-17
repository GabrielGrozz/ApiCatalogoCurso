using ApiCatalogo.Context;
using ApiCatalogo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    //os outros repositorios irão herdar desse repositorio generico
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Get()
        {
            //o método Set é utilizado para acessar uma coleção
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(int id, T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return entity;
        }


    }
}
