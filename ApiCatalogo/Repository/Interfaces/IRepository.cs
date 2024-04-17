namespace ApiCatalogo.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T GetById(int id);
        T Create(T entity);
        T Update(int id,T entity);
        T Delete(T entity);
    }
}
