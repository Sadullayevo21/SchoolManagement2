using System.Text.Json;

namespace SchoolManagement2.Repositories;

public class JsonRepository<T> : IRepository<T> where T : class
{
    private readonly string _path;

    public JsonRepository(string fileName)
    {
        _path = fileName;
        if (!File.Exists(_path))
        {
            File.WriteAllText(_path, "[]");
        }
    }

    public IEnumerable<T> GetAll()
    {
        var data = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<IEnumerable<T>>(data) ?? new List<T>();
    }

    public void Create(T entity)
    {
        var entities = GetAll().ToList();
        entities.Add(entity);
        var data = JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, data);
    }

    public void Update(Func<T, bool> predicate, T updatedEntity)
    {
        var entities = GetAll().ToList();
        var item = entities.FirstOrDefault(predicate);
        if (item != null)
        {
            int index = entities.IndexOf(item);
            entities[index] = updatedEntity;
            var data = JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, data);
        }
    }

    public void Delete(Func<T, bool> predicate)
    {
        var entities = GetAll().ToList();
        var item = entities.FirstOrDefault(predicate);
        if (item != null)
        {
            entities.Remove(item);
            var data = JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, data);
        }
    }
}