namespace SchoolManagement2.Repositories.GenericRepositories;

public interface IRepository<T> where T : class
{
    void Create(T entity);
    IEnumerable<T> GetAll();
    T GetById(Guid id);
    void Update(T entity);
    void Delete(Guid id);
}