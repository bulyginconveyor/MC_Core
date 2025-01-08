using core_service.services.Result;

namespace core_service.infrastructure.repository.redis.storages;

// Можно с помощью этого класса хранить ключи данных в кеше
// TODO: Надо ли это вообще? Или нахуй всю эту хрень?
public class KeyStorage<T> : IKeyStorage<T> where T : class
{
    public List<string> Keys { get; private set; } = [];

    public KeyStorage(){}

    public Result FullSetStorage(List<string> keys)
    {
        if(keys is null || keys.Count == 0)
            return Result.Error("List<string> keys is empty");
        
        Keys = keys;
        return Result.Success();
    }

    public Result AddKey(string key)
    {
        if (string.IsNullOrEmpty(key)) return Result.Error("Key is empty");
        
        if(Keys.Contains(key)) return Result.Error("Key already exists");
        
        Keys.Add(key);
        return Result.Success();
    }
    
    public Result RemoveKey(string key)
    {
        if (string.IsNullOrEmpty(key)) return Result.Error("Key is empty");
        
        if(!Keys.Contains(key)) return Result.Error("Key don't exists");
        
        Keys.Remove(key);
        return Result.Success();
    }
    
    public void Clear() => Keys.Clear();
    public bool Contains(string key) => Keys.Contains(key);

    private Result<List<string>> GetByPrefix(string prefix)
    {
        if (string.IsNullOrEmpty(prefix)) Result<List<string>>.Error(null, "Prefix is empty");
        
        var result = Keys.Where(key => key.StartsWith(prefix)).ToList();
        return Result<List<string>>.Success(result);
    }
}

public interface IKeyStorage<T> where T : class
{
}
