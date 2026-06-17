namespace SchoolManagement2.Repositories;

public interface IRepository<T> where T : class
{
    void Create(T entity);
    IEnumerable<T> GetAll();
    void Update(Func<T, bool> predicate, T updatedEntity);
    void Delete(Func<T, bool> predicate);
}