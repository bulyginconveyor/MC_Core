using core_service.domain.models.@base;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.infrastructure.repository.redis;

public class BaseCacheRepository<T> : ICacheRepository<T> where T : class, ICached<T>
{
    public async Task<Result<T>> Get(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Add(string key, T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Add(string key, T entity, TimeSpan timeLife)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Update(string key, T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Update(string key, T entity, TimeSpan timeLife)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UnSet(string key)
    {
        throw new NotImplementedException();
    }
}
