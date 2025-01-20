using core_service.domain.models.@base;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;
using core_service.services.StringExtensions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace core_service.infrastructure.repository.redis.repositories.@base;

public class BaseCacheRepository<T>(IConnectionMultiplexer mux) : ICacheRepository<T> where T : class
{
    protected readonly IDatabase _redis = mux.GetDatabase();
    protected virtual string PREFIX
    {
        get => typeof(T).Name.ToSnakeCase();
    }
    
    public virtual async Task<Result<T>> Get(string key)
    {
        T entity;
        
        try
        {
            var jsonStr = await _redis.StringGetAsync($"{PREFIX}{key}");
            if (jsonStr.IsNullOrEmpty)
                return Result<T>.Error(null!, "Not found");
            
            entity = JsonConvert.DeserializeObject<T>(jsonStr);
        }
        catch (Exception ex)
        {
            return Result<T>.Error(null!, ex.Message);
        }
        
        return Result<T>.Success(entity);
    }

    public virtual async Task<Result> Add(string key, T entity)
    {
        try
        {
            await _redis.StringSetAsync($"{PREFIX}{key}", ToJsonString(entity));
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
        
        return Result.Success();
    }

    public virtual async Task<Result> Add(string key, T entity, TimeSpan timeLife)
    {
        try
        {
            await _redis.StringSetAsync($"{PREFIX}{key}", ToJsonString(entity), timeLife);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
        
        return Result.Success();
    }

    public virtual async Task<Result> Update(string key, T entity) 
        => await Add(key, entity);

    public virtual async Task<Result> Update(string key, T entity, TimeSpan timeLife) 
        => await Add(key, entity, timeLife);

    public virtual async Task<Result> UnSet(string key)
    {
        try
        {
            await _redis.KeyDeleteAsync($"{PREFIX}{key}");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
        
        return Result.Success();
    }

    public virtual string ToJsonString(T entity) 
        => JsonConvert.SerializeObject(entity);
}
