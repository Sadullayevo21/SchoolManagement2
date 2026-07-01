using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolManagement2.Repositories.GenericRepositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly string path;

    public Repository(string fileName)
    {
        path = fileName;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        if (!File.Exists(path))
        {
            return new List<T>();
        }

        var data = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<IEnumerable<T>>(data) ?? new List<T>();
    }

    public async Task CreateAsync(T entity)
    {
        List<T> items = (await GetAllAsync()).ToList();
        items.Add(entity);
        
        var data = JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(path, data);
    }
    
    public async Task<T?> GetByIdAsync(Guid id)
    {
        var items = await GetAllAsync();
        return items.FirstOrDefault(item => ((dynamic)item).Id == id);
    }

    public async Task DeleteAsync(Guid id)
    { 
        var items = (await GetAllAsync()).ToList();
        var deleteEntity = items.FirstOrDefault(eachItem => ((dynamic)eachItem).Id == id);
        
        if (deleteEntity != null)
        {
            items.Remove(deleteEntity);
            var itemdata = JsonSerializer.Serialize(items);
            await File.WriteAllTextAsync(path, itemdata);
        }
    }

    public async Task UpdateAsync(T entity)
    {
        var items = (await GetAllAsync()).ToList();
        var deletedEntity = items.FirstOrDefault(eachItem => ((dynamic)eachItem).Id == ((dynamic)entity).Id);
        
        if (deletedEntity != null)
        {
            items.Remove(deletedEntity);
        }
        
        items.Add(entity);
        var data = JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(path, data);
    }
}