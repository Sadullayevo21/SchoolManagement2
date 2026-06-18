using System.Text.Json;
using System.Linq;

namespace SchoolManagement2.Repositories.GenericRepositories;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly string path;

    public Repository(string fileName)
    {
        path = fileName;
    }

    public IEnumerable<T> GetAll()
    {
        var data = File.ReadAllText(path);
        return JsonSerializer.Deserialize<IEnumerable<T>>(data) ?? new List<T>();
    }

    public void Create(T entity)
    {
        List<T> items = GetAll().ToList();
        items.Add(entity);
        var data = JsonSerializer.Serialize(items);
        File.WriteAllText(path, data);
    }
    
    public T GetById(Guid id)
    {
        var items = GetAll();
        return items.FirstOrDefault(item => ((dynamic)item).Id == id);
    }

    public void Delete(Guid Id)
    { 
        var items = GetAll().ToList();
        var deleteEntity = items.FirstOrDefault(eachItem => ((dynamic)eachItem).Id == Id);
        items.Remove(deleteEntity);
        var itemdata = JsonSerializer.Serialize(items);
        File.WriteAllText(path, itemdata);
    }

    public void Update(T entity)
    {
        var items = GetAll().ToList();
        var deletedEntity = items.FirstOrDefault(eachItem => ((dynamic)eachItem).Id == ((dynamic)entity).Id);
        items.Remove(deletedEntity);
        items.Add(entity);
        var data = JsonSerializer.Serialize(items);
        File.WriteAllText(path, data);
    }
}